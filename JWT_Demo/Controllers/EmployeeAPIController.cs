using JWT_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAPIController : ControllerBase
    {
        private readonly EmployeeDbContext employeeDbContext;

        public EmployeeAPIController(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(employeeDbContext.EmployeeData.ToList());
        }

        [Authorize]
        [HttpGet]
        [Route("GetByID")]
        public IActionResult Get(int Id)
        {
            var employee = employeeDbContext.EmployeeData.Where(x => x.Id == Id).FirstOrDefault();
            if(employee == null)
                return NoContent();

            return Ok(employee);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(EmployeeUI employeeUI)
        {
            if (employeeUI == null)
                return BadRequest();

            if (employeeUI.Name == "")
                return BadRequest();

            var employee = new Employee()
            {
                Name = employeeUI.Name,
            };
            employeeDbContext.EmployeeData.Add(employee);
            employeeDbContext.SaveChanges();
            return Ok(employee);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            employeeDbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            employeeDbContext.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var employee = employeeDbContext.EmployeeData.Where(x => x.Id == Id).FirstOrDefault();
            if (employee == null)
                return NoContent();

            employeeDbContext.EmployeeData.Remove(employee);
            employeeDbContext.SaveChanges();
            return Ok();
        }
    }
}
