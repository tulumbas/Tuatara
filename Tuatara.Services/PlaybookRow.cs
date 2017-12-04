using Tuatara.Data;

namespace Tuatara.Services
{
    public class PlaybookRow
    {
        /// <summary>
        /// Row id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Id and name of assignable resource
        /// </summary>
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }

        /// <summary>
        /// Name and ID of project
        /// </summary>
        public int WhatID { get; set; }
        public string WhatName { get; set; }

        /// <summary>
        /// Priority id, label and sort order
        /// </summary>
        public int PriorityID { get; set; }
        public string PriorityName { get; set; }
        public int PrioritySort { get; set; }

        /// <summary>
        /// Status id, name and sort oreder
        /// </summary>
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int StatusSort { get; set; }

        /// <summary>
        /// Intraweek id, name and sort order
        /// </summary>
        public int IntraweekID { get; set; }
        public string IntraweekName { get; set; }
        public int IntraweekSort { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Requestor name
        /// </summary>
        public string RequestorName { get; set; }
    }
}