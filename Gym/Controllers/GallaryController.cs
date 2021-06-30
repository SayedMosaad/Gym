using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.Areas.Admin.Repositories;
using Gym.Areas.Admin.Models;
using Gym.ViewModels;
namespace Gym.Controllers
{
    public class GallaryController : Controller
    {
        private readonly IApplicationRepository<Biography> repository;
        private readonly IApplicationRepository<Group> grouprepository;

        public GallaryController(IApplicationRepository<Biography> repository,IApplicationRepository<Group> grouprepository)
        {
            this.repository = repository;
            this.grouprepository = grouprepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies=repository.List().ToList(),
                Groups=grouprepository.List().ToList()
            };
            return View(model);
        }

        public ActionResult Photos(int id)
        {
            var group = grouprepository.Find(id);
            if(group==null)
            {
                return NotFound();
            }
            var model = new IndexViewModel
            {
                Biographies = repository.List().ToList(),
                Group=group
            };
            return View(model);
        }
    }
}
