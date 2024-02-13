using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using JWT_Demo.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace JWT_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        [Route("GetEmployees")]
        public string GetEmployees()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("EmployeeCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM EmployeeData", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Employee> EmpList = new List<Employee>();
            Response response = new Response();
            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    emp.Name = Convert.ToString(dt.Rows[i]["Name"]);
                    EmpList.Add(emp);
                }
            }
            if(EmpList.Count > 0)
            {
                return JsonConvert.SerializeObject(EmpList);
            } else
            {
                response.StausCode = 100;
                response.ErrorMessage = "Data Not Found";
                return JsonConvert.SerializeObject(response);
            }
            //return "Get Employees";
        }

        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenticated with JWT";
        }

        [HttpGet]
        [Route("Details")]
        public string Details()
        {
            return "Authenticated with JWT";
        }

        [Authorize]
        [HttpPost]
        public string AddUser(Users user)
        {
            return "User added with username " + user.Username;
        }
    }
}
