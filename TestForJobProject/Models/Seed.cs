using TestForJobProject.Models;

namespace TestForJobProject.Context
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if(!dataContext.Employees.Any())
            {
                var employees = new List<Employee>()
                {
                     new Employee
                    {
                        Name = "Manuel Rodriguez",
                        Address = "123 Main St",
                        DOB = new DateTime(1980, 6, 15),
                        Salary = 45000.50m,
                        IsActive = true
                    },
                    new Employee
                    {
                        Name = "Rose Manuel",
                        Address = "456 Elm St",
                        DOB = new DateTime(1995, 3, 20),
                        Salary = 55000.75m,
                        IsActive = false
                    },
                    new Employee
                    {
                        Name = "Michael Johnson",
                        Address = "789 Oak St",
                        DOB = new DateTime(1978, 10, 10),
                        Salary = 35000.25m,
                        IsActive = true
                    }
                };
                dataContext.Employees.AddRange(employees);
                dataContext.SaveChanges();
            }
        }
    }
}
