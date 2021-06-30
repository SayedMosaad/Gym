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
    public class HomeController : Controller
    {
        private readonly IApplicationRepository<Biography> repository;
        private readonly IApplicationRepository<Slider> sliderrepository;
        private readonly IApplicationRepository<Blog> blogrepository;

        public HomeController(IApplicationRepository<Biography> repository, IApplicationRepository<Slider> sliderrepository,
                                IApplicationRepository<Blog> blogrepository)
        {
            this.repository = repository;
            this.sliderrepository = sliderrepository;
            this.blogrepository = blogrepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies = repository.List().ToList(),
                Sliders = sliderrepository.List().ToList(),
                Blogs=blogrepository.List().ToList()
            };
            return View(model);
        }
    }
}
