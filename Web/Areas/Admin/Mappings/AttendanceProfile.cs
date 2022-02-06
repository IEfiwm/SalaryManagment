using Web.Areas.Admin.Models;
using AutoMapper;
using Common.Models.DataTable;
using System.Collections.Generic;

namespace Web.Areas.Admin.Mappings
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceViewModel, Domain.Entities.Porc.Attendance>().ReverseMap();

            CreateMap<DataTableViewModel<IEnumerable<AttendanceViewModel>>, DataTableDTO<IEnumerable<Domain.Entities.Porc.Attendance>>>()
                .ForMember(model => model.Model, m => m.MapFrom(s => s.ViewModel))
                .ReverseMap()
                .ForMember(model => model.ViewModel, m => m.MapFrom(s => s.Model));
        }
    }
}