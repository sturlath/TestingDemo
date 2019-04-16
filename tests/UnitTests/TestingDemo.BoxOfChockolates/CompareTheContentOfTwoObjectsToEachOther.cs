using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.TestFramework.Extensions.AttributeEx;
using System.Collections.Generic;
using FluentAssertions;

namespace TestingDemo.BoxOfChockolates
{
	// Unit test naming convention: Should_ExpectedBehavior_When_StateUnderTest

	[TestClass]
	[TestCategory("UnitTests")]
	public class CompareTheContentOfTwoObjectsToEachOther
	{
		[TestMethod]
		[Author("Bob")]
		public void Should_FindOutThatValuesInTwoClassesAreTheSame_When_TheyAreTheSame()
		{
			// First class 
			Company first = new Company() { Id = 1, Name = "Big Inc." };
			first.Employees.Add(new Employee() { Id = 1, Name = "John", Age = 21 });
			first.Employees.Add(new Employee() { Id = 2, Name = "Bill", Age = 33 });

			// second class
			Company second = new Company() { Id = 1, Name = "Big Inc." };
			second.Employees.Add(new Employee() { Id = 1, Name = "John", Age = 21 });
			second.Employees.Add(new Employee() { Id = 2, Name = "Bill", Age = 33 });

			// Tests if the VALUES differ by using FluentAssertions (https://fluentassertions.com/)
			first.Should().BeEquivalentTo(second);
		}

		//[TestMethod] //<-- Can't run this test since it always fails on BeEquivalentTo()
		[Author("Bob")]
		public void Should_FindOutThatValuesInTwoClassesAreNotTheSame_When_TheyAreNotTheSame()
		{
			// First class 
			Company first = new Company() { Id = 1, Name = "Big Inc." };
			first.Employees.Add(new Employee() { Id = 1, Name = "John", Age = 21 }); // <-- Lets change this value in the second class
			first.Employees.Add(new Employee() { Id = 2, Name = "Bill", Age = 33 });

			// second class
			Company second = new Company() { Id = 1, Name = "Big Inc." };
			second.Employees.Add(new Employee() { Id = 1, Name = "John", Age = 66 }); //<-- Will fail here (21 != 66)
			second.Employees.Add(new Employee() { Id = 2, Name = "Bill", Age = 33 });

			// Tests if the VALUES differ by using FluentAssertions (https://fluentassertions.com/)
			first.Should().BeEquivalentTo(second);
		}

		// Test classes to try out on.
		public class Company
		{
			public int Id { get; set; }
			public string Name { get; set; }

			public List<Employee> Employees { get; set; } = new List<Employee>();
		}

		public class Employee
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int Age { get; set; }
		}
	}
}
