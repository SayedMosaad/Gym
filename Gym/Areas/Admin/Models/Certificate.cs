using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Models
{
    public class Certificate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
