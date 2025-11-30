using Application.DTOs.Destination;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Destination, DestinationDTO>()
                .ForMember(dest => dest.ten, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.dia_chi, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.mo_ta, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();
        }
    }
}
