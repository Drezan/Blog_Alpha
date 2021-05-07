using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace Blog_Alpha.Data.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(int Id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> Filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy = null,
            string IncludeProperties = null
        );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> Filter = null,
            string IncludeProperties = null
        );

        void Add (T Entity);
        void Delete(int Id);
        void Delete(T Entity);
    }
}
