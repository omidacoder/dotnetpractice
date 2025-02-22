using AutoMapper;
using DotnetPractice.Common.Dtos.Product;
using DotnetPractice.Common.Dtos.User;
using DotnetPractice.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base() {
            CreateMapForUser();
            CreateMapForProduct();
        }
        protected void CreateMapForUser()
        {
            CreateMap<User, UserDetailsDto>()
                .ForMember(dest => dest.RegisteredAt, opt =>
                {
                    opt.MapFrom(src => src.CreatedAt.ToString());
                });
            CreateMap<CreateUserDto, User>().ForMember(dest => dest.CreatedAt, opt =>
            {
                opt.MapFrom(src => DateTime.Now);
            }).ForMember(dest => dest.UpdatedAt, opt =>
            {
                opt.MapFrom(src => DateTime.Now);
            });
            CreateMap<User, UserGeneralDto>();
            CreateMap<UpdateUserDto, User>().ForMember(dest => dest.UpdatedAt, opt =>
            {
                opt.MapFrom(src => DateTime.Now);
            }); 
        }
        protected void CreateMapForProduct()
        {
            CreateMap<Product, ProductGeneralDto>();
            CreateMap<Product, ProductDetailsDto>().ForPath(dest => dest.User, opt =>
            {
                opt.MapFrom(src => src.User);
            });
            CreateMap<CreateProductDto,  Product>().ForMember(dest => dest.CreatedAt, opt =>
            {
                opt.MapFrom(src => DateTime.Now);
            }).ForMember(dest => dest.UpdatedAt, opt =>
            {
                opt.MapFrom(src => DateTime.Now);
            }); ;
            CreateMap<UpdateProductDto, Product>().ForMember(dest => dest.UpdatedAt, opt =>
            {
                opt.MapFrom(src => DateTime.Now);
            }); ;
        }
    }
}
