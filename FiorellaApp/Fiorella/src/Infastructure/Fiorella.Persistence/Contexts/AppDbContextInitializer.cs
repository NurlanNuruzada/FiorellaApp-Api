using Fiorella.Domain.Entities;
using Fiorella.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Fiorella.Persistence.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AppDbContextInitializer(UserManager<AppUser> userManager,
                                       DbContext context,
                                       RoleManager<IdentityRole> roleManager,
                                       IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task Inititalizer()
        {
            await _context.Database.MigrateAsync();
        }
        public async Task RoleSeedAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Role)))
            {
            if(await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }

            }
        }
        public async Task UserSeedService()
        {
            AppUser appUser = new()
            {
                UserName = _configuration["SuperAdminSettings:username"],
                Email = _configuration["SuperAdminSettings:email"],
            };
            await _userManager.CreateAsync(appUser, _configuration["SuperAdminSettings:password"]);
            await _userManager.AddToRoleAsync(appUser, Role.SuperAdmin.ToString());
        }
    }
