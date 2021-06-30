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
    public class BlogsController : Controller
    {
        private readonly IApplicationRepository<Blog> repository;
        private readonly IToastNotification toastNotification;
        private readonly IWebHostEnvironment hosting;

        public BlogsController(IApplicationRepository<Blog> repository,IToastNotification toastNotification, IWebHostEnvironment hosting)
        {
            this.repository = repository;
            this.toastNotification = toastNotification;
            this.hosting = hosting;
        }
        public IActionResult Index()
        {
            return View(repository.List());
        }

        public ActionResult Details(int id)
        {
            var blog = repository.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(CreateBlogsViewModel model)
        {
    
                string FileName = UploadFile(model.File) ?? string.Empty;
                if (ModelState.IsValid)
                {
                    var blog = new Blog
                    {
                        Title = model.Title,
                        Image = FileName,
                        Description = model.Description,
                        Body = model.Body
                    };
                    repository.Add(blog);
                    toastNotification.AddSuccessToastMessage("Blog has been added successfuly");
                    return RedirectToAction("index");
                }
                return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blog = repository.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            var model = new EditBlogsViewModel
            {
                Id = blog.ID,
                Title = blog.Title,
                ImageUrl = blog.Image,
                Description = blog.Description,
                Body = blog.Body
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(int id, EditBlogsViewModel model)
        {
            string FileName = UploadFile(model.File, model.ImageUrl);
            if (ModelState.IsValid)
            {
                var blog = repository.Find(id);
                blog.Title = model.Title;
                blog.Image = FileName;
                blog.Description = model.Description;
                blog.Body = model.Body;
                repository.Update(blog);
                toastNotification.AddInfoToastMessage("Updated!");
                return RedirectToAction("index");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var blog = repository.Find(id);
            if (blog == null)
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/blogs");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/blogs");
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
