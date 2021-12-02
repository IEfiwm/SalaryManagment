using Application.Features.Brands.Queries.GetAllCached;
using Application.Features.Brands.Queries.GetById;
using AutoMapper;

namespace Application.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            //CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            //CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            //CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
        }
    }
}