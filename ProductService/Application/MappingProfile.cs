using AutoMapper;
using Entities.Models;
using Application.RequestFeatures;
using Application.Dtos;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductForCreationDTO, Product>();
            CreateMap<ProductForUpdateDTO, Product>().ReverseMap();
        }
    }
}