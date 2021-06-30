using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class GroupRepository : IApplicationRepository<Group>
    {
        private readonly ApplicationDBContext _db;

        public GroupRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Group entity)
        {
            _db.Groups.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var group = Find(id);
            _db.Groups.Remove(group);
            _db.SaveChanges();
        }

        public Group Find(int id)
        {
            return _db.Groups.Include(i=>i.Images).FirstOrDefault(x => x.ID == id);
        }

        public IList<Group> List()
        {
            return _db.Groups.Include(i => i.Images).ToList();
        }

        public void Update(Group entity)
        {
            _db.Groups.Update(entity);
            _db.SaveChanges();

        }
    }
}
