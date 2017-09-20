(function () {
    var Tuatara = (window.Tuatara || (window.Tuatara = {}));

    Tuatara.statuses = [
        {value: 0, text: "Booked"},
        {value: 1, text: "Confirmed"},
        {value: 2, text: "Rescheduled"},
        {value: 3, text: "Cancelled"},
        {value: 4, text: "Completed"}
    ];

    Tuatara.priorities = [
        {value: 0, text: "None"},
        {value: 1, text: "Holiday"},
        {value: 2, text: "Amber"},
        {value: 3, text: "Red"},
        {value: 4, text: "BAU"}
    ];

    Tuatara.calendarRanges = {
        day: 0,
        week: 1,
        month: 2
    };

    Tuatara.weekDays = [
        {}
    ];

})();
