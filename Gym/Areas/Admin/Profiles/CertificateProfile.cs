using AutoMapper;
using Gym.Areas.Admin.DTO;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Profiles
{
    public class CertificateProfile:Profile
    {
        public CertificateProfile()
        {
            CreateMap<Certificate, CertificateDTO>();
            CreateMap<CertificateDTO, Certificate>();
        }
    }
}
