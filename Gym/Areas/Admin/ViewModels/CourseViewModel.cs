using Gym.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.ViewModels
{
    public class CourseViewModel
    {

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile File { get; set; }
        [Required]
        [Display(Name ="Doctor")]
        public int DoctorId { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
    
}
