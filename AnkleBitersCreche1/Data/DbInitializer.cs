﻿using AnkleBitersCreche1.Models;
using AnkleBitersCreche1.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
//creating database seed with Admin user account
namespace AnkleBitersCreche1.Data
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;



        }

        public async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            //check if any Admin roles exist in datbase. do not execute further if admin exists.
            if (_db.Roles.Any(r => r.Name == SD.AdminEndUser)) return;
            //If roles do not exist then create them.
            _roleManager.CreateAsync(new IdentityRole(SD.AdminEndUser)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.CustomerEndUser)).GetAwaiter().GetResult();

            //Create admin user
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Admin1@gmail.com",
                Email = "Admin1@gmail.com",
                Name = "Admin User",
                EmailConfirmed = true,
                PhoneNumber = "0881234567"
            }, "Pass123!").GetAwaiter().GetResult();




            //assign Admin role to acc

            _userManager.AddToRoleAsync(_db.Users.FirstOrDefaultAsync(u => u.Email == "Admin1@gmail.com").GetAwaiter().GetResult(), SD.AdminEndUser).GetAwaiter().GetResult();
        }
    }
}