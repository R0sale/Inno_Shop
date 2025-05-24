using Application.Dtos;
using Entities.Models;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingProfile : Profile
    {


        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserForUpdateDTO, User>().ReverseMap();
            CreateMap<UserForRegistrationDTO, User>();
        }
    }
}
