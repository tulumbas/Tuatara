using AutoMapper;
using System.Web.Http;
using Tuatara.Models;
using Tuatara.Models.Services;

namespace Tuatara.Controllers
{
    public class CalendarController : ApiController
    {
        CalendarService _service;
        IMapper _mapper;

        public CalendarController(CalendarService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public CalendarItemDto GetWeek(int shift)
        {
            var result = _service.GetStartOfWeekItemByShift(shift);
            return result;
        }

    }
}