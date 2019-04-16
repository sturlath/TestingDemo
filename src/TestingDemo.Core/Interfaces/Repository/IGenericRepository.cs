using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingDemo.Core.Entities;

namespace TestingDemo.Core.Interfaces.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
	{
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeExpressions);
        Task<T> GetOneByMatchAsync(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> GetByMatchAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includeExpressions);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<int> CountAsync();
        Task<int> SaveAsync();

        void Dispose();
    }
}
