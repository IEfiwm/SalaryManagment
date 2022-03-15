using Web.Areas.Admin.Models;
using AutoMapper;
using Common.Models.DataTable;
using System.Collections.Generic;

namespace Web.Areas.Admin.Mappings
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<FieldViewModel, Domain.Entities.Data.Field>().ReverseMap();
        }
    }
}