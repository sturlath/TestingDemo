using System;
using System.Threading.Tasks;
using TestingDemo.Core.Entities;
using TestingDemo.Core.Interfaces.Repository;
using TestingDemo.Core.Interfaces.Services;
using TestingDemo.Core.ModelsDtos;

namespace TestingDemo.ServiceLayer
{
	public class EmployeeService : GenericService<EmployeeDto, Employee>, IEmployeeService
	{
		public readonly IEmployeeRepository employeeRepository;
		public readonly IProxyMethods proxyMethods;

		/// <summary>
		/// All dependencies for this class go through the constructor.
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="proxyMethods"></param>
		public EmployeeService(IEmployeeRepository repository = null, IProxyMethods proxyMethods = null) : base(repository)
		{
			employeeRepository = repository;
			this.proxyMethods = proxyMethods;
		}

		//Only implement none-generic functions from IEmployeeService here below!

		public async Task<bool> HasEmployeeReachedRetirementAge(int id)
		{
			return await employeeRepository.HasEmployeeReachedRetirementAge(x => x.Id == id && x.Age > 67).ConfigureAwait(false);
		}

		public async Task<string> SendEmailToEmployee(EmployeeDto employee, string content)
		{
			if (employee.Age < 0)
			{
				throw new NotImplementedException("Sorry this doesn't work... employee needs to be born!");
			}

			if (employee.Age > 13)
			{
				return await proxyMethods.SendEmailToEmployeeAsync(employee, content).ConfigureAwait(false);
			}
			else
			{
				return "The person is to young to be an employee!";
			}
		}

		public virtual bool FirstMethod()
		{
			return SecondMethod();
		}

		public virtual bool SecondMethod()
		{
			ThirdMethod();

			return true;
		}

		public virtual void ThirdMethod()
		{
			throw new NotImplementedException("We never reach this one!");
		}

		public bool DefaultVoidMethod()
		{
			proxyMethods.ThrowsAnErrorIfNotMcked();

			return true;
		}

		public bool DefaultPrimitiveType()
		{
			return proxyMethods.ShouldReturnTrueForRestOfTestToSucceed();
		}
	}
}