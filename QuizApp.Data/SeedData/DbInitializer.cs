using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using QuizApp.Core;
using QuizApp.Models;

namespace QuizApp.Data;

public class DbInitializer
{
    public static void Seed(QuizAppDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager,
        string rolesJsonPath, string usersJsonPath, bool isNeedSeedData = false)
    {
        if (!isNeedSeedData)
        {
            return;
        }
        
        context.Database.EnsureCreated();

        string jsonRoles = File.ReadAllText(rolesJsonPath);
        var roles = JsonConvert.DeserializeObject<List<Role>>(jsonRoles);

        string jsonUsers = File.ReadAllText(usersJsonPath);
        var users = JsonConvert.DeserializeObject<List<UserJsonViewModel>>(jsonUsers);

        if (roles == null || users == null)
        {
            return;
        }

        SeedUserAndRoles(userManager, roleManager, users, roles);

        context.SaveChanges();
    }

    private static void SeedUserAndRoles(UserManager<User> userManager, RoleManager<Role> roleManager, List<UserJsonViewModel> users, List<Role> roles)
    {
        if (!userManager.Users.Any(x => x.UserName == "systemadministrator"))
        {
            if (users == null)
            {
                return;
            }

            var passwordHash = new PasswordHasher<User>();

            foreach (var user in users)
            {
                var newUser = new User
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.Parse(user.DateOfBirth).ToUniversalTime(),
                };

                string password = passwordHash.HashPassword(newUser, user.Password);
                newUser.PasswordHash = password;

                // check if system administrator exists
                var systemAdministrator = userManager.FindByNameAsync("systemadministrator").Result;
                if (systemAdministrator != null)
                {
                    newUser.CreatedById = systemAdministrator.Id;
                }

                var result = userManager.CreateAsync(newUser, user.Password).Result;

                if (result.Succeeded)
                {
                    var userRole = roleManager.FindByNameAsync(user.Role).Result;

                    if (userRole == null)
                    {
                        var newRole = roles.FirstOrDefault(x => x.Name == user.Role);
                        if (newRole == null)
                        {
                            continue;
                        }

                        if(systemAdministrator != null)
                        {
                            newRole.CreatedById = systemAdministrator.Id;
                        }
                        roleManager.CreateAsync(newRole).Wait();
                    }

                    var result2 = userManager.AddToRoleAsync(newUser, user.Role).Result;

                    if (!result2.Succeeded)
                    {
                        continue;
                    } 
                }
            }
        }
    }
}

internal class UserJsonViewModel
{
    public Guid Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string UserName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
    
    public required string PhoneNumber { get; set; }

    public required string DateOfBirth { get; set; }

    public required string Role { get; set; }
}