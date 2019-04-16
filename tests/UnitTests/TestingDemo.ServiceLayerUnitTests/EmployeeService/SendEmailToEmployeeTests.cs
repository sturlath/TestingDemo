using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using Shouldly;
using System;
using System.Threading.Tasks;
using TestingDemo.Core.Interfaces.Repository;
using TestingDemo.Core.Interfaces.Services;
using TestingDemo.Core.ModelsDtos;
using TestingDemo.ServiceLayer;

namespace TestingDemo.ServiceLayerUnitTests
{
    //Unit test naming convention: Should_ExpectedBehavior_When_StateUnderTest

    [TestClass]
    [TestCategory("UnitTests")]
    public class SendEmailToEmployeeTests
    {
        /*******************************************************************************/
        /*  A detailed explanation on how this all works is at the bottom of this page */
        /*******************************************************************************/

        [TestMethod]
        [TestCategory("Mocks")]
        [Author("Bob")]
        public async Task Should_SendEmailToEmployee_When_EmployeeIsOverTheAgeOf13()
        {
            // Arrange
            IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
            IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

            var returnString = "Successfully sent email to the employee";

            A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).Returns(returnString);
            // Act
            var employeeService = new EmployeeService(employeeRepository, proxyMethods);
            var employeeDto = new EmployeeDto() { Age = 27, Name = "Bill" };
            var result = await employeeService.SendEmailToEmployee(employeeDto, "");

            // Assert
            result.ShouldBe(returnString);
            A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }

        [TestMethod]
        [TestCategory("Mocks")]
        [Author("Bob")]
        [DataRow(14)]
        [DataRow(15)]
        [DataRow(16)]
        [DataRow(17)]
        [DataRow(18)]
        [DataRow(19)]
        [DataRow(20)]
        [DataRow(21)]
        public async Task Should_SendEmailToManyEmployees_When_EmployeeIsOverTheAgeOf13(int age)
        {
            // Arrange
            IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
            IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

            var returnString = "Successfully sent email to the employee";

            A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).Returns(returnString);
            // Act
            var employeeService = new EmployeeService(employeeRepository, proxyMethods);
            var employeeDto = new EmployeeDto() { Age = age, Name = "Bill" };
            var result = await employeeService.SendEmailToEmployee(employeeDto, "");

            // Assert
            result.ShouldBe(returnString);
            A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }

        [TestMethod]
        [TestCategory("Mocks")]
        [Author("Bob")]
        public async Task Should_NotSendEmailToEmployee_When_EmployeeIsUnderTheAgeOf13()
        {
            // Arrange
            IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
            IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

            var returnString = "The person is to young to be an employee!";

            A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).Returns(returnString);

            // Act
            var employeeService = new EmployeeService(employeeRepository, proxyMethods);
            var employeeDto = new EmployeeDto() { Age = 12, Name = "Christopher" };
            var result = await employeeService.SendEmailToEmployee(employeeDto, "").ConfigureAwait(false);

            // Assert
            result.ShouldBe(returnString);
             A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).MustNotHaveHappened();
        }



        [TestMethod]
        [TestCategory("Exceptions")]
        public async Task Should_ThrowException_When_EmployeeIsNotBorn()
        {
            // Arrange
            IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
            IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

            // Act
            var employeeService = new EmployeeService(employeeRepository, proxyMethods);
            var employeeDto = new EmployeeDto() { Age = -1, Name = "Christopher" };

            // Assert (the call to the method should throw an exception)
            NotImplementedException exception = await Assert.ThrowsExceptionAsync<NotImplementedException>(async () => await employeeService.SendEmailToEmployee(employeeDto, string.Empty));

            exception.Message.ShouldBe("Sorry this doesn't work... employee needs to be born!");
        }

        /*******************************************************************************/
        /*  More thorough explanation of the tests here above   */
        /*******************************************************************************/

        //[TestMethod]
        //[TestCategory("Mocks")] <-- A way to sort your tests in the Test Explorer
        //public async Task Should_SendEmailToEmployee_When_EmployeeIsOverTheAgeOf13Async()
        //{
        //    // Arrange
        //    var proxyMethods = A.Fake<IProxyMethods>();                       <-- Create fake objects with FakeItEasy
        //    var employeeRepository = A.Fake<IEmployeeRepository>();           <-- Create fake objects with FakeItEasy

        //    string returnString = "Successfully sent email to the employee";  <-- The value we want the proxy method to return

        //    v-- Setup of how the mocking should function. A<T>.Ingnored means that is doesn't matter what goes in to the proxy. You could restrict the return based on what is sent in by newing up an object and send it in.
        //    A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).Returns(returnString); 

        //    // Act
        //    var employeeService = new EmployeeService(employeeRepository, proxyMethods); <-- new up the service with the fake objects
        //    var employeeDto = new EmployeeDto() { Age = 27, Name = "Bill" };              
        //    var result = await employeeService.SendEmailToEmployee(employeeDto, "");      <-- Call your code just like would do normally

        //    // Assert
        //    result.ShouldBe(returnString); <-- assert using the Shoudly package.
        //    A.CallTo(() => proxyMethods.SendEmailToEmployeeAsync(A<EmployeeDto>.Ignored, A<string>.Ignored)).MustHaveHappened(); <-- Assert that this method was called
        //}
    }
}
