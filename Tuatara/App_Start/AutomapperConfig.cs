using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tuatara.Models;
using Tuatara.Data.Models;

namespace Tuatara.App_Start
{
    public class AutomapperConfig
    {
        public class TuataraProfile: Profile
        {
            public TuataraProfile()
            {
                CreateMap<Assignment, PlaybookRowDto>(MemberList.Destination);
                CreateMap<Work, ProjectDto>(MemberList.Destination);
                CreateMap<AssignableResource, ResourceDto>(MemberList.Destination);
                CreateMap<CalendarItem, CalendarItemDto>(MemberList.Destination);
            }
        }

        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<TuataraProfile>();
            });

            return config;
        }
    }
}