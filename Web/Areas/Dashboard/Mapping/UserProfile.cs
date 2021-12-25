using AutoMapper;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Web.Areas.Dashboard.Models;

namespace Web.Areas.Dashboard.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AdditionalUserDataViewModel, AdditionalUserData>().ReverseMap();

            CreateMap<ApplicationUser, EditUserViewModel>().ReverseMap();
        }
    }
}
