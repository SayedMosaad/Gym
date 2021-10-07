using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class CoursesRepository : IApplicationRepository<Course>
    {
        private readonly ApplicationDBContext _db;
        public CoursesRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Course entity)
        {
            _db.Courses.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var Course = Find(id);
            _db.Courses.Remove(Course);
            _db.SaveChanges();
        }

        public Course Find(int id)
        {
            return _db.Courses.Include(x=>x.Doctors).FirstOrDefault(x => x.ID == id);
        }

        public IList<Course> List()
        {
            return _db.Courses.Include(x=>x.Doctors).ToList();
        }

        public void Update(Course entity)
        {
            _db.Courses.Update(entity);
            _db.SaveChanges();
        }
    }
}
