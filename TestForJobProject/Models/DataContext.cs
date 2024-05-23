using Microsoft.EntityFrameworkCore;
using TestForJobProject.Models;

namespace TestForJobProject.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
           .Property(e => e.Id)
           .ValueGeneratedOnAdd();
        }
    }
}
