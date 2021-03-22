using pjModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pjDataAccess
{
    public class ProjectDAL : IFunctional<Project>
    {
        private readonly DataContext _context;
        public ProjectDAL(DataContext context)
        {
            _context = context;
        }

        public Project add(Project Tmodel)
        {
            try
            {
                _context.Projects.Add(Tmodel);
                _context.SaveChanges();
                return Tmodel;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Project delete(Project Tmodel)
        {
            try
            {
               _context.Projects.Remove(Tmodel);
                _context.SaveChanges();
                return Tmodel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Project get(int id)
        {
            try
            {
                Project project = _context.Projects.FirstOrDefault(e => e.ProjectId == id);
                return project;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ICollection<Project> getall()
        {
            try
            {
                List<Project> listproject = _context.Projects.ToList();
                return listproject;
            }
            catch (Exception e)
            {
                throw e;
            }
          
        }

        public Project update(Project Tmodel)
        {

            _context.Entry(Tmodel).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return Tmodel;
        }

        public void update1(Project t1, Project t2)
        {
            throw new NotImplementedException();
        }
    }
}
