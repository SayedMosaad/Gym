using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string photo { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
