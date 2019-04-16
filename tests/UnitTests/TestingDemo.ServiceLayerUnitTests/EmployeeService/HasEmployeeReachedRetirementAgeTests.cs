using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using Shouldly;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestingDemo.Core.Interfaces.Repository;
using TestingDemo.Core.Interfaces.Services;
using TestingDemo.Core.Entities;
using TestingDemo.ServiceLayer;

namespace TestingDemo.ServiceLayerUnitTests
{
    //Unit test naming convention: Should_ExpectedBehavior_When_StateUnderTest

    [TestClass]
    [TestCategory("UnitTests")]
    [TestCategory("Mocks")]
    public class HasEmployeeReachedRetirementAgeTests
    {
        [TestMethod]
        [Author("Peter")]

        public async Task Should_SendEmailToEmployee_When_EmployeeIsOverTheAgeOf13()
        {
            // Arrange
            IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
            IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

            A.CallTo(() => employeeRepository.HasEmployeeReachedRetirementAge(A<Expression<Func<Employee, bool>>>.Ignored)).Returns(true);

            // Act
            var employeeService = new EmployeeService(employeeRepository, proxyMethods);
            var result = await employeeService.HasEmployeeReachedRetirementAge(1).ConfigureAwait(false);

            // Assert
            result.ShouldBe(true);
        }

        [TestMethod]
        [Author("Peter")]
        public async Task Should_NoSendEmailToEmployee_When_EmployeeIsUnderTheAgeOf13()
        {
            // Arrange
            IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
            IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

            A.CallTo(() => employeeRepository.HasEmployeeReachedRetirementAge(A<Expression<Func<Employee, bool>>>.Ignored)).Returns(false);

            // Act
            var employeeService = new EmployeeService(employeeRepository, proxyMethods);
            var result = await employeeService.HasEmployeeReachedRetirementAge(1).ConfigureAwait(false);

            // Assert
            result.ShouldBe(false);
        }
    }
}
