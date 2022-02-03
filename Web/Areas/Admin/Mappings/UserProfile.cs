using Domain.Entities.Base.Identity;
using Web.Areas.Admin.Models;
using AutoMapper;
using System.Collections.Generic;
using Common.Models.DataTable;

namespace Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(des => des.BankAccNumber, m => m.MapFrom(s => s.Bank.AccountNumber))
                .ForMember(des => des.BankName, m => m.MapFrom(s => s.Bank.Title))
                .ForMember(des => des.ProjectName, m => m.MapFrom(s => s.Project.Title));

            CreateMap<UserViewModel, ApplicationUser>();

            CreateMap<DataTableDTO<IEnumerable<ApplicationUser>>, DataTableViewModel<IEnumerable<UserViewModel>>>()
                .ForMember(model => model.ViewModel, m => m.MapFrom(s => s.Model));

            //CreateMap<DataTableViewModel<IEnumerable<UserViewModel>>, DataTableDTO<IEnumerable<ApplicationUser>>>()
            //    .ForMember(model => model.Model, m => m.MapFrom(s => s.ViewModel));
        }
    }
}