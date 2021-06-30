
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Models
{
    public class Doctor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public ICollection<Certificate> Certificates { get; set; }
    }
}
