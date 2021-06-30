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
    public class CertificatesController : Controller
    {
        private readonly IApplicationRepository<Certificate> repository;
        private readonly IApplicationRepository<Doctor> doctorRepository;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public CertificatesController(IApplicationRepository<Certificate> repository,IApplicationRepository<Doctor> DoctorRepository,IMapper mapper,IToastNotification toastNotification)
        {
            this.repository = repository;
            doctorRepository = DoctorRepository;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View(repository.List());
        }
        List<Doctor> FillDoctors()
        {
            var doctors = doctorRepository.List().ToList();
            doctors.Insert(0, new Doctor { ID = -1, Name = "---- Please Select Doctor ----" });
            return doctors;
        }
        CertificateDTO GetAllDoctors()
        {
            var model = new CertificateDTO
            {
                Doctors = FillDoctors()
            };
            return model;
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View(GetAllDoctors());
        }

        [HttpPost]
        public ActionResult Create(CertificateDTO certificateDTO)
        {
            if(ModelState.IsValid)
            {
                if (certificateDTO.DoctorId == -1)
                {
                    ModelState.AddModelError("", "Please select the Doctor");
                    return View(GetAllDoctors());
                }
                var model=mapper.Map<Certificate>(certificateDTO);
                repository.Add(model);
                toastNotification.AddSuccessToastMessage("Certificate has been added");
                return RedirectToAction(nameof(Index));
            }
            certificateDTO.Doctors = FillDoctors();
            return View(certificateDTO);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var certificate = repository.Find(id);
            if(certificate==null)
            {
                return NotFound();
            }
            var model = mapper.Map<CertificateDTO>(certificate);
            model.DoctorId = certificate.DoctorId;
            model.Doctors = FillDoctors();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CertificateDTO certificateDTO)
        {
            if(ModelState.IsValid)
            {
                var certificate = repository.Find(certificateDTO.Id);
                if (certificate == null)
                {
                    return NotFound();
                }
                if (certificateDTO.DoctorId == -1)
                {
                    ModelState.AddModelError("", "Please select the Doctor");
                    certificateDTO.Doctors = FillDoctors();
                    return View(certificateDTO);
                }
                repository.Update(mapper.Map(certificateDTO, certificate));
                toastNotification.AddInfoToastMessage("Updated!");
                return RedirectToAction(nameof(Index));
                
            }
            certificateDTO.Doctors = FillDoctors();
            return View(certificateDTO);
        }

        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            toastNotification.AddWarningToastMessage("Certificate deleted!");
            return Ok();
        }
    }
}
