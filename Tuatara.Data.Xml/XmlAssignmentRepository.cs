using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Xml
{
    public class XmlAssignmentRepository : XmlRepositoryBase<AssignmentEntity>
    {
        public XmlAssignmentRepository()
        {
            SimpleLoad("Assignments.xml");
        }

        protected override AssignmentEntity ItemFactory(XElement node)
        {
            var item = new AssignmentEntity
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

                IntraweekID = node.GetIntFromChild("Intraweek"),
                PriorityID = node.GetIntFromChild("Priority"),
                StatusID = node.GetIntFromChild("Status"),                
            };

            return item;
        }

        void RestoreKeys()
        {

        }
    }

}
