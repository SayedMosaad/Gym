using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Models
{
    public class Request
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string Message { get; set; }
    }
}
