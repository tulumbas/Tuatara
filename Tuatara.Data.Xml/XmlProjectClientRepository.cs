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
    public class XmlProjectClientRepository : XmlRepositoryBase<Work>, IProjectClientRepository
    {
        public XmlProjectClientRepository()
        {
            var fileName = "Works.xml";
            SimpleLoad(fileName);
            RestoreHierarchy();
        }

        protected override Work ItemFactory(XElement node)
        {
            var item = new Work
            {
                ID = node.GetIntFromChild("ID"),
                ProjectName = node.GetStringFromChild("ProjectName"),
                ParentID = node.GetNullableIntFromChild("ParentID")
            };
            return item;
        }

        private void RestoreHierarchy()
        {
            var reference = Items.ToDictionary(x => x.ID);

            foreach (var item in Items)
            {
                Work parent;
                if (item.ParentID.HasValue && reference.TryGetValue(item.ParentID.Value, out parent))
                {
                    item.Parent = parent;
                    parent.SubItems.Add(item);
                }
            }
        }
    }
}
