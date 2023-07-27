
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.ResponseDTOs;
using Fiorella.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fiorella.Infrastucture.Services.Token;
public class TokenHandler : ITokenHandler
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public TokenHandler(UserManager<AppUser> userManager,
                       IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<TokenResponseDto> CreateAccessToken(int addminutes,int refreshTokenMinutes, AppUser user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(user);
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
        var refreshToken = GenerateRefreshToken();
        return new TokenResponseDto(token, ExpireDate, DateTime.UtcNow.AddMinutes(refreshTokenMinutes), refreshToken);
    }

    private string GenerateRefreshToken()
    {
        byte[] bytes =new byte[64];
        var randomNumber = RandomNumberGenerator.Create();
        randomNumber.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
