namespace Tuatara.Migrations
{
    using Data;
    using Data.DB;
    using Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TuataraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Tuatara.Models.DB.TuataraContext";
        }

        protected override void Seed(TuataraContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Priorities.AddOrUpdate(
                p => p.Name,
                new PriorityEntity { Name = "Holiday", DefaultSortOrder = 100, ID = 1 },
                new PriorityEntity { Name = "BAU", DefaultSortOrder = 1, ID = 4 },
                new PriorityEntity { Name = "Red", DefaultSortOrder = 2, ID = 3 },
                new PriorityEntity { Name = "Amber", DefaultSortOrder = 3, ID = 2 });

            context.Intraweeks.AddOrUpdate(
                p => p.Name,
                new IntraweekEntity { Name = "Mon", DefaultSortOrder = 1, ID = 1 },
                new IntraweekEntity { Name = "Tue", DefaultSortOrder = 2, ID = 2 },
                new IntraweekEntity { Name = "Start", DefaultSortOrder = 3, ID = 3 },
                new IntraweekEntity { Name = "Wed", DefaultSortOrder = 4, ID = 4 },
                new IntraweekEntity { Name = "Mid", DefaultSortOrder = 5, ID = 5 },
                new IntraweekEntity { Name = "Thur", DefaultSortOrder = 6, ID = 6 },
                new IntraweekEntity { Name = "Fri", DefaultSortOrder = 7, ID = 7 },
                new IntraweekEntity { Name = "End", DefaultSortOrder = 8, ID = 8 },
                new IntraweekEntity { Name = "Any", DefaultSortOrder = 9, ID = 9 });

            context.PlaybookStatuses.AddOrUpdate(
                p => p.Name,
                new PlaybookStatusEntity { Name = "Booked", DefaultSortOrder = 1, ID = 1 },
                new PlaybookStatusEntity { Name = "Confirmed", DefaultSortOrder = 2, ID = 2 },
                new PlaybookStatusEntity { Name = "Completed", DefaultSortOrder = 3, ID = 3 },
                new PlaybookStatusEntity { Name = "Rescheduled", DefaultSortOrder = 4, ID = 4 },
                new PlaybookStatusEntity { Name = "Cancelled", DefaultSortOrder = 5, ID = 5 });

            var calendar = CreateCalendar();
            context.CalendarItems.AddOrUpdate(p => p.ID, calendar);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private static CalendarItemEntity[] CreateCalendar()
        {
            var calendar = new List<CalendarItemEntity>();
            // year 2017
            var seed = new DateTime(2017, 01, 02); // 02/01/17 Mon
            CalendarAddYear(calendar, seed);
            seed = new DateTime(2018, 01, 01); // 01/01/18 Mon
            CalendarAddYear(calendar, seed);
            return calendar.ToArray();
        }

        private static void CalendarAddYear(List<CalendarItemEntity> calendar, DateTime seed)
        {
            var dt = seed;
            while (dt.Year == seed.Year)
            {
                calendar.Add(new CalendarItemEntity
                {
                    ID = dt.Year * 10000 + dt.Month * 100 + dt.Day,
                    WeekNo = (int)Math.Floor((dt - seed).Days / 7.0) + 1,
                    PeriodType = (dt.Day == 1 ? PeriodTypes.StartOfMonth : PeriodTypes.None) |
                        (dt.DayOfWeek == DayOfWeek.Monday ? PeriodTypes.StartOfWeek : PeriodTypes.None)
                });
                dt = dt.AddDays(1);
            }
        }
    }
}
