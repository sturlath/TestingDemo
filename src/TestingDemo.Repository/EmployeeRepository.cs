using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingDemo.Core.Interfaces.Repository;
using TestingDemo.Core.Entities;

namespace TestingDemo.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

		//Only implement none-generic functions from IEmployeeRepository here below!

		public async Task<bool> HasEmployeeReachedRetirementAge(Expression<Func<Employee, bool>> match)
        {
            var result =  await dbContext.Set<Employee>().Where(match).ToListAsync();
            return result.Count > 0;
        }
    }
}
