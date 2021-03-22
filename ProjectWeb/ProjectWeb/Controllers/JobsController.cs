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
    //[Authorize(Roles = UserRoles.Manager)]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        service Service;
        public JobsController(DataContext _context)
        {
            Service = new service(_context);
        }
        // GET: api/<JobsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ICollection<Job> listjob = Service.getallJob();
                return Ok(listjob);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Job job = Service.getJob(id);
                if (job != null){
                    return Ok(job);
                }
                return BadRequest("Not Find Id");
                
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // POST api/<JobsController>
        [HttpPost]
        public IActionResult Post([FromBody] Job job)
        {
            try
            {
                Job newjob =Service.addJob(job);
                if (newjob != null)
                {
                    return Ok(newjob);
                }
                return BadRequest("Add false");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // PUT api/<JobsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Job job)
        {
            try
            {
                Job newjob = Service.updatejob(job);
                if (newjob != null)
                {
                    return Ok(newjob);
                }
                return BadRequest("Not Find Id");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
               Job job = Service.deleteJob(id);
                if (job != null)
                {
                    return Ok(job);
                }
                return BadRequest("Delete is Fail");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("UpdateStatusJob")]
        public IActionResult UpdateStatusJob([FromBody] Job u)
        {
            Job a = Service.UpdateStatusJob(u);
            if (a != null)
            {
                return Ok(a);
            }
            return BadRequest(a);
        }

        [HttpGet]
        [Route("Job/{id}")]
        public IActionResult GetJob(string id)
        {
            try
            {
                List<Job> DS = new List<Job>();
                DS = Service.getjob(id);
                return Ok(DS);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
