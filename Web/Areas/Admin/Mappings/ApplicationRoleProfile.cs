using Web.Areas.Admin.Models;
using AutoMapper;
using Domain.Entities.Basic;
using Domain.Entities.Base.Identity;

namespace Web.Areas.Admin.Mappings
{
    public class ApplicationRoleProfile : Profile
    {
        public ApplicationRoleProfile()
        {
            CreateMap<ApplicationRole, RoleViewModel>().ReverseMap();
        }
    }
}