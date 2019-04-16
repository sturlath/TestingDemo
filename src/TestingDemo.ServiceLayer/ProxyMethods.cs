using RestSharp;
using System;
using System.Threading.Tasks;
using TestingDemo.Core.Interfaces.Services;
using TestingDemo.Core.ModelsDtos;

namespace TestingDemo.ServiceLayer
{
	public class ProxyMethods : IProxyMethods
	{
		public async Task<string> SendEmailToEmployeeAsync(EmployeeDto employee, string content)
		{
			// We are never going to call this code since we are going to mock it out!

			RestClient client = new RestClient("http://some.emailService.com");
			RestRequest request = new RestRequest("");
			var result = await client.ExecuteTaskAsync(request).ConfigureAwait(false);

			return result.Content;
		}

		public void ThrowsAnErrorIfNotMcked()
		{
			throw new NotImplementedException("We never end up in here because we have mocked it away in our tests!");
		}

		public bool ShouldReturnTrueForRestOfTestToSucceed()
		{
			throw new NotImplementedException("We never end up in here because we have mocked it away in our tests!");
		}
	}
}
