using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class DoctorRepository : IApplicationRepository<Doctor>
    {
        private readonly ApplicationDBContext _db;

        public DoctorRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Doctor entity)
        {
            _db.Doctors.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = Find(id);
            _db.Doctors.Remove(doctor);
            _db.SaveChanges();
        }

        public Doctor Find(int id)
        {
            var doctor = _db.Doctors.FirstOrDefault(x => x.ID==id);
            return doctor;
        }

        public IList<Doctor> List()
        {
            return _db.Doctors.ToList();
        }

        public void Update(Doctor entity)
        {
            _db.Doctors.Update(entity);
            _db.SaveChanges();
        }
    }
}
