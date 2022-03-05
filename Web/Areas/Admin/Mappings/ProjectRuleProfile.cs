using AutoMapper;
using Domain.Entities.Basic;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Mappings
{
    public class ProjectRuleProfile : Profile
    {
        public ProjectRuleProfile()
        {
            CreateMap<ProjectRule, ProjectRuleViewModel>().ReverseMap();
        }
    }
}
