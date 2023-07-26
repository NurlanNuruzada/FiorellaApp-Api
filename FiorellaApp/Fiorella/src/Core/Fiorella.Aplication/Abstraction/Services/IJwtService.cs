using Fiorella.Domain.Entities;
using Fiorello.Application.DTOs.ResponseDTOs;

namespace Fiorello.Application.Abstraction.Services
{
    public interface IJwtService
    {
        TokenResponseDto CreateJwtToken(AppUser appUser);
    }
}
