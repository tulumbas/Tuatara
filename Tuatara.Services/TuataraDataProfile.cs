using AutoMapper;
using Tuatara.Data.Dto;
using Tuatara.Data.Entities;

namespace Tuatara.Services
{
    public class TuataraDataProfile : Profile
    {
        public TuataraDataProfile()
        {
            CreateMap<AssignmentEntity, PlaybookRow>(MemberList.Destination)
                .ForMember(
                    dest=>dest.IntraweekSort,
                    opts=>opts.MapFrom(src=>src.Intraweek.DefaultSortOrder))
                .ForMember(
                    dest=>dest.PrioritySort,
                    opts=>opts.MapFrom(src=>src.Priority.DefaultSortOrder))
                .ForMember(
                    dest => dest.StatusSort,
                    opts => opts.MapFrom(src => src.Status.DefaultSortOrder))
                    ;

            CreateMap<WorkEntity, ProjectDto>(MemberList.Destination);
            CreateMap<AssignableResourceEntity, ResourceDto>(MemberList.Destination);
            CreateMap<CalendarItemEntity, CalendarItemDto>(MemberList.Destination);
        }
    }
}