using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingDemo.Core.Infrastructure;
using TestingDemo.Core.ModelsDtos;

namespace TestingDemo.Core.Interfaces.Services
{
    public interface IGenericService<T> where T : BaseDto
	{
        Task<GenericResult<T>> GetByIdAsync(int id);
        Task<GenericResult<T>> GetOneByMatchAsync(Expression<Func<T, bool>> match);
        Task<GenericResult<IEnumerable<T>>> GetByMatchAsync(Expression<Func<T, bool>> match);
        Task<GenericResult<IEnumerable<T>>> GetAllAsync();

        Task<GenericResult<T>> AddAsync(T dto);
        Task<GenericResult<T>> UpdateAsync(T dto);
        Task<GenericResult<int>> DeleteAsync(int id);
        Task<GenericResult<int>> CountAsync();
        Task<GenericResult<int>> SaveAsync();

        void Dispose();
    }
}
