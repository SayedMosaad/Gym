using AutoMapper;
using Gym.Areas.Admin.DTO;
using Gym.Areas.Admin.Models;
using Gym.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles ="Admin")]
    public class BiographyController : Controller
    {
        private readonly IApplicationRepository<Biography> _repository;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public BiographyController(IApplicationRepository<Biography> repository, IMapper mapper,IToastNotification toastNotification)
        {
            _repository = repository;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            var biographies = _repository.List().ToList();
            return View(_mapper.Map<IEnumerable<BiographyReadDTO>>(biographies));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BiographyCreateDTO biography)
        {
            if (ModelState.IsValid)
            {
                var files = Request.Form.Files;
                if (!files.Any())
                {
                    ModelState.AddModelError("", "Please select an Image");
                    return View(biography);
                }
                var image = files["Image"];
                var allowedextention = new List<string> { ".jpg", ".jpeg", ".png" };
                if (!allowedextention.Contains(Path.GetExtension(image.FileName).ToLower()))
                {
                    ModelState.AddModelError("", "please select an allowed extenstion");
                    return View(biography);
                }
                using var datastream = new MemoryStream();
                image.CopyToAsync(datastream);

                var model = _mapper.Map<Biography>(biography);
                model.Image = datastream.ToArray();
                _repository.Add(model);

                _toastNotification.AddSuccessToastMessage("profile created successfuly");
                return RedirectToAction("index");

            }

            return View(biography);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var profile = _repository.Find(id);
            if (profile == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<BiographyUpdateDTO>(profile);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BiographyUpdateDTO biography)
        {
            if (ModelState.IsValid)
            {
                var profile = _repository.Find(biography.Id);
                if (profile == null)
                {
                    return NotFound();
                }
                var files = Request.Form.Files;
                if (files.Any())
                {
                    var image = files.FirstOrDefault();
                    using var datastream = new MemoryStream();
                    image.CopyTo(datastream);

                    var allowedextention = new List<string> { ".jpg", ".jpeg", ".png" };
                    if (!allowedextention.Contains(Path.GetExtension(image.FileName).ToLower()))
                    {
                        ModelState.AddModelError("", "please select an allowed extenstion");
                        return View(biography);
                    }

                    biography.Image = datastream.ToArray();
                }
                _mapper.Map(biography, profile);
                _repository.Update(profile);

                _toastNotification.AddInfoToastMessage("your data has been updated!");
                return RedirectToAction("index");
            }
            return View(biography);
        }

    }
}
