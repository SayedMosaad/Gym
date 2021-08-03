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
    public class ImagesController : Controller
    {
        private readonly IApplicationRepository<Group> groupRepository;
        private readonly IApplicationRepository<Image> repository;
        private readonly IWebHostEnvironment hosting;
        private readonly IToastNotification toastNotification;

        public ImagesController(IApplicationRepository<Group> GroupRepository, IApplicationRepository<Image> repository,IWebHostEnvironment hosting,IToastNotification toastNotification)
        {
            groupRepository = GroupRepository;
            this.repository = repository;
            this.hosting = hosting;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            var groups = groupRepository.List();
            return View(groups);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var group = groupRepository.Find(id);
            if(group==null)
            {
                return NotFound();
            }
            //ViewData["GroupId"] = group.ID;
            var model = new CreateImagesViewModel
            {
                GroupId=group.ID
            };
            return View("AddImage",model);
        }

        [HttpPost]
        public ActionResult Create(CreateImagesViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                for (int i = 0; i < model.File.Count(); i++)
                {
                    string FileName = UploadFile(model.File[i]) ?? string.Empty;
                    var Groups = groupRepository.Find(model.GroupId);
                    var Image = new Image
                    {
                        photo = FileName,
                        Group = Groups
                    };
                    repository.Add(Image);
                }

                toastNotification.AddSuccessToastMessage("Images has been added");
                return RedirectToAction("Details","Groups",new {id=model.GroupId });
            }
            return View(model);

        }



        public ActionResult Delete(int id)
        {
            var image = repository.Find(id);
            if(image==null)
            {
                return NotFound();
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\images",image.photo);
            repository.Delete(id);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return Ok();
        }

        public IActionResult Upload()
        {
            var files = Request.Form.Files;
            var obj = new object { };
            foreach (var file in files)
            {
                string filedic = "/images/images";
                string filePath = Path.Combine(hosting.WebRootPath, filedic);
                if(!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var uniquefilename = Guid.NewGuid() + "_" + file.FileName;
                filePath = Path.Combine(filePath, uniquefilename);
                using FileStream fs = System.IO.File.Create(filePath);
                file.CopyTo(fs);
                obj = new { linl = "/images/images" + uniquefilename };
            }
            return Json(obj);
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                //string UniqueFileName = null;
                //foreach (IFormFile photo in files)
                //{
                //    string uploads = Path.Combine(hosting.WebRootPath, "images/images");
                //    string FileName = photo.FileName;
                //    UniqueFileName = Guid.NewGuid().ToString() + FileName;
                //    string fullpath = Path.Combine(uploads, UniqueFileName);
                //    photo.CopyTo(new FileStream(fullpath, FileMode.Create));
                //}
                string uploads = Path.Combine(hosting.WebRootPath, "images/images");
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
                string uploads = Path.Combine(hosting.WebRootPath, "images/images");
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
