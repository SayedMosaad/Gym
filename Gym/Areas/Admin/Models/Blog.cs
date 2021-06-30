using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Models
{
    public class Blog
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
    }
}
