using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using Shouldly;
using TestingDemo.Core.Interfaces.Repository;
using TestingDemo.Core.Interfaces.Services;
using TestingDemo.ServiceLayer;

namespace TestingDemo.ServiceLayerUnitTests
{
	//Unit test naming convention: Should_ExpectedBehavior_When_StateUnderTest

	[TestClass]
	[TestCategory("UnitTests")]
	[TestCategory("Mocks")]
	public class DefaultValuesAndVoidMethodMocking
	{
		[TestMethod]
		[TestCategory("Mocks")]
		[Author("Bob")]
		public void Should_AutomaticallyMockVoidMethod_When_TheNotSetupWithACallTox()
		{
			// Arrange
			IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
			IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

			// DefaultVoidMethod() calls void method ThrowsAnErrorIfNotMcked() that is mocked by DEFAULT if not explicitly set up with A.CallTo.
			//
			// All other methods NOT setup will return their default values (bool == false,  int == 0 etc. )
			// Take a look at this default value table https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/default-values-table)

			// Act
			var employeeService = new EmployeeService(employeeRepository, proxyMethods);
			var result = employeeService.DefaultVoidMethod();

			// Assert
			result.ShouldBe(true);

			// Void methods are mocked out by default. 
			A.CallTo(proxyMethods).Where(x => x.Method.Name == "ThrowsAnErrorIfNotMcked").MustHaveHappened(); //<-- This is how we check if a void method is called.
		}

		[TestMethod]
		[TestCategory("Mocks")]
		[Author("Bob")]
		public void Should_AutomaticallyMockVoidMethod_When_TheNotSetupWithACallTo()
		{
			// Arrange
			IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
			IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

			A.CallTo(() => proxyMethods.ThrowsAnErrorIfNotMcked()).DoesNothing(); // <-- By setting this up we get the control to call MustHaveHappened()

			// Act
			var employeeService = new EmployeeService(employeeRepository, proxyMethods);
			var result = employeeService.DefaultVoidMethod();

			// Assert
			result.ShouldBe(true);

			// Void methods are mocked out by default. 
			A.CallTo(() => proxyMethods.ThrowsAnErrorIfNotMcked()).MustHaveHappened();
		}

		[TestMethod]
		[TestCategory("Mocks")]
		[Author("Bob")]
		public void Should_AutomaticallyMockPremitiveReturnMethod_When_TheNotSetupWithACallTo()
		{
			// Arrange
			IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
			IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

			// No mocking setup code needed if we just want the defaults! 
			// I personally think its better to be explicit about what you want to do and should use A.CallTo setup

			// Act
			var employeeService = new EmployeeService(employeeRepository, proxyMethods);
			var result = employeeService.DefaultPrimitiveType();

			// Assert
			result.ShouldBe(false); // <-- The default for bool is false. So if no mock setup we get the default value 

			A.CallTo(() => proxyMethods.ShouldReturnTrueForRestOfTestToSucceed()).MustHaveHappened();
		}

		[TestMethod]
		[TestCategory("Mocks")]
		[Author("Bob")]
		public void Should_AutomaticallyMockPremitiveReturnMethod_When_TheUsingSetupWithACallTo()
		{
			// Arrange
			IProxyMethods proxyMethods = A.Fake<IProxyMethods>();
			IEmployeeRepository employeeRepository = A.Fake<IEmployeeRepository>();

			A.CallTo(() => proxyMethods.ShouldReturnTrueForRestOfTestToSucceed()).Returns(true); // <-- With the setup we get control over the return value.

			// Act
			var employeeService = new EmployeeService(employeeRepository, proxyMethods);
			var result = employeeService.DefaultPrimitiveType();

			// Assert
			result.ShouldBe(true); // <-- The return value

			A.CallTo(() => proxyMethods.ShouldReturnTrueForRestOfTestToSucceed()).MustHaveHappened();
		}
	}
}
