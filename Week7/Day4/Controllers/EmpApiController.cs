using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication10.Model;

namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EmpApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emp>>> GetAllEmployees()
        {
            var allEmployees = await _context.Emps.ToListAsync();
            return Ok(allEmployees);
        }

        // GET: api/EmpApi/5
        [HttpGet("{id}")]

        public async Task<ActionResult<Emp>> GetEmployeeById(int id)
        {
            var employee = await _context.Emps.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Requested Employee not available");
            }
            return Ok(employee);
        }

        // POST: api/EmpApi
        [HttpPost]
        public async Task<ActionResult<Emp>> CreateEmployee(Emp emp)
        {
            if (emp == null)
            {
                return BadRequest("Invalid employee data");
            }
            await _context.Emps.AddAsync(emp);
            await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetEmployeeById), new { id = emp.Id }, emp);
            return Ok(new { Status = "Employee created successfully" });
        }

        // PUT: api/EmpApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Emp emp)
        {
            if (id != emp.Id)
            {
                return BadRequest("Employee ID mismatch");
            }
            _context.Emps.Update(emp);
            await _context.SaveChangesAsync();
            return Ok(new { Status = "Employee updated successfully" });
        }

        // DELETE: api/EmpApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Emps.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Requested Employee not found ");
            }
            _context.Emps.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok(new { Status = "Employee deleted successfully" });
        }
    }
}
