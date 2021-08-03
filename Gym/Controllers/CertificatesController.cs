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
    public class CertificatesController : Controller
    {
        private readonly IApplicationRepository<Biography> repository;
        private readonly IApplicationRepository<Certificate> certificaterepository;

        public CertificatesController(IApplicationRepository<Biography> repository,IApplicationRepository<Certificate> certificaterepository)
        {
            this.repository = repository;
            this.certificaterepository = certificaterepository;
        }
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies=repository.List().ToList(),
                Certificates=certificaterepository.List().ToList(),
            };
            return View(model);
        }

        public ActionResult GetCertificate(int id)
        {
            var certificate = from c in certificaterepository.List()
                              select c;
            
            var model = new IndexViewModel
            {
                Biographies = repository.List().ToList(),
                Certificate=certificate.Where(x=>x.ID==id).FirstOrDefault()
            };
            return View(model);
        }
    }
}
