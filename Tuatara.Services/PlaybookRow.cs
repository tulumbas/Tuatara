using Tuatara.Data;

namespace Tuatara.Services
{
    public class PlaybookRow
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string ResourceName { get; set; }
        public string WhatName { get; set; }
        public string PriorityName { get; set; }
        public string StatusName { get; set; }
        public string IntraweekName { get; set; }

        public int ResourceID { get; set; }
        public int WhatID { get; set; }

        public int PriorityID { get; set; }
        public int StatusID { get; set; }
        public int IntraweekID { get; set; }
        public int PrioritySort { get; set; }
        public int StatusSort { get; set; }
        public int IntraweekSort { get; set; }
        public double Duration { get; set; }
        public string RequestorName { get; set; }
    }
}