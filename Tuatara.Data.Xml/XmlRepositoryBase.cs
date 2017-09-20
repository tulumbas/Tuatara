using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tuatara.Data.Repositories;

namespace Tuatara.Data.Xml
{
    public abstract class XmlRepositoryBase<T> : IRepository<T> where T:class, new()
    {
        public List<T> Items { get; protected set; } 
        public Func<T, int, bool> CompareID { get; }

        public virtual void Dispose() { }

        protected XmlRepositoryBase()
        {
            Items = new List<T>();
            var type = typeof(T);
            var param1 = Expression.Parameter(type, "x");
            var param2 = Expression.Parameter(typeof(int), "id");
            CompareID = Expression.Lambda<Func<T, int, bool>>(
                Expression.Equal(Expression.PropertyOrField(param1, "ID"), param2), 
                param1, param2).Compile();
        }

        protected virtual T ItemFactory(XElement node) { return null; }

        protected void SimpleLoad(string fileName)
        {
            fileName = Path.Combine(XmlContextExtensions.XMLDATA_FOLDER, fileName);
            var xdoc = XDocument.Load(fileName);
            foreach (var node in xdoc.Root.Elements())
            {
                var item = ItemFactory(node);
                Items.Add(item);
            }
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Items.AsQueryable<T>().FirstOrDefault(predicate);
        }

        public virtual T Get(int id)
        {
            return Items.FirstOrDefault(z => CompareID(z, id));
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Items;
        }

        public virtual IEnumerable<T> Query(Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            int? skip = default(int?), int? take = default(int?))
        {
            IQueryable<T> query = Items.AsQueryable<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}
