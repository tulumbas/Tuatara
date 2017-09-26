using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tuatara.Data.Repositories;
using Tuatara.Data.Dto;

namespace Tuatara.Data.Services
{
    public class AssignmentService : BaseService
    {
        CalendarService _calendar;
        IAssignmentRepository _repository;
        IMapper _mapper;
        
        /// <summary>
        /// Automapper DI by Unity
        /// </summary>
        /// <param name="mapper"></param>
        public AssignmentService(IMapper mapper, IAssignmentRepository repository, CalendarService calendar)
            : base()
        {
            _repository = repository;
            _mapper = mapper;
            _calendar = calendar;
        }

        public List<PlaybookRowDto> GetAllAssignmentsPerWeek(int weekID)
        {
            var result = new List<PlaybookRowDto>();
            var data = _repository.Query(a => a.WhenID == weekID);
            foreach (var item in data)
            {
                var row = _mapper.Map<PlaybookRowDto>(item);
                row.IntraweekName = row.Intraweek.ToString();
                row.PriorityName = row.Priority.ToString();
                row.StatusName = row.Status.ToString();
                row.Project = item.What.ProjectName;
                row.Resource = item.Resource.Name;
                row.RequestorName = item.Requestor.Name;
                result.Add(row);
            }
            return result;
        }

        public PlaybookDto GetPlaybookForWeekShift(int weekShift)
        {
            var week = _calendar.GetStartOfWeekItemByShift(weekShift);
            var result = new PlaybookDto
            {
                Week = week
            };
            result.Rows.AddRange(GetAllAssignmentsPerWeek(week.ID));
            return result;
        }

        protected override void DisposeDisposables()
        {
            _repository.Dispose();
        }
    }
}