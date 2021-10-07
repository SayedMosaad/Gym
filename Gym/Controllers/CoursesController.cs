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
    public class CoursesController : Controller
    {
        private readonly IApplicationRepository<Course> _courseRepository;
        private readonly IApplicationRepository<Biography> _profileRepository;

        public CoursesController(IApplicationRepository<Course> courseRepository,IApplicationRepository<Biography> profileRepository)
        {
            _courseRepository = courseRepository;
            _profileRepository = profileRepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies=_profileRepository.List().ToList(),
                Courses=_courseRepository.List().ToList()
            };
            return View(model);
        }
    }
}
