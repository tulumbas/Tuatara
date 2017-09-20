using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tuatara.Data;

namespace Tuatara.Models
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