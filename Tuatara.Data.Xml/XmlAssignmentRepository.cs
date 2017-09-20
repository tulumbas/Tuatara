using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Xml
{
    public class XmlAssignmentRepository : XmlRepositoryBase<Assignment>, IAssignmentRepository
    {
        public XmlAssignmentRepository()
        {
            SimpleLoad("Assignments.xml");
        }

        protected override Assignment ItemFactory(XElement node)
        {
            var item = new Assignment
            {
                ApprovedStamp = node.GetStampFromChild("ApprovedStamp"),
                ApproverID = node.GetNullableIntFromChild("ApproverID"),
                Description = node.GetStringFromChild("Description"),
                Duration = node.GetDoubleFromChild("Duration") ?? 0.0,
                ID = node.GetIntFromChild("ID"),
                RequestedStamp = node.GetStampFromChild("RequestedStamp") ?? DateTime.MinValue,
                RequestorID = node.GetIntFromChild("RequestorID"),
                ResourceID = node.GetIntFromChild("ResourceID"),
                WhatID = node.GetIntFromChild("WhatID"),
                WhenID = node.GetIntFromChild("WhenID"),

                Intraweek = (Intraweeks)node.GetIntFromChild("Intraweek"),
                Priority = (Priorities)node.GetIntFromChild("Priority"),
                Status = (Statuses)node.GetIntFromChild("Status"),                
            };

            return item;
        }

        void RestoreKeys()
        {

        }
    }

}
