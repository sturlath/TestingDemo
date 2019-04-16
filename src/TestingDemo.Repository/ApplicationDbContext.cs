using Microsoft.EntityFrameworkCore;
using TestingDemo.Core.Entities;

namespace TestingDemo.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();
        }
    }
}