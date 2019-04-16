using Newtonsoft.Json;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestingDemo.API;
using TestingDemo.Core.ModelsDtos;
using Xunit;

namespace TestingDemo.ApiIntegrationTests
{
    [Trait("End2End", "Api")]
    public class EmployeesControllerApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public EmployeesControllerApiTests(CustomWebApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
            Client.BaseAddress = new Uri("https://localhost");
        }

        //Unit test naming convention: Should_ExpectedBehavior_When_StateUnderTest

        [Fact(DisplayName = "Get an employee by id")]
        public async Task Should_GetEmployeeById_When_SuppliedWithAnIdThatExists()
        {
            // Arrange & Act
            HttpResponseMessage response = await Client.GetAsync("api/employee/1").ConfigureAwait(false);

            // Assert
            var message = response.Content.ReadAsStringAsync().Result;
            response.StatusCode.ShouldBe(HttpStatusCode.OK, message);

            EmployeeDto employee = JsonConvert.DeserializeObject<EmployeeDto>(response.Content.ReadAsStringAsync().Result);

            // Test data was added to the in-memory database in CustomWebApplicationFactory.cs
            employee.Id.ShouldBe(1);
            employee.Age.ShouldBe(27);
            employee.Name.ShouldBe("Gilbert");
        }

        [Fact(DisplayName = "Add an employee")]
        public async Task Should_AddEmployee_When_SuppliedWithAnNewEmployee()
        {
            //Arrange & Act
            var employee = new EmployeeDto() { Age = 55, Name = "Jónas" };

            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            // Add the QuoteDTO
            HttpResponseMessage response = await Client.PostAsync("/api/employee/add", content).ConfigureAwait(false);

            // Assert the response
            var message = response.Content.ReadAsStringAsync().Result;
            response.StatusCode.ShouldBe(HttpStatusCode.OK, message);

            EmployeeDto employeeResponse = JsonConvert.DeserializeObject<EmployeeDto>(response.Content.ReadAsStringAsync().Result);

            employeeResponse.Age.ShouldBe(55);
            employeeResponse.Name.ShouldBe("Jónas");

            // Double check!
            HttpResponseMessage response2 = await Client.GetAsync($"api/employee/{employeeResponse.Id}").ConfigureAwait(false);
            var message2 = response.Content.ReadAsStringAsync().Result;
            response.StatusCode.ShouldBe(HttpStatusCode.OK, message);
            EmployeeDto employeeResponse2 = JsonConvert.DeserializeObject<EmployeeDto>(response.Content.ReadAsStringAsync().Result);
            employeeResponse2.Age.ShouldBe(55);
            employeeResponse2.Name.ShouldBe("Jónas");
        }
    }
}
