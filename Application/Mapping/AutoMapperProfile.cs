using Application.DTOs.Customer;
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

            CreateMap<Customer, CustomerRegisterDTO>()
                // Customer -> CustomerRegisterDTO
                .ForMember(dest => dest.Gender, 
                            opt => opt.MapFrom(src => src.Gender ? "Male" : "Female"))
                .ReverseMap()
                // CustomerRegisterDTO -> Customer
                .ForMember(dest => dest.PasswordHash,
                            opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Gender, 
                            opt => opt.MapFrom(src => src.Gender == "Male"));

            CreateMap<Customer, CustomerViewDTO>()
                .ForMember(dest => dest.FullName, 
                            opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Gender, 
                            opt => opt.MapFrom(src => src.Gender ? "Male" : "Female"))
                .ReverseMap()
                .ForMember(dest => dest.Gender, 
                            opt => opt.MapFrom(src => src.Gender == "Male"));

            CreateMap<Customer, CustomerLoginDTO>()
                .ReverseMap();
        }
    }
}
