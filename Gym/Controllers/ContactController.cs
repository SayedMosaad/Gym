using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gym.Areas.Admin.Repositories;
using Gym.Areas.Admin.Models;
using Gym.ViewModels;
using NToastNotify;

namespace Gym.Controllers
{
    public class ContactController : Controller
    {
        private readonly IApplicationRepository<Biography> repository;
        private readonly IApplicationRepository<Request> requestrepository;
        private readonly IToastNotification toastNotification;

        public ContactController(IApplicationRepository<Biography> repository,IApplicationRepository<Request> requestrepository,IToastNotification toastNotification)
        {
            this.repository = repository;
            this.requestrepository = requestrepository;
            this.toastNotification = toastNotification;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel
            {
                Biographies=repository.List().ToList()
            };
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            model.Biographies = repository.List().ToList();
            if (ModelState.IsValid)
            {
                var request = new Request
                {
                    Name=model.Name,
                    Email=model.Email,
                    phone=model.Phone,
                    Message=model.Message
                };
                requestrepository.Add(request);
                toastNotification.AddSuccessToastMessage("Your Message has been sent");
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
