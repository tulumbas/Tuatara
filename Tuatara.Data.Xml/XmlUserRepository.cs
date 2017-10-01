using System.Xml.Linq;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Xml
{
    public class XmlUserRepository: XmlRepositoryBase<TuataraUserEntity>
    {
        public XmlUserRepository()
        {
            var fileName = "Users.xml";
            SimpleLoad(fileName);
        }

        protected override TuataraUserEntity ItemFactory(XElement node)
        {
            var item = new TuataraUserEntity
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