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
    public class XmlProjectClientRepository : XmlRepositoryBase<WorkEntity>
    {
        public XmlProjectClientRepository()
        {
            var fileName = "Works.xml";
            SimpleLoad(fileName);
            RestoreHierarchy();
        }

        protected override WorkEntity ItemFactory(XElement node)
        {
            var item = new WorkEntity
            {
                ID = node.GetIntFromChild("ID"),
                Name = node.GetStringFromChild("ProjectName"),
                ParentID = node.GetNullableIntFromChild("ParentID")
            };
            return item;
        }

        private void RestoreHierarchy()
        {
            var reference = Items.ToDictionary(x => x.ID);

            foreach (var item in Items)
            {
                WorkEntity parent;
                if (item.ParentID.HasValue && reference.TryGetValue(item.ParentID.Value, out parent))
                {
                    item.Parent = parent;
                    if(parent.SubItems == null)
                    {
                        parent.SubItems = new List<WorkEntity>();
                    }
                    parent.SubItems.Add(item);
                }
            }
        }
    }
}
