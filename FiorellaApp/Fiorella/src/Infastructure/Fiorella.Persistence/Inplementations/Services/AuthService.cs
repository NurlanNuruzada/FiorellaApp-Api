using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.AuthDTOs;
using Fiorella.Aplication.DTOs.ResponseDTOs;
using Fiorella.Domain.Entities;
using Fiorella.Domain.Enums;
using Fiorella.Persistence.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiorella.Persistence.Inplementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager,
                       RoleManager<IdentityRole> roleManager,
                       IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<TokenResponseDto> Login(SingInDto loginDto)
    {
        var AppUser = await _userManager.FindByEmailAsync(loginDto.UserOrEmail);
        if (AppUser is null)
        {
            AppUser = await _userManager.FindByNameAsync(loginDto.UserOrEmail);
            if (AppUser is null)
            {
                throw new SingInFailureException("Username Or Password is wrong!");
            }
        }
        SignInResult result = await _signInManager.CheckPasswordSignInAsync(AppUser, loginDto.password, true);
        if (!result.Succeeded)
        {
            throw new SingInFailureException("Username Or Password is wrong!");
        }
        if (!AppUser.IsActive)
        {
            throw new UserBlockedException("Your Accond Is Blocked!");
        }
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,AppUser.Id),
        };
        var roles = await _userManager.GetRolesAsync(AppUser);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:securityKey"]));
        var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        DateTime ExpireDate = DateTime.UtcNow.AddMinutes(120);
        JwtSecurityToken jwt = new(
             audience: _configuration["JWT:Audience"],
             issuer: _configuration["JWT:Issuer"],
             claims: claims,
             notBefore: DateTime.UtcNow,
             expires: ExpireDate,
             signingCredentials: Credentials
             );
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new TokenResponseDto(token, DateTime.Now);
    }

    public async Task register(RegisterDto registerDto)
    {
        AppUser user = new()
        {
            Fullname = registerDto.Fullname,
            UserName = registerDto.Username,
            Email = registerDto.email,
            IsActive = true
        };
        IdentityResult identityResult = await _userManager.CreateAsync(user, registerDto.password);
        if (!identityResult.Succeeded)
        {
            StringBuilder error = new();
            foreach (var identityError in identityResult.Errors)
            {
                error.AppendLine(identityError.Description);
            }
            throw new UserRegistrationException(error.ToString());
        }
        var result = await _userManager.AddToRoleAsync(user, Role.Member.ToString());
        if (!result.Succeeded)
        {
            StringBuilder error = new();
            foreach (var identityError in result.Errors)
            {
                error.AppendLine(identityError.Description);
            }
            throw new UserRegistrationException(error.ToString());
        }
    }
}
