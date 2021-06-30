using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class RequestRepository:IApplicationRepository<Request>
    {
        private readonly ApplicationDBContext _db;

        public RequestRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public void Add(Request entity)
        {
            _db.Requests.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Requests.Remove(Find(id));
            _db.SaveChanges();
        }

        public Request Find(int id)
        {
            return _db.Requests.FirstOrDefault(x => x.ID == id);
        }

        public IList<Request> List()
        {
            return _db.Requests.ToList();
        }

        public void Update(Request entity)
        {
            _db.Requests.Update(entity);
            _db.SaveChanges();
        }
    }
}
