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
    private readonly ITokenHandler _tokekHandler;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager,
                       RoleManager<IdentityRole> roleManager,
                       IConfiguration configuration,
                       ITokenHandler tokekHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _tokekHandler = tokekHandler;
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
       
        return await _tokekHandler.CreateAccessToken(120,AppUser);
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
