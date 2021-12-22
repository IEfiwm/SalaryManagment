using AutoMapper;
using Domain.Entities.Basic;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectViewModel>().ReverseMap();
        }
    }
}
