using AutoMapper;
using Gym.Areas.Admin.DTO;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Profiles
{
    public class BiographyProfile : Profile
    {
        public BiographyProfile()
        {
            CreateMap<Biography, BiographyReadDTO>();
            CreateMap<BiographyCreateDTO, Biography>();
            CreateMap<Biography, BiographyUpdateDTO>();
            CreateMap<BiographyUpdateDTO, Biography>();
        }
    }
}
