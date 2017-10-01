using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data.Dto;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Services.BL
{
    public class CalendarService : BaseService
    {
        IRepository<CalendarItemEntity> _repository;
        IMapper _mapper;

        public CalendarService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork)
        {
            _repository = unitOfWork.Repository<CalendarItemEntity>();
            _mapper = mapper;
        }

        public CalendarItemDto GetStartOfWeekItemByShift(int weekShift)
        {
            var dayInsideWeek = DateTime.Now.AddDays(weekShift * 7);
            var result = GetStartOfWeekItemByDate(dayInsideWeek);
            return result;
        }

        public CalendarItemEntity GetItemByDate(DateTime dt)
        {
            var serialDate = dt.Year * 10000 + dt.Month * 100 + dt.Day;
            return _repository.FirstOrDefault(cal => cal.ID == serialDate);
        }

        public CalendarItemDto GetStartOfWeekItemByDate(DateTime dt)
        {
            var shift = dt.DayOfWeek == DayOfWeek.Sunday ? -6 : 1 - ((int)dt.DayOfWeek);
            var startOfWeek = dt.AddDays(shift);
            var result = _mapper.Map<CalendarItemDto>(GetItemByDate(startOfWeek));
            return result;
        }

        protected override void DisposeDisposables()
        {
            //_repository.Dispose();
        }
    }
}