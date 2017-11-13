using Tuatara.Data;

namespace Tuatara.Services.Dto
{
    public class CalendarItemDto
    {
        public int ID { get; set; }
        public int WeekNo { get; set; }
        public PeriodTypes PeriodType { get; set; }
    }
}