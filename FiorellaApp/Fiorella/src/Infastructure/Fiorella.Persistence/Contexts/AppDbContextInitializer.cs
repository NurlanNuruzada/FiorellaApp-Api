﻿using Fiorella.Domain.Entities;
using Fiorella.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorella.Persistence.Contexts;

public class AppDbContextInitializer
{
	private readonly AppDbContext _context;	
	private readonly UserManager<AppUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IConfiguration _configuration;

	public AppDbContextInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
	{
		_context = context;
		_userManager = userManager;
		_roleManager = roleManager;
		_configuration = configuration;
	}


	public async Task InitializeAsync()
	{
		await _context.Database.MigrateAsync();
	}

	public async Task RoleSeedAsync()
	{
		foreach (var role in Enum.GetValues(typeof(Role)))
		{
			if (!await _roleManager.RoleExistsAsync(Role.Admin.ToString()))
			{
				await _roleManager.CreateAsync(new() { Name = Role.Admin.ToString() });
			}
		}
		
	}

	public async Task UserSeedAsync()
	{
		AppUser appUser = new()
		{
			UserName = _configuration["SuperAdminSettings:username"],
			Email = _configuration["SuperAdminSettings:email"]
		};

		await _userManager.CreateAsync(appUser,_configuration["SuperAdminSettings:username"]);
		await _userManager.AddToRoleAsync(appUser,Role.SuperAdmin.ToString());	
	}
}
