using AutoMapper;
using Gym.Areas.Admin.DTO;
using Gym.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Areas.Admin.Profiles
{
    public class GroupProfile:Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupDTO, Group>();
            CreateMap<Group, GroupDTO>();
        }
    }
}
