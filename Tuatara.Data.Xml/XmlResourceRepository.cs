using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Xml
{
    public class XmlResourceRepository : XmlRepositoryBase<AssignableResourceEntity>
    {
        public XmlResourceRepository()
        {
            var fileName = "AssignableResources.xml";
            SimpleLoad(fileName);
            RestoreHierarchy();
        }

        private void RestoreHierarchy()
        {
            var reference = Items.ToDictionary(x => x.ID);

            foreach (var item in Items)
            {
                AssignableResourceEntity parent;
                if (item.ParentID.HasValue && reference.TryGetValue(item.ParentID.Value, out parent))
                {
                    item.Parent = parent;
                    parent.SubResources.Add(item);
                }
            }
        }

        protected override AssignableResourceEntity ItemFactory(XElement node)
        {
            var item = new AssignableResourceEntity
            {
                ID = node.GetIntFromChild("ID"),
                IsBookable = node.GetIntFromChild("IsBookable") != 0,
                ParentID = node.GetNullableIntFromChild("ParentID"),
                Name = node.GetStringFromChild("Name"),
                SubResources = new List<AssignableResourceEntity>()                
            };

            return item;
        }
    }
}
