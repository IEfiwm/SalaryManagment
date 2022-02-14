using AutoMapper;
using Domain.Entities.Basic;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Mappings
{
    public class Bank_AccountProfile : Profile
    {
        public Bank_AccountProfile()
        {
            CreateMap<Bank_Account, Bank_AccountViewModel>().ReverseMap();
        }
    }
}
