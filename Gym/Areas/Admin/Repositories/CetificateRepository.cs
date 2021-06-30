using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class CetificateRepository : IApplicationRepository<Certificate>
    {
        private readonly ApplicationDBContext _db;

        public CetificateRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Certificate entity)
        {
            _db.Certificates.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Certificates.Remove(Find(id));
            _db.SaveChanges();
        }

        public Certificate Find(int id)
        {
            return _db.Certificates.FirstOrDefault(x => x.ID == id);
        }

        public IList<Certificate> List()
        {
            return _db.Certificates.Include(x=>x.Doctor).ToList();
        }

        public void Update(Certificate entity)
        {
            _db.Certificates.Update(entity);
            _db.SaveChanges();
        }
    }
}
