using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication10.Model;


namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class DeptApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeptApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DeptApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dept>>> GetAllDepts()
        {
            var allDepts = await _context.Depts.ToListAsync();
            return Ok(allDepts);
        }

        // GET: api/DeptApi/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Dept>> GetDeptById(int id)
        {
            var dept = await _context.Depts.FindAsync(id);
            if (dept == null)
            {
                return NotFound("Requested Department not found");
            }
            return Ok(dept);
        }

        // POST: api/DeptApi
        [HttpPost]
        public async Task<ActionResult<Dept>> CreateDept(Dept dept)
        {
            if (dept == null)
            {
                return BadRequest("Invalid department data");
            }

            await _context.Depts.AddAsync(dept);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeptById), new { id = dept.Deptno }, dept);
        }

        // PUT: api/DeptApi/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDept(int id, Dept dept)
        {
            if (id != dept.Deptno)
            {
                return BadRequest("Department ID mismatch");
            }

            var existingDept = await _context.Depts.FindAsync(id);
            if (existingDept == null)
            {
                return NotFound("Department not found");
            }

            // Update scalar properties only
            existingDept.Dname = dept.Dname;
            existingDept.Loc = dept.Loc;


            await _context.SaveChangesAsync();

            return Ok(new { Status = "Department updated successfully" });
        }
        // DELETE: api/DeptApi/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDept(int id)
        {
            var dept = await _context.Depts.FindAsync(id);
            if (dept == null)
            {
                return NotFound("Requested Department not found");
            }

            _context.Depts.Remove(dept);
            await _context.SaveChangesAsync();

            return Ok(new { Status = "Department deleted successfully" });
        }
    }
}
