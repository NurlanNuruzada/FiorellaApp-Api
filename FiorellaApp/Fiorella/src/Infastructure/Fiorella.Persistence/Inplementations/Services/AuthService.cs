using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.AuthDTOs;
using Fiorella.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text;
using UnionArchitecture.Persistence.Exceptions;

namespace Fiorella.Persistence.Inplementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> userManager;
    private object identityResult;
    private object _userManager;

    public AuthService(UserManager<AppUser> userManager)
    {
        this.userManager = userManager;
    }

    public object identityresult { get; private set; }

    public async Task Register(RegisterDto registerDto)
    {

        AppUser appUser =new()
        {
            Fullname = registerDto.Fullname,
            UserName = registerDto.username,
            Email = registerDto.email,
            IsActive = true
        };
        IdentityResult identityUser= await userManager.CreateAsync(appUser,registerDto.password);
        if (!identityUser.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in identityUser.Errors)
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }
    }
}
