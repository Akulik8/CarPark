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
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            CreateMap<Bid, BidDto>().ReverseMap();
            CreateMap<CreateBidDto, Bid>()
                .ForMember(dest => dest.BidID, opt => opt.Ignore());

            CreateMap<Subdivision, SubdivisionDto>().ReverseMap();
            CreateMap<CreateSubdivisionDto, Subdivision>()
                .ForMember(dest => dest.SubdivisionID, opt => opt.Ignore());
        }
    }
}
