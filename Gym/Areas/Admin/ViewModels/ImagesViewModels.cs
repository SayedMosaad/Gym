using Gym.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.ViewModels
{
    public class CreateImagesViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public List<IFormFile> File { get; set; }
        [Display(Name = "Project")]
        public int GroupId { get; set; }
        public List<Group> Groups { get; set; }
    }

    public class EditImagesViewModel : CreateImagesViewModel
    {

    }
}
