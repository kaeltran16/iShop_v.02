using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace iShop.Repo.Helpers
{
    // This class will responsible for seeding data in the database
    public class AppInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AppInitializer(IServiceProvider services, ApplicationDbContext context)
        {
            _roleManager = services.GetService<RoleManager<ApplicationRole>>();
            _userManager = services.GetService<UserManager<ApplicationUser>>();
            _context = context;
        }

        private async Task SeedRoles()
        {
            var roles = new List<ApplicationRole>()
            {
                //new ApplicationRole {Name = ApplicationConstants.RoleName.SuperUser, Description = "Full permission"},
                //new ApplicationRole {Name = ApplicationConstants.RoleName.User, Description = "Limited permission"}
            };

            foreach (var role in roles)
            {
                if (!_roleManager.RoleExistsAsync(role.Name).Result)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }

        //private void SeedSuperUser()
        //{
        ////    if (!Queryable.Any(_context.Users))
        //    {
        //        var adminInfo = Startup.Configuration.GetSection("SuperUserInfo");
        //        var admin = new ApplicationUser
        //        {
        //            UserName = adminInfo.GetSection("UserName").Value,
        //            FirstName = "Khang",
        //            LastName = "Tran",
        //            Email = adminInfo.GetSection("Email").Value,
        //        };


        //        var createResult = _userManager
        //            .CreateAsync(admin, adminInfo.GetSection("Password").Value).Result;

        //        var user = _userManager.FindByNameAsync(adminInfo.GetSection("UserName").Value).GetAwaiter()
        //            .GetResult();

        //        var addRoleResult = _userManager
        //            .AddToRoleAsync(user, ApplicationConstants.RoleName.SuperUser)
        //            .Result;
        //        var addClaimResult =
        //            _userManager.AddClaimAsync(user, new Claim(ApplicationConstants.ClaimName.SuperUser, "true")).Result;


        //        if (createResult.Succeeded && addRoleResult.Succeeded && addClaimResult.Succeeded)
        //            _logger.Info("Super User is created successfully");
        //        else
        //            _logger.Error("createResult: " + createResult.ToString() + " | roleResult: " + addRoleResult +
        //                          " | claimResult: " + addClaimResult);

        //    }
        //}

        public async Task Seed()
        {
            await SeedRoles();
            //SeedSuperUser();
            //SAMPLE
        }
    }
}
