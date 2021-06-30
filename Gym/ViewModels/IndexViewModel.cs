using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.ViewModels
{
    public class IndexViewModel
    {
        public List<Biography> Biographies { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
        public List<Certificate> Certificates { get; set; }
        public Certificate Certificate { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Group> Groups { get; set; }
        public Group Group { get; set; }
        public List<Image> Images { get; set; }


        [Required]
        [MinLength(4, ErrorMessage = "Please enter at least 4 chars")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
