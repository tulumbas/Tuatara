using System;

namespace Tuatara.Data
{
    public enum Priorities
    {
        None,
        Holiday,
        Amber,
        Red,
        BAU,
    };

    public enum Statuses
    {
        Booked,
        Confirmed,
        Rescheduled,
        Cancelled,
        Completed,
    };

    public enum Intraweeks
    {
        Any,
        Mon,
        Tue,
        Start,
        Wed,
        Mid,
        Thur,
        Fri,
        End
    };

    [Flags]
    public enum PeriodTypes : Int32
    {
        None,
        StartOfWeek = 1,
        StartOfMonth = 4,
    }

    public enum CalendarRanges
    {
        Day,
        Week,
        Month
    }

}