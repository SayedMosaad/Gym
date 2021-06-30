using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.DTO
{
    public class SliderReadDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
    }
    public class SliderCreateDTO
    {
        [Required,MaxLength(250)]
        public string Title { get; set; }
        [Required, MaxLength(250)]
        public string Subtitle { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
    }
    public class SliderUpdateDTO:SliderCreateDTO
    {
        public int Id { get; set; }
    }
}
