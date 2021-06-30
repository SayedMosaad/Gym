using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class ImagesRepository : IApplicationRepository<Image>
    {
        private readonly ApplicationDBContext _db;

        public ImagesRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Image entity)
        {
            _db.Images.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var image = Find(id);
            _db.Images.Remove(image);
            _db.SaveChanges();
        }

        public Image Find(int id)
        {
            return _db.Images.FirstOrDefault(x => x.ID == id);
        }

        public IList<Image> List()
        {
            return _db.Images.ToList();
        }

        public void Update(Image entity)
        {
            _db.Images.Update(entity);
            _db.SaveChanges();
        }
    }
}
