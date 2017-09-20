using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Models.Services
{
    public class CalendarService : BaseService
    {
        ICalendarItemRepository _repository;
        IMapper _mapper;

        public CalendarService(ICalendarItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public CalendarItemDto GetStartOfWeekItemByShift(int weekShift)
        {
            var dayInsideWeek = DateTime.Now.AddDays(weekShift * 7);
            var result = GetStartOfWeekItemByDate(dayInsideWeek);
            return result;
        }

        public CalendarItemDto GetStartOfWeekItemByDate(DateTime dt)
        {
            var shift = dt.DayOfWeek == DayOfWeek.Sunday ? 7 : ((int)dt.DayOfWeek) - 1;
            var startOfWeek = dt.AddDays(-shift);
            var result = _mapper.Map<CalendarItemDto>(_repository.GetItemByDate(startOfWeek));
            return result;
        }

        protected override void DisposeDisposables()
        {
            _repository.Dispose();
        }
    }
}