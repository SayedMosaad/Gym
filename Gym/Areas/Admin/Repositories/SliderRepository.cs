using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Repositories
{
    public class SliderRepository : IApplicationRepository<Slider>
    {
        private readonly ApplicationDBContext _db;

        public SliderRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public void Add(Slider entity)
        {
            _db.Sliders.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var slider = Find(id);
            _db.Sliders.Remove(slider);
            _db.SaveChanges();
        }

        public Slider Find(int id)
        {
            var slider = _db.Sliders.FirstOrDefault(x => x.ID == id);
            return slider;
        }

        public IList<Slider> List()
        {
            return _db.Sliders.ToList();
        }

        public void Update(Slider entity)
        {
            _db.Sliders.Update(entity);
            _db.SaveChanges();
        }
    }
}
