using pjModels;
using System;
using pjDataAccess;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace pjBusiness
{
    public class service
    {
        private ProjectDAL pdal;
        private JobDAL jdal;
        private DataDAL dal;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public service(DataContext db,UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            dal = new DataDAL(db,userManager, roleManager, configuration);
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            jdal = new JobDAL(db);
        
        }
        public service(DataContext _context)
        {
            pdal = new ProjectDAL(_context);
            jdal = new JobDAL(_context);
        }
        // Authenticate
        public async Task<dynamic> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return (new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return null;
        }
        public async Task<object> RegisterEmployee(RegisterModel model)
        {
            var z = await dal.RegisterEmployee(model);
            return z;
        }
        public async Task<object> RegisterManager(RegisterModel model)
        {
            var z = await dal.RegisterManager(model);
            return z;
        }

        // PROJECT Manager
        public ICollection<Project> GetallProject()
        {
            try
            {
                ICollection<Project> listp = pdal.getall();
                return listp;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public Project addProject(Project project)
        {
            try
            {
                pdal.add(project);
                Project newproject = pdal.get(project.ProjectId);
                if (newproject != null)
                {
                    return newproject;
                }
                return null;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public Project deleteproject(int id)
        {
            try
            {
                Project Iproject = pdal.get(id);
                if (Iproject != null)
                {
                    pdal.delete(Iproject);
                    return Iproject;
                }
                return null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
         public  Project updateproject(Project project)
        {
                 Project newproject = pdal.update(project);
                   return newproject;  
        }

        public Project GetByIdProject( int id)
        {
            try
            {
                Project project = pdal.get(id);
                if (project != null)
                {
                    return project;
                }
                return null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Job Manager

        public ICollection<Job> getallJob()
        {
            try
            {
                ICollection<Job> listjob = jdal.getall();
                return listjob;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public Job addJob(Job job)
        {
            try
            {
               jdal.add(job);
               Job  newJob = jdal.get(job.JobId);
                if (newJob != null)
                {
                    return newJob;
                }
                return null;
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Job getJob(int id)
        {
            try
            {
                Job job = jdal.get(id);
                return job;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public Job deleteJob(int id)
        {
            try
            {
                Job Ijob = jdal.get(id);
                if (Ijob != null)
                {
                    jdal.delete(Ijob);
                    return Ijob;
                }
                return null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Job updatejob(Job job)
        {
            try
            {
                //Job newjob = jdal.get(id);
                //if (newjob != null)
                //{
                //    jdal.update1(newjob,job);
                //    return job;
                //}
                //return null;
                Job newjob = jdal.update(job);
                return newjob;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public Job UpdateStatusJob(Job j) {
            return jdal.update(j);
        }
        public User UpdateAccount(User a)
        {
            User b = dal.Update(a);
            return b;
        }
        public IEnumerable<User> Get() {
            return dal.Get();
        }
        public User Get(string id) {
            return dal.Get(id);
        }
        public User Delete(string id) {
            return dal.Delete(id);
        
        }
        public List<Job> getjob(string userid)
        {
            try
            {
                List<Job> DS = new List<Job>();
                DS = jdal.getUserID(userid);
                return DS;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
 
}
