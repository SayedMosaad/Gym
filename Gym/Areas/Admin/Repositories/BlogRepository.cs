using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class BlogRepository : IApplicationRepository<Blog>
    {
        private readonly ApplicationDBContext _db;

        public BlogRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Blog entity)
        {
            _db.Blogs.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var blog = Find(id);
            _db.Blogs.Remove(blog);
            _db.SaveChanges();
        }

        public Blog Find(int id)
        {
            return _db.Blogs.FirstOrDefault(x => x.ID == id);
        }

        public IList<Blog> List()
        {
            return _db.Blogs.ToList();
        }

        public void Update(Blog entity)
        {
            _db.Blogs.Update(entity);
            _db.SaveChanges();
        }
    }
}
