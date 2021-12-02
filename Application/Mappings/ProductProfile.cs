using Application.Features.Products.Queries.GetAllCached;
using Application.Features.Products.Queries.GetAllPaged;
using Application.Features.Products.Queries.GetById;
using AutoMapper;

namespace Application.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //CreateMap<CreateProductCommand, Product>().ReverseMap();
            //CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            //CreateMap<GetAllProductsCachedResponse, Product>().ReverseMap();
            //CreateMap<GetAllProductsResponse, Product>().ReverseMap();
        }
    }
}