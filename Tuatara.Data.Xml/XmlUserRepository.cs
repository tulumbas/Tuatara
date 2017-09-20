using System.Xml.Linq;
using Tuatara.Data.Models;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Xml
{
    public class XmlUserRepository: XmlRepositoryBase<TuataraUser>, IUserRepository
    {
        public XmlUserRepository()
        {
            var fileName = "CalendarItem.xml";
            SimpleLoad(fileName);
        }

        protected override TuataraUser ItemFactory(XElement node)
        {
            var item = new TuataraUser
            {
                ID = node.GetIntFromChild("ID"),
                Name = node.GetStringFromChild("Name"),
                DomainName = node.GetStringFromChild("DomainName"),
                Email = node.GetStringFromChild("Email"),
            };

            return item;
        }
    }
}