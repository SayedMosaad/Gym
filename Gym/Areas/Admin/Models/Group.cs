using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
