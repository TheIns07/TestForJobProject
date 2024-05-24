using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestForJobProject.Context;
using TestForJobProject.Models;

namespace TestForJobProject.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly DataContext _context;

        public EmployeeController(DataContext employeeService)
        {
            _context = employeeService;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> FillForm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            // Aquí redirige a una vista de formulario de edición y pasa el objeto empleado
            return View("Edit", employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,DOB,Salary,IsActive")] Employee employee)
        {
            // Convertir el valor del dropdown a un booleano
            employee.IsActive = employee.IsActive.Equals("true") ? true : false;

            if (ModelState.IsValid)
            {
                // Validar la fecha de nacimiento (DOB)
                DateTime minDateOfBirth = new DateTime(1950, 1, 1);
                DateTime maxDateOfBirth = DateTime.Today;
                if (employee.DOB < minDateOfBirth || employee.DOB > maxDateOfBirth)
                {
                    ModelState.AddModelError("DOB", $"Date of Birth must be between {minDateOfBirth.ToString("MM/dd/yyyy")} and {maxDateOfBirth.ToString("MM/dd/yyyy")}.");
                    return View(employee);
                }

                // Validar el rango de salario (Salary)
                decimal minSalary = 100;
                decimal maxSalary = 50000;
                if (employee.Salary < minSalary || employee.Salary > maxSalary)
                {
                    ModelState.AddModelError("Salary", $"Salary must be between {minSalary} and {maxSalary}.");
                    return View(employee);
                }

                // Validar si ya existe un empleado con el mismo nombre
                if (_context.Employees.Any(e => e.Name == employee.Name))
                {
                    ModelState.AddModelError("Name", "Employee with this name already exists.");
                    return View(employee);
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, [Bind("Id,Name,Address,DOB,Salary,IsActive")] Employee employee)
        {
            if (name != employee.Name)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }

}