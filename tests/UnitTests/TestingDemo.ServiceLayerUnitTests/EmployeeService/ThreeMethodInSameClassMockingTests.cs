using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using Shouldly;
using TestingDemo.ServiceLayer;

namespace TestingDemo.ServiceLayerUnitTests
{
	//Unit test naming convention: Should_ExpectedBehavior_When_StateUnderTest

	/// <summary>
	/// Test to show how you can mock methods in the same class. 
	/// E.g if one method in a class calls another method you like to mock out.
	/// </summary>
	[TestClass]
	[TestCategory("UnitTests")]
	public class ThreeMethodInSameClassMockingTests
	{
		/*******************************************************************************/
		/*  A detailed explanation on how this all works is at the bottom of this page */
		/*******************************************************************************/

		[TestMethod]
		[TestCategory("Mocks")]
		[Author("Bob")]
		public void Should_ReturnFalse_When_FirstMethodIsCalled()
		{
			/* NOTE: To make this work the methods you are mocking need to be marked with the VIRTUAL keyword */

			// Arrange 
			var mockedEmployeeService = A.Fake<EmployeeService>(); //<-- Not an Interface but concrete class

			A.CallTo(() => mockedEmployeeService.FirstMethod()).CallsBaseMethod();  //<-- Tells fake FirtMethod() to call the real FirstMethod()
			A.CallTo(() => mockedEmployeeService.SecondMethod()).Returns(false);    //<-- And then when we hit the SecondMehtod we return FALSE instead of the TRUE that the real one does.
			A.CallTo(() => mockedEmployeeService.ThirdMethod()).DoesNothing();      //<-- Will never throw the exception in the method.

			// Act
			var result = mockedEmployeeService.FirstMethod(); // <-- Use the mocked object to call (not the concrete one that we usually do)

			// Assert
			result.ShouldBe(false);
			A.CallTo(() => mockedEmployeeService.SecondMethod()).MustHaveHappened(); //<-- check that we called SecondMethod
		}

		[TestMethod]
		[TestCategory("Mocks")]
		[Author("Bob")]
		public void Should_ReturnFalse_When_FirstMethodIsCalled_DifferentSettup()
		{
			/* NOTE: To make this work the methods you are mocking need to be marked with the VIRTUAL keyword */

			// Arrange 
			var mockedEmployeeService = A.Fake<EmployeeService>(options => options.CallsBaseMethods()); //<-- You don't need to set this up for every method explicitly like in the test above.

			A.CallTo(() => mockedEmployeeService.SecondMethod()).Returns(false);    //<-- And then when we hit the SecondMehtod we return FALSE instead of the TRUE that the real one does.
			A.CallTo(() => mockedEmployeeService.ThirdMethod()).DoesNothing();      //<-- Will never throw the exception in the method.

			// Act
			var result = mockedEmployeeService.FirstMethod(); // <-- Use the mocked object to call (not the concrete one that we usually do)

			// Assert
			result.ShouldBe(false);
			A.CallTo(() => mockedEmployeeService.SecondMethod()).MustHaveHappened(); //<-- check that we called SecondMethod
		}
	}
}
