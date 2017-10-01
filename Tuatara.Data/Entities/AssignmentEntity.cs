using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tuatara.Data.Entities
{
    [Table("Assignments")]
    public class AssignmentEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Priority")]
        public int PriorityID { get; set; }
        public virtual PriorityEntity Priority { get; set; }

        [Required]
        [ForeignKey("Status")]
        public int StatusID { get; set; }
        public virtual PlaybookStatusEntity Status { get; set; }

        [Required]
        [ForeignKey("Intraweek")]
        public int IntraweekID { get; set; }
        public virtual IntraweekEntity Intraweek { get; set; }

        [Required]
        [ForeignKey("Resource")]
        public int ResourceID { get; set; }
        public virtual AssignableResourceEntity Resource { get; set; }

        [Required]
        [ForeignKey("What")]
        public int WhatID { get; set; }
        public virtual WorkEntity What { get; set; }

        [Required]
        [ForeignKey("When")]
        public int WhenID { get; set; }
        public virtual CalendarItemEntity When { get; set; }

        public int RequestorID { get; set; }
        public int? ApproverID { get; set; }

        public DateTime RequestedStamp { get; set; }
        public DateTime? ApprovedStamp { get; set; }

        public virtual TuataraUserEntity Requestor { get; set; }
        public virtual TuataraUserEntity Approver { get; set; }

        public double Duration { get; set; }
        public double Completed { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide short description of what should be done")]
        public string Description { get; set; }

        public string RTID { get; set; }
        public string JiraID { get; set; }

    }

}
