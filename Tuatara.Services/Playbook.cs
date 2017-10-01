using System.Collections.Generic;

namespace Tuatara.Services
{
    public class Playbook
    {
        /// <summary>
        /// ID of calendar item, i.e. Date
        /// </summary>
        public int WeekID { get; set; }

        /// <summary>
        /// Week number
        /// </summary>
        public int WeekNo { get; set; }

        /// <summary>
        /// Rows
        /// </summary>
        public List<PlaybookRow> Rows { get; } = new List<PlaybookRow>();
    }

}