using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuatara.Models
{
    public class ResourceDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
    }
}