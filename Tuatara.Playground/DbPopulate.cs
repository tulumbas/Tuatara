using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tuatara.Data.DB;
using Tuatara.Data.Entities;
using Tuatara.Data.Repositories;
using Tuatara.Data.Xml;

namespace Tuatara.Playground
{
    class DbPopulate
    {
        public static void PopulateDB()
        {
            //PopulateData<AssignableResourceEntity, XmlResourceRepository>(x => x.Name);
            //PopulateData<TuataraUserEntity, XmlUserRepository>(x => x.Name);
            PopulateData<WorkEntity, XmlProjectClientRepository>(x => x.ID);
        }


        private static void PopulateData<T, TXmlRepository>(Expression<Func<T, object>> identifierEpression) 
            where T: class, IBaseEntity
            where TXmlRepository: IReadOnlyRepository<T>, new()
        {
            var repository = new TXmlRepository();
            var data = repository.GetAll().ToArray();

            using (var context = new TuataraContext())
            {
                context.Set<T>().AddOrUpdate(identifierEpression, data);
                context.SaveChanges();
            }
        }
    }
}
