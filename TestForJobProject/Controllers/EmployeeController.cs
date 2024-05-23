using Microsoft.AspNetCore.Mvc;
using TestForJobProject.Interfaces;
using TestForJobProject.Models;

namespace TestForJobProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var categories = _employeeService.GetEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest(ModelState);

            var employeeFind = _employeeService.GetEmployees()
                .Where(c => c.Name.Trim().ToUpper() == employee.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (employeeFind != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeService.CreateEmployee(employee))
            {
                ModelState.AddModelError("", "Something went wrong while saving the employee");
                return StatusCode(500, ModelState);
            }

            return Ok("Employee successfully created");
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] Employee employee)
        {
            if (employee == null)
            {
                ModelState.AddModelError("", "Please add valid info");
                return BadRequest(ModelState);
            }

            if (employeeId != employee.Id)
            {
                ModelState.AddModelError("", "Employee doesn't exist");
                return BadRequest(ModelState);
            }
               

            if (!_employeeService.EmployeeExists(employeeId))
            {
                ModelState.AddModelError("", "Employee already exists");
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_employeeService.UpdateEmployee(employee))
            {
                ModelState.AddModelError("", "Something went wrong updating the employee");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employeeService.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            var employeeToDelete = _employeeService.GetEmployee(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeService.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting employee");
            }

            return NoContent();
        }
    }
}
