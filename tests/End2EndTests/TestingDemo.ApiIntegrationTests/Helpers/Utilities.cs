using TestingDemo.Core.Entities;
using TestingDemo.Repository;

namespace TestingDemo.ApiIntegrationTests
{
    public class Utilities
    {
        internal static void InitializeDbForTests(ApplicationDbContext context)
        {
            using (context)
            {
                AddEmployeeData(context);

                // Saved
                context.SaveChanges();
            }
        }

        private static void AddEmployeeData(ApplicationDbContext context)
        {
            Employee emp1 = new Employee() { Age = 27, Name = "Gilbert" };
            Employee emp2 = new Employee() { Age = 37, Name = "Hjálmar" };
            Employee emp3 = new Employee() { Age = 47, Name = "Unnbjörn" };
            Employee emp4 = new Employee() { Age = 57, Name = "Rafael" };
            Employee emp5 = new Employee() { Age = 67, Name = "Gunnar" };
            Employee emp6 = new Employee() { Age = 77, Name = "Jónas" };
            Employee emp7 = new Employee() { Age = 87, Name = "Friðbjörn" };
            Employee emp8 = new Employee() { Age = 97, Name = "Jósafat" };

            context.Employees.Add(emp1);
            context.Employees.Add(emp2);
            context.Employees.Add(emp3);
            context.Employees.Add(emp4);
            context.Employees.Add(emp5);
            context.Employees.Add(emp6);
            context.Employees.Add(emp7);
            context.Employees.Add(emp8);
        }
    }
}
