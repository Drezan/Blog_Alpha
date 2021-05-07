using Blog_Alpha.Data.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog_Alpha.Data.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = Context.Set<T>();
        }

        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }

        public void Delete(int Id)
        {
            T EntityToRemove = dbSet.Find(Id);
            Delete(EntityToRemove);
        }

        public void Delete(T Entity)
        {
            dbSet.Remove(Entity);
        }

        public T Get(int Id)
        {
            return dbSet.Find(Id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null, string IncludeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (Filter != null)
            {
                query.Where(Filter);
            }

            if (IncludeProperties != null) //Incluir propiedades separados por comas.
            {
                foreach (var includeProperty in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }     
            }

            if (OrderBy != null)
            {
                return OrderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> Filter = null, string IncludeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (Filter != null)
            {
                query.Where(Filter);
            }

            if (IncludeProperties != null) //Incluir propiedades separados por comas.
            {
                foreach (var includeProperty in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();

        }
    }
}
