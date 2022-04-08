using AutoMapper;
using Domain.Entities.Base.Identity;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<ApplicationRole, RoleViewModel>().ReverseMap();
        }
    }
}