using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.ViewModels
{
    public class CreateDoctorViewModel
    {
        [Required]
        public string Name { get; set; }

        public IFormFile File { get; set; }

        [Required]
        [MinLength(50)]
        public string Bio { get; set; }
    }
    public class EditDoctorViewModel : CreateDoctorViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
