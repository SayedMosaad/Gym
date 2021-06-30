using AutoMapper;
using Gym.Areas.Admin.Data;
using Gym.Areas.Admin.DTO;
using Gym.Areas.Admin.Models;
using Gym.Areas.Admin.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SlidersController : Controller
    {
        
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IApplicationRepository<Slider> _repository;

        public SlidersController(IApplicationRepository<Slider> repository,IMapper mapper, IToastNotification toastNotification)
        {

            _mapper = mapper;
            _toastNotification = toastNotification;
            _repository = repository;
        }
        public IActionResult Index()
        {
            var sliders = _repository.List().ToList();
            var model =_mapper.Map<IEnumerable<SliderReadDTO>>(sliders);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SliderCreateDTO objectDTO)
        {
            if(ModelState.IsValid)
            {
                var model = _mapper.Map<Slider>(objectDTO);
                _repository.Add(model);
                _toastNotification.AddSuccessToastMessage("Data has been added successfuly");
                return RedirectToAction("Index");
            }
            return View(objectDTO);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var slider = _repository.Find(id);
            if(slider==null)
            {
                return NotFound();
            }
            var model = _mapper.Map<SliderUpdateDTO>(slider);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SliderUpdateDTO updateDTO)
        {
            if(ModelState.IsValid)
            {
                var slider = _mapper.Map<Slider>(updateDTO);
                _repository.Update(slider);
                _toastNotification.AddSuccessToastMessage("Slider updated!");
                return RedirectToAction("Index");
            }
            return View(updateDTO);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();

        }
    }
}
