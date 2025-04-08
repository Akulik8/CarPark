using AutoMapper;
using CarParkSystem.App.DTOs;
using CarParkSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarParkSystem.App.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                 .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                 .ForMember(dest => dest.LastLogin, opt => opt.Ignore())
                 .ForMember(dest => dest.Bids, opt => opt.Ignore());
        }
    }
}