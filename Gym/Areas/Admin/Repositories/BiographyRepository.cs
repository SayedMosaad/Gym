using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class BiographyRepository : IApplicationRepository<Biography>
    {
        private readonly ApplicationDBContext _db;

        public BiographyRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Biography entity)
        {
            _db.Profiles.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var profile = Find(id);
            _db.Profiles.Remove(profile);
            _db.SaveChanges();
        }

        public Biography Find(int id)
        {
            var profile = _db.Profiles.SingleOrDefault(x => x.ID == id);
            return profile;
        }

        public IList<Biography> List()
        {
            IList<Biography> profiles = _db.Profiles.ToList();
            return profiles;
        }

        public void Update(Biography entity)
        {
            _db.Profiles.Update(entity);
            _db.SaveChanges();
        }
    }
}
