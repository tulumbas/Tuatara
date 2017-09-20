using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data;

namespace Tuatara.Models
{
    public class CalendarItemDto
    {
        public int ID { get; set; }
        public int WeekNo { get; set; }
        public PeriodTypes PeriodType { get; set; }
    }
}