using AutoMapper;
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
    public class GroupsController : Controller
    {
        private readonly IApplicationRepository<Group> repository;
        private readonly IToastNotification toastNotification;
        private readonly IMapper mapper;

        public GroupsController(IApplicationRepository<Group> repository, IToastNotification toastNotification,IMapper mapper)
        {
            this.repository = repository;
            this.toastNotification = toastNotification;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(repository.List());
        }

        public ActionResult Details(int id)
        {
            var group = repository.Find(id);
            if(group==null)
            {
                return NotFound();
            }
            return View(group);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(GroupDTO groupDTO)
        {
            if(ModelState.IsValid)
            {

                repository.Add(mapper.Map<Group>(groupDTO));
                toastNotification.AddSuccessToastMessage("Group has been added");
                return RedirectToAction(nameof(Index));
            }
            return View(groupDTO);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = repository.Find(id);
            if(model==null)
            {
                return NotFound();
            }
            return View(mapper.Map<GroupDTO>(model));
        }

        [HttpPost]
        public ActionResult Edit(GroupDTO groupDTO)
        {
            if(ModelState.IsValid)
            {
                repository.Update(mapper.Map<Group>(groupDTO));
                toastNotification.AddInfoToastMessage("Updated!");
                return RedirectToAction(nameof(Index));
            }
            return View(groupDTO);
        }

        public ActionResult Delete(int id)
        {
            var group = repository.Find(id);
            if(group==null)
            {
                return NotFound();
            }
            repository.Delete(id);
            return Ok();
        }
    }
}
