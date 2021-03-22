using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using pjBusiness;
using pjDataAccess;
using pjModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeb.Controllers
{
    // [Authorize(Roles = UserRoles.Manager)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private service services;
        public AuthenticateController(DataContext db,UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            services = new service(db,userManager, roleManager, configuration);
        }
        //  [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var x = services.Login(login).Result;
            if (x != null)
            {
                return Ok(x);
            }
            return BadRequest();
            
        }
        // [AllowAnonymous]
        [HttpPost]
        [Route("RegisterEmployee")]
        public IActionResult RegisterEmployee([FromBody] RegisterModel model)
        {
            var x = (Response)services.RegisterEmployee(model).Result;
            if (x.Status == "Error")
                return BadRequest(x);
            return Ok(x);
        }
        //[AllowAnonymous]
        [HttpPost]
        [Route("RegisterManager")]
        public IActionResult RegisterManager([FromBody] RegisterModel model)
        {
            var x = (Response)services.RegisterManager(model).Result;
            return Ok(x);
        }
    }
}
