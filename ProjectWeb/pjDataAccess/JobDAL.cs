using Microsoft.EntityFrameworkCore;
using pjModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pjDataAccess
{
    public class JobDAL : IFunctional<Job>
    {
        private readonly DataContext _context;
        public JobDAL(DataContext context)
        {
            _context = context;
        }

        public Job add(Job Tmodel)
        {
            try
            {
                _context.Jobs.Add(Tmodel);
                _context.SaveChanges();
                return Tmodel;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Job delete(Job Tmodel)
        {

            try
            {
                _context.Jobs.Remove(Tmodel);
                _context.SaveChanges();
                return Tmodel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Job get(int id)
        {
            try
            {
                Job job = _context.Jobs.FirstOrDefault(e => e.JobId==id);
                return job;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ICollection<Job> getall()
        {
            try
            {
                List<Job> listjob = _context.Jobs.ToList();
                return listjob;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Job update(Job Tmodel)
        {
            _context.Entry(Tmodel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Tmodel;
        }

        public void update1(Job t1, Job t2)
        {
            try
            {
                t1.JobName = t2.JobName;
                t1.ProjectId = t2.ProjectId;
                t1.Status = t2.Status;

                _context.SaveChanges();
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public List<Job> getUserID(string id)
        {
            try
            {
                string sql = "select JobId,ProjectId,JobName,Status,JobDate from dbo.JobUser inner join dbo.AspNetUsers on dbo.AspNetUsers.Id =dbo.JobUser.UserId where dbo.AspNetUsers.Id = '" + id+"'";
                //  var listjob = _context.JobUser 

                List<Job> ds = new List<Job>();
                
             
                
                return ds;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
