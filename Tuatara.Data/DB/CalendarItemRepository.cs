using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.DB
{
    public class CalendarItemRepository : EFRepositoryBase<CalendarItem>, ICalendarItemRepository
    {
        public CalendarItem GetItemByDate(DateTime dt)
        {
            var serialDate = dt.Year * 10000 + dt.Month * 100 + dt.Day;
            return FirstOrDefault(cal => cal.ID == serialDate);
        }
    }
}
