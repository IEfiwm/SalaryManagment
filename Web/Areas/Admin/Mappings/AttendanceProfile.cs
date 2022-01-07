using Web.Areas.Admin.Models;
using AutoMapper;

namespace Web.Areas.Admin.Mappings
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceViewModel, Domain.Entities.Porc.Attendance>().ReverseMap();
        }
    }
}