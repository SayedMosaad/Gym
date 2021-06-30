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
    public class AboutController : Controller
    {
        private readonly IApplicationRepository<Biography> repository;

        public AboutController(IApplicationRepository<Biography> repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies = repository.List().ToList()
            };
            return View(model);
        }
    }
}
