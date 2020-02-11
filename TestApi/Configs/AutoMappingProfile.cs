using AutoMapper;
using System;
using TestApi.BL.DTOs;
using TestApi.DAL.Entities;
using TestApi.DAL.Enums;

namespace TestApi
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<ProductEntity, ProductDTO>().ReverseMap();
            CreateMap<ProductEntity, ProductOutcomeDTO>().ReverseMap();

            CreateMap<UserEntity, UserDTO>()
                .ForMember(dest => dest.Login, opts => opts.MapFrom(src => src.UserLogin))
                .ForMember(dest => dest.Login, opts => opts.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<UserEntity, UserAuth>()
                .ForMember(dest => dest.Login, opts => opts.MapFrom(src => src.UserLogin))
                .ForMember(dest => dest.Login, opts => opts.MapFrom(src => src.UserName)).ReverseMap();


            CreateMap<TransactionEntity, TransactionDTO>().ReverseMap();
            CreateMap<TransactionEntity, TransactionOutcomeDTO>().ReverseMap();
        }
    }
}
