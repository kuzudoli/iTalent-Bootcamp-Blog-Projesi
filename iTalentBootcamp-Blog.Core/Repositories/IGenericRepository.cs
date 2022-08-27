using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
