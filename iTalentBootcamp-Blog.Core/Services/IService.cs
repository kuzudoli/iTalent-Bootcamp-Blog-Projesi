using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);//multiple add

        Task UpdateAsync(T entity);//SaveChanges çalıştırılacağı için async olarak belirlendi

        Task RemoveAsync(T entity);//SaveChanges çalıştırılacağı için async olarak belirlendi

        Task RemoveRangeAsync(IEnumerable<T> entities);//SaveChanges çalıştırılacağı için async olarak belirlendi
    }
}
