using Gym.Areas.Admin.Models;
using Gym.Areas.Admin.Repositories;
using Gym.Areas.Admin.ViewModels;
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
    public class CoursesController : Controller
    {
        private readonly IApplicationRepository<Course> _Repository;
        private readonly IWebHostEnvironment hosting;
        private readonly IApplicationRepository<Doctor> DoctorRepository;
        private readonly IToastNotification toastNotification;

        public CoursesController(IApplicationRepository<Course> Repository, IWebHostEnvironment hosting, IApplicationRepository<Doctor> DoctorRepository,
            IToastNotification toastNotification)
        {
            _Repository = Repository;
            this.hosting = hosting;
            this.DoctorRepository = DoctorRepository;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            var courses = _Repository.List();
            return View(courses);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CourseViewModel
            {
                //Doctors= FillDoctors()
                Doctors=DoctorRepository.List().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CourseViewModel model)
        {
            string fileName = UploadFile(model.File) ?? string.Empty;
            if(ModelState.IsValid)
            {
                var course = new Course
                {
                    Title=model.Title,
                    Description=model.Description,
                    Image=fileName,
                    DoctorId=model.DoctorId
                };
                _Repository.Add(course);
                toastNotification.AddSuccessToastMessage("Course has been added");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return BadRequest();
            }
            var course = _Repository.Find((int)id);
            if(course==null)
            {
                return NotFound();
            }
            var model = new CourseViewModel
            {
                Id=course.ID,
                Title=course.Title,
                Description=course.Description,
                ImageUrl=course.Image,
                DoctorId=course.DoctorId,
                Doctors=DoctorRepository.List().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel model)
        {
            string filename;
            if(model.ImageUrl!=null)
            {
                filename = UploadFile(model.File, model.ImageUrl);
            }
            else
            {
                filename = UploadFile(model.File);
            }
            
            if(ModelState.IsValid)
            {
                var doctor = DoctorRepository.Find(model.DoctorId);
                if(model.DoctorId==-1)
                {
                    ModelState.AddModelError("", "Please Select Doctor");
                    return View(GetAllDoctors());
                }
                var course = _Repository.Find(model.Id);
                course.Title = model.Title;
                course.Description = model.Description;
                course.Image = filename;
                course.DoctorId = model.DoctorId;

                _Repository.Update(course);
                toastNotification.AddSuccessToastMessage("Updated!");
                return RedirectToAction(nameof(Index));
            }
            model.Doctors = FillDoctors();
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var course = _Repository.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            _Repository.Delete(id);
            return Ok();
        }


        List<Doctor> FillDoctors()
        {
            var doctors = DoctorRepository.List().ToList();
            doctors.Insert(0, new Doctor { ID = -1, Name = "---- Select Doctor ----" });
            return doctors;
        }
        CourseViewModel GetAllDoctors()
        {
            CourseViewModel model = new CourseViewModel
            {
                Doctors = FillDoctors()
            };
            return model;
        }


        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images/courses");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/courses");
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
