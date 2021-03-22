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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        service svc;
        public UserController(DataContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration) {
            svc = new service(db,userManager,roleManager,configuration);
        }
        [HttpPut]
        [Route("UpdateAccount")]
        public IActionResult UpdateAccount([FromBody] User u) 
        {
            User a = svc.UpdateAccount(u);
            if (a != null) {
                return Ok(a);
            }
            return BadRequest(a);
        }
        [Authorize(Roles = UserRoles.Manager)]
        [HttpGet]
        public IEnumerable<User> Get() {
            return svc.Get();
        }
        [HttpGet]
        [Route("{id}")]
        public User Get(string id) {
            return svc.Get(id);
        }
        [Authorize(Roles = UserRoles.Manager)]
        [HttpDelete]
        [Route("{id}")]
        public User Remove(string id) {
            return svc.Delete(id);
        }
        

    }
}
