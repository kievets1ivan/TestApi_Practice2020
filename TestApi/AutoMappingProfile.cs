using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi.DTOs;
using TestApi.Entities;

namespace TestApi
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<ProductEntity, ProductDTO>().ReverseMap();
            CreateMap<UserEntity, UserDTO>()
                .ForMember(dest => dest.Login, opts => opts.MapFrom(src => src.UserLogin)).ReverseMap();
        }
    }
}
