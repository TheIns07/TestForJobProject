using Microsoft.AspNetCore.Mvc;
using TestForJobProject.Models;

namespace TestForJobProject.Interfaces
{
    public interface IEmployeeService
    {
        bool CreateEmployee(Employee employee);
        ICollection<Employee> GetEmployees();
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        bool Save();
        Employee GetEmployee(int id);
        bool EmployeeExists(int id);

    }
}
