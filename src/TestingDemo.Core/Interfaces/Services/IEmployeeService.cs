using System.Threading.Tasks;
using TestingDemo.Core.ModelsDtos;

namespace TestingDemo.Core.Interfaces.Services
{
    public interface IEmployeeService : IGenericService<EmployeeDto>
    {
        /// <summary>
        /// Just some specific method that the generic ones are ill suited to handle (just an example)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> HasEmployeeReachedRetirementAge(int id);

        /// <summary>
        /// Doesn't go to the database but out to some web-hook
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        Task<string> SendEmailToEmployee(EmployeeDto employee, string content);
    }
}