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
    public class DoctorsController : Controller
    {
        private readonly IApplicationRepository<Doctor> doctorrepository;
        private readonly IApplicationRepository<Biography> repository;

        public DoctorsController(IApplicationRepository<Doctor> doctorrepository,IApplicationRepository<Biography> repository)
        {
            this.doctorrepository = doctorrepository;
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies=repository.List().ToList(),
                Doctors=doctorrepository.List().ToList()
            };
            return View(model);
        }
    }
}
