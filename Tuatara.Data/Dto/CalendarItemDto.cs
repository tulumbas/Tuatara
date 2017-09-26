using Tuatara.Data;

namespace Tuatara.Data.Dto
{
    public class CalendarItemDto
    {
        public int ID { get; set; }
        public int WeekNo { get; set; }
        public PeriodTypes PeriodType { get; set; }
    }
}