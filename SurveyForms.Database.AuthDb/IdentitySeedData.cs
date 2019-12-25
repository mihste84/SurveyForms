using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SurveyForms.Application.Exceptions;
using SurveyForms.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SurveyForms.Database.AuthDb
{
    public static class IdentitySeedData
    {
        public async static Task SeedDevUser(IServiceProvider provider)
        {
            var mngr = provider.GetRequiredService<UserManager<AppUser>>();

            var user = await mngr.FindByEmailAsync(DevUser.Username);
            if (user == null)
            {
                var newUser = new AppUser
                {
                    FirstName = DevUser.Firstname,
                    Email = DevUser.Username,
                    LastName = DevUser.Lastname,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PhoneNumber = DevUser.PhoneNumber,
                    UserName = DevUser.Username
                };

                var claims = new List<Claim> {
                    new Claim(ClaimKeys.FormAreaPermissions, "1"),
                    new Claim(ClaimKeys.FormAreaPermissions, "2")
                };

                var createResult = await mngr.CreateAsync(newUser);
                if (!createResult.Succeeded)
                    throw new IdentityException($"Failed to add user to database. " + GetAllErrorsAsString(createResult.Errors));
                var claimsResult = await mngr.AddClaimsAsync(newUser, claims);
                if (!claimsResult.Succeeded)
                {
                    var deleteResult = await mngr.DeleteAsync(newUser);
                    if (!deleteResult.Succeeded)
                        throw new IdentityException($"Failed to delete user. " + GetAllErrorsAsString(createResult.Errors));

                    throw new IdentityException($"Failed to add claims to user. " + GetAllErrorsAsString(createResult.Errors));
                }
            }
        }
        public async static Task SeedMasterAdmin(IServiceProvider provider)
        {
            var mngr = provider.GetRequiredService<UserManager<AppUser>>();

            var admin = await mngr.FindByEmailAsync(DevAdmin.Username);
            if (admin == null)
            {
                var newAdmin = new AppUser { 
                    FirstName = DevAdmin.Firstname,
                    Email = DevAdmin.Username,
                    LastName = DevAdmin.Lastname,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PhoneNumber = DevAdmin.PhoneNumber,
                    UserName = DevAdmin.Username
                };

                var claims = new List<Claim> {
                    new Claim(ClaimKeys.MasterAdmin, ClaimKeys.MasterAdmin)
                };

                var createResult = await mngr.CreateAsync(newAdmin);
                if (!createResult.Succeeded)
                    throw new IdentityException($"Failed to add master admin to database. " + GetAllErrorsAsString(createResult.Errors));
                var claimsResult =  await mngr.AddClaimsAsync(newAdmin, claims);
                if (!claimsResult.Succeeded)
                {
                    var deleteResult = await mngr.DeleteAsync(newAdmin);
                    if(!deleteResult.Succeeded) 
                        throw new IdentityException($"Failed to delete master admin. " + GetAllErrorsAsString(createResult.Errors));

                    throw new IdentityException($"Failed to add claims to master admin. " + GetAllErrorsAsString(createResult.Errors));
                }
            }
        }

        private static string GetAllErrorsAsString(IEnumerable<IdentityError> errors) => string.Join(' ', errors.Select(_ => _.Description));
    }
}
