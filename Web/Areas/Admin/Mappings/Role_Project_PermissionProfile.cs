using Web.Areas.Admin.Models;
using AutoMapper;

namespace Web.Areas.Admin.Mappings
{
    public class Role_Project_PermissionProfile : Profile
    {
        public Role_Project_PermissionProfile()
        {
            CreateMap<Domain.Entities.Basic.Role_Project_Permission, Role_Project_PermissionViewModel>().ReverseMap();
        }
    }
}