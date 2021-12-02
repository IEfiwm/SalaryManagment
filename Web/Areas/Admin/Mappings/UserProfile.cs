using Domain.Entities.Base.Identity;
using Web.Areas.Admin.Models;
using AutoMapper;

namespace Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}