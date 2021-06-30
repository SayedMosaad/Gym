using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.DTO
{
    public class CertificateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Grade { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [Required]
        public int Hours { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
