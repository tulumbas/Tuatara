using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Xml
{
    public class XmlCalendarItemRepository : XmlRepositoryBase<CalendarItemEntity>
    {
        public XmlCalendarItemRepository()
        {
            var fileName = "CalendarItem.xml";
            SimpleLoad(fileName);
        }

        public CalendarItemEntity GetItemByDate(DateTime dt)
        {
            var serialDate = dt.Year * 10000 + dt.Month * 100 + dt.Day;
            return FirstOrDefault(cal => cal.ID == serialDate);
        }

        protected override CalendarItemEntity ItemFactory(XElement node)
        {
            var item = new CalendarItemEntity
            {
                ID = node.GetIntFromChild("ID"),
                WeekNo = node.GetIntFromChild("WeekNo"),
                PeriodType = (PeriodTypes)node.GetIntFromChild("PeriodType")
            };

            return item;
        }
    }
}
