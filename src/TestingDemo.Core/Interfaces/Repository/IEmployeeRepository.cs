using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingDemo.Core.Entities;

namespace TestingDemo.Core.Interfaces.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        // If you need ADD a customized (GetAllOffersThatExpireToDayExeptJohns) method you can put it here. But lets try not to shall we!

        // But if you just need to OVERRIDE the default generic function, do that in OfferRepository.cs

        Task<bool> HasEmployeeReachedRetirementAge(Expression<Func<Employee, bool>> match);
    }
}
