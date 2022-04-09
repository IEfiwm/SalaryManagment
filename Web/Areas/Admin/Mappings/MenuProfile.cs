using Web.Areas.Admin.Models;
using AutoMapper;
using Domain.Entities.Basic;

namespace Web.Areas.Admin.Mappings
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuViewModel>().ReverseMap();
        }
    }
}