using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Tuatara.Data.Repositories;
using Tuatara.Data.Entities;

namespace Tuatara.Services.BL
{
    public class AssignmentService : BaseService
    {
        CalendarService _calendar;
        StatusService _statuses;
        IRepository<AssignmentEntity> _repository;
        IMapper _mapper;

        /// <summary>
        /// Automapper DI by Unity
        /// </summary>
        /// <param name="mapper"></param>
        public AssignmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            StatusService statuses,
            CalendarService calendar)
            : base(unitOfWork)
        {
            _repository = unitOfWork.Repository<AssignmentEntity>();
            _mapper = mapper;
            _calendar = calendar;
            _statuses = statuses;
        }

        public void CreateAssignment(int weekID, PlaybookRow newRow, int requestorID)
        {
            var task = new AssignmentEntity
            {
                Description = newRow.Description,
                Duration = newRow.Duration,
                IntraweekID = newRow.IntraweekID,
                PriorityID = newRow.PriorityID,
                StatusID = _statuses.Booked.ID,
                RequestorID = requestorID,
                RequestedStamp = DateTime.Now,
                ResourceID = newRow.ResourceID,
                WhatID = newRow.WhatID,
                WhenID = weekID,
                Completed = 0.0,
            };

            _repository.Add(task);
            UnitOfWork.Commit();
        }

        public Playbook GetPlaybookForWeekShift(int weekShift)
        {
            var week = _calendar.GetStartOfWeekItemByShift(weekShift);

            var data = _repository.Query(a => a.WhenID == week.ID).ToList();
            var rows = data.Select(row => _mapper.Map<PlaybookRow>(row)).ToList();

            var result = new Playbook
            {
                WeekID = week.ID,
                WeekNo = week.WeekNo
            };
            result.Rows.AddRange(rows);

            return result;
        }

        protected override void DisposeDisposables()
        {
            //_repository.Dispose();
        }
    }
}