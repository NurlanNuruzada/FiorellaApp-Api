using Fiorella.Aplication.DTOs.AuthDTOs;
using Fiorella.Aplication.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Identity;

namespace Fiorella.Aplication.Abstraction.Services;
public interface IAuthService
{
    Task register(RegisterDto registerDto);
    Task<TokenResponseDto> Login(SingInDto loginDto);
}
