using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using TestForJobProject.Context;
using TestForJobProject.Interfaces;

namespace TestForJobProject.Models
{
    public class EmployeeService : IEmployeeService
    {
        private DataContext _context;
        
        public EmployeeService(DataContext context)
        {
            _context = context;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        public bool CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
            return Save();
        }

        public ICollection<Employee> GetEmployees()
        {
            return _context.Employees.ToList();

        }

        public bool UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return Save();
        }

        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(c => c.Id == id);
        }

        public Employee GetEmployee(int id)
        {
            return _context.Employees.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
