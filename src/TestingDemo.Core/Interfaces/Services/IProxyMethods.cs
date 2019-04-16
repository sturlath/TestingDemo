using System.Threading.Tasks;
using TestingDemo.Core.ModelsDtos;

namespace TestingDemo.Core.Interfaces.Services
{
    public interface IProxyMethods
    {
        Task<string> SendEmailToEmployeeAsync(EmployeeDto employee, string content);

		void ThrowsAnErrorIfNotMcked();

		bool ShouldReturnTrueForRestOfTestToSucceed();
	}
}
