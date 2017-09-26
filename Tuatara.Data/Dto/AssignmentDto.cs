using Tuatara.Data;

namespace Tuatara.Data.Dto
{
    public class AssignmentDto
    {
        public int ID { get; set; }
        public int ResourceID  { get; set; }
        public int ProjectID { get; set; }
        public string Description { get; set; }
        public Priorities Priority { get; set; }
        public Statuses Status { get; set; }
        public double Duration { get; set; }
    }
}