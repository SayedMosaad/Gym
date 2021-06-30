using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.DTO
{
    public class BiographyReadDTO
    {
        public int ID { get; set; }
        public string About { get; set; }
        public byte[] Image { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
    }

    public class BiographyCreateDTO
    {
        [Required,MaxLength(2500)]
        public string About { get; set; }
        [Display(Name ="About Photo")]
        public byte[] Image { get; set; }

        [Required, MaxLength(250)]
        public string Address { get; set; }

        [Required, MaxLength(50)]
        public string Mobile { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string Facebook { get; set; }

        [MaxLength(150)]
        public string Twitter { get; set; }

        [MaxLength(150)]
        public string Instagram { get; set; }
    }

    public class BiographyUpdateDTO:BiographyCreateDTO
    {
        public int Id { get; set; }
        
    }
}
