using Tuatara.Data;

namespace Tuatara.Models
{
    public class PlaybookRowDto
    {
        public int ID { get; set; }
        public int ResourceID  { get; set; }
        public string Resource { get; set; }
        public int ProjectID { get; set; }
        public string Project { get; set; }
        public string Description { get; set; }
        public Priorities Priority { get; set; }
        public Statuses Status { get; set; }
        public Intraweeks Intraweek { get; set; }
        public double Duration { get; set; }
        public string RequestorName { get; set; }
    }
}