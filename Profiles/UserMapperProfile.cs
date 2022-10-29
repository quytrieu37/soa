using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data.DTOs;
using DatingApp.API.Data.Entities;

namespace DatingApp.API.Profiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, MemberDto>()
                .ForMember(dest => dest.Age, options => options.MapFrom
                (src=> (src.DateOfBirth != null ? DateTime.Now.Year - src.DateOfBirth.Value.Year : 0)));
        }
    }
}