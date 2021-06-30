using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Models
{
    public class Biography
    {
        public int ID { get; set; }

        [MaxLength(2500)]
        public string About { get; set; }
        public byte[] Image { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Mobile { get; set; }
        
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(150)]
        public string Facebook { get; set; }

        [MaxLength(150)]
        public string Twitter { get; set; }

        [MaxLength(150)]
        public string Instagram { get; set; }
    }
}
