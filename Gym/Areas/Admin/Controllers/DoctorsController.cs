using Gym.Areas.Admin.Models;
using Gym.Areas.Admin.Repositories;
using Gym.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DoctorsController : Controller
    {
        private readonly IApplicationRepository<Doctor> repository;
        private readonly IWebHostEnvironment hosting;
        private readonly IToastNotification toastNotification;

        public DoctorsController(IApplicationRepository<Doctor> repository, IWebHostEnvironment hosting, IToastNotification toastNotification)
        {
            this.repository = repository;
            this.hosting = hosting;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View(repository.List());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDoctorViewModel model)
        {
                string FileName = UploadFile(model.File) ?? string.Empty;
                if (ModelState.IsValid)
                {
                    var doctor = new Doctor
                    {
                        Name = model.Name,
                        Image = FileName,
                        Bio = model.Bio
                    };
                    repository.Add(doctor);
                    toastNotification.AddSuccessToastMessage("Doctor has been added!");
                    return RedirectToAction("index");
                }
                return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var doctor = repository.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var model = new EditDoctorViewModel
            {
                Id = doctor.ID,
                Name=doctor.Name,
                ImageUrl=doctor.Image,
                Bio=doctor.Bio
            };

            return View(model);
        }

        [HttpPost]

        public ActionResult Edit(int id, EditDoctorViewModel model)
        {
            var doctor = repository.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            string FileName = UploadFile(model.File, model.ImageUrl);
            if (ModelState.IsValid)
            {
                doctor.Image = FileName;
                doctor.Name = model.Name;
                doctor.Bio = model.Bio;
                repository.Update(doctor);
                toastNotification.AddInfoToastMessage("Doctor Updated!");
                return RedirectToAction("index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var doctor = repository.Find(id);
            if(doctor==null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok();
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images/doctors");
                string FileName = file.FileName;
                string UniqueFileName = Guid.NewGuid().ToString() + FileName;
                string fullpath = Path.Combine(uploads, UniqueFileName);
                file.CopyTo(new FileStream(fullpath, FileMode.Create));
                return UniqueFileName;
            }
            return null;
        }

        string UploadFile(IFormFile file, string ImageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images/doctors");
                string FileName = file.FileName;
                string UniqueFileName = Guid.NewGuid().ToString() + FileName;
                string newpath = Path.Combine(uploads, UniqueFileName);
                string OldFileName = ImageUrl;
                string oldpath = Path.Combine(uploads, OldFileName);
                if (newpath != oldpath)
                {
                    System.IO.File.Delete(oldpath);
                    file.CopyTo(new FileStream(newpath, FileMode.Create));
                }
                return UniqueFileName;
            }
            return ImageUrl;
        }
    }
}
