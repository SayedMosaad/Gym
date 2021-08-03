using Gym.Areas.Admin.Models;
using Gym.Areas.Admin.Repositories;
using Gym.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IApplicationRepository<Biography> repository;
        private readonly IApplicationRepository<Blog> blogrepository;

        public BlogsController(IApplicationRepository<Biography> repository, IApplicationRepository<Blog> blogrepository)
        {
            this.repository = repository;
            this.blogrepository = blogrepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies=repository.List().ToList(),
                Blogs=blogrepository.List().ToList()
            };
            return View(model);
        }

        public ActionResult blogdetails(int id)
        {
            var model = new IndexViewModel
            {
                Biographies=repository.List().ToList(),
                Blog=blogrepository.Find(id)
            };
            return View(model);
        }
    }
}
