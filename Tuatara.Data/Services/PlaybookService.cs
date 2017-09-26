using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data.Dto;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Services
{
    public class PlaybookService : BaseService
    {
        IAssignmentRepository _assignments;

        CalendarService _calendar;
        AssignmentService _assignments;
        IMapper _mapper;

        public PlaybookService(IMapper mapper, IAssignmentRepository assignements, CalendarService calendar)
        {
            _calendar = calendar;
            _assignments = assignments;
            _mapper = mapper;
        }

        public PlaybookDto GetPlaybookForWeekShift(int weekShift)
        {
            var week = _calendar.GetStartOfWeekItemByShift(weekShift);
            var result = new PlaybookDto
            {
                Week = week
            };
            result.Rows.AddRange(_assignments.GetAllAssignmentsPerWeek(week.ID));
            return result;
        }

        //public PlaybookDto GetPlaybookByDate(DateTime dt)
        //{
        //    var calendarItem = _calendar.GetStartOfWeekItemByDate(dt);

        //    var result = new PlaybookDto
        //    {
        //        Week = calendarItem
        //    };

        //    if (calendarItem != null)
        //    {
        //        result.Rows.AddRange(
        //            _assignments.GetAllAssignmentsPerWeek(calendarItem.ID).Select(x => ConvertToVM(x))
        //            );
        //    }

        //    return result;
        //}

        //public static PlaybookRowDto ConvertToVM(Assignment row)
        //{
        //    return new PlaybookRowDto
        //    {
        //        ID = row.ID,
        //        Description = row.Description,
        //        Duration = row.Duration,
        //        Intraweek = row.Intraweek,
        //        Priority = row.Priority,
        //        ProjectID = row.WhatID,
        //        RequestorName = row.Requestor?.Name,
        //        ResourceID = row.ResourceID,
        //        Status = row.Status
        //    };
        //}

        protected override void DisposeDisposables()
        {
            _calendar.Dispose();
            _assignments.Dispose();
        }
    }

}