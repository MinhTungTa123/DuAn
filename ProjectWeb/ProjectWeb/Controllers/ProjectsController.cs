using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pjBusiness;
using pjDataAccess;
using pjModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectWeb.Controllers
{   
    [Authorize(Roles =UserRoles.Manager)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {


        service Service;
        public ProjectsController(DataContext _context)
        {
            Service = new service(_context);
        }
        // GET: api/<ProjectsController>
        [HttpGet]
        public IActionResult  Get()
        {
            try
            {
                return Ok(Service.GetallProject());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<ProjectsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Project project = Service.GetByIdProject(id);
                if (project != null)
                {
                    return Ok(project);
                }
                return BadRequest("Not Find Id");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // POST api/<ProjectsController>
        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            try
            {
                Project newproject = Service.addProject(project);
                if (newproject != null)
                {
                    return Ok(newproject);
                }
                return BadRequest("Add false");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT api/<ProjectsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Project project)
        {
            try
            {
                  Project newproject = Service.updateproject(project);
                  if (newproject != null)
                  {
                        return Ok(newproject);
                  }
                
                return BadRequest("Not Find Id");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // DELETE api/<ProjectsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Project project = Service.deleteproject(id);
                if (project != null)
                {
                    return Ok(project);
                }
                return BadRequest("Not Find Id");

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
