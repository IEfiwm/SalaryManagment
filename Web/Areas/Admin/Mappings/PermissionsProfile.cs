using Web.Areas.Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Web.Areas.Admin.Mappings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Domain.Entities.Basic.Permission, PermissionsViewModel>().ReverseMap();
        }
    }
}