using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tuatara.Data;

namespace Tuatara.Data.Models
{
    public class Assignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Priorities Priority { get; set; }
        public Statuses Status { get; set; }
        public Intraweeks Intraweek { get; set; }

        public int ResourceID { get; set; }
        public int WhatID { get; set; }
        public int WhenID { get; set; }

        public int RequestorID { get; set; }
        public int? ApproverID { get; set; }

        public DateTime RequestedStamp { get; set; }
        public DateTime? ApprovedStamp { get; set; }

        public virtual AssignableResource Resource { get; set; }
        public virtual Work What { get; set; }
        public virtual CalendarItem When { get; set; }

        public virtual TuataraUser Requestor { get; set; }
        public virtual TuataraUser Approver { get; set; }

        public double Duration { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide short description of what should be done")]
        public string Description { get; set; }

        public string RTID { get; set; }
        public string JiraID { get; set; }

    }

}
