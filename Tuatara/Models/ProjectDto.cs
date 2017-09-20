using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuatara.Models
{
    public class ProjectDto
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public int? ParentID { get; set; }
    }
}