using AutoMapper;
using Domain.Entities.Basic;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Mappings
{
    public class BankProfile : Profile
    {
        public BankProfile()
        {
            CreateMap<Bank, BankViewModel>().ReverseMap();
        }
    }
}
