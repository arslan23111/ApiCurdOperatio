using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using EmployeeAttendanceApi.Models;
using System;
using System.Data;

namespace StoredProcedureWithWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AttendanceController (IConfiguration configuration) { 
      
            _configuration = configuration;
  
            }
        [HttpPost]
        public IActionResult AddPerson([FromBody] Employee attendance)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertAttendance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@Id", attendance.Id);
                cmd.Parameters.AddWithValue("@Name", attendance.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Date", attendance.Date);
                cmd.Parameters.AddWithValue("@Status", attendance.Status);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0 ? Ok("Inserted") : BadRequest("Insert Failed");
            }
        }

        [HttpGet]
        public IActionResult GetAllPersons()
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetAllAttendance", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<Employee>attendances  = new List<Employee>();

                while (reader.Read())
                {
                    Employee attendance = new Employee
                    {

                  //      Id = reader.GetInt32(1),
                        Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Date = reader.GetDateTime(2),
                        Status = reader.IsDBNull(3) ? null : reader.GetString(3)
                    };
                    attendances.Add(attendance);
                }
                return Ok(attendances);
            }
        }

        //[HttpPut]
        //public IActionResult UpdatePerson([FromBody]  Attendance)
        //{
        //    string cs = _configuration.GetConnectionString("DefaultConnection");
        //    using (SqlConnection con = new SqlConnection(cs))
        //    {
        //        SqlCommand cmd = new SqlCommand("UpdateStu", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Id", person.Id);
        //        cmd.Parameters.AddWithValue("@Name", person.Name ?? (object)DBNull.Value);
        //        cmd.Parameters.AddWithValue("@Age", person.Age);
        //        cmd.Parameters.AddWithValue("@Salary", person.Salary);

        //        con.Open();
        //        int rows = cmd.ExecuteNonQuery();

        //        return rows > 0 ? Ok("Updated") : BadRequest("Update Failed");
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("DeleteAttendance", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return rows > 0 ? Ok("Deleted") : NotFound("Not Found");
            }
        }
    }
}
