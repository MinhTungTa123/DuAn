using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pjModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace pjDataAccess
{
    public class DataDAL
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly DataContext data;
        public DataDAL(DataContext db,UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            data = db;
        }
        public async Task<dynamic> RegisterEmployee(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Status = false
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return (new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return (new Response { Status = "Success", Message = "User created successfully!" });
        }
        public async Task<dynamic> RegisterManager(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Status = true
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return (new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));

            if (await roleManager.RoleExistsAsync(UserRoles.Manager))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Manager);
            }

            return (new Response { Status = "Success", Message = "User created successfully!" });
        }
        public IEnumerable<User> Get() {
            return data.Users.ToList();
        }
        public User Get(string id) {
            User a = data.Users.Find(id);
            return a;
        }
        public User Delete(string id) {
            User a = data.Users.Find(id);
            if (a != null) {
                User b = a;
                data.Users.Remove(a);
                data.SaveChanges();
                return b;
            }
            return null;
        }
        public User Update(User a) {
            User old = data.Users.Find(a.Id);
            if (old != null) {
                old.Id = a.Id;
                old.Status = a.Status;
                old.UserName = a.UserName;
                old.Email = a.Email;
                old.PhoneNumber = a.PhoneNumber;
                data.SaveChanges();
                return a;
            }
            return null;
        
        }
 

    }
}

