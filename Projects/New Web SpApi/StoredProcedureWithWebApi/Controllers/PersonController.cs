using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using StoredProcedureWithWebApi.Models;

namespace StoredProcedureWithWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PersonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody] Person person)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("InsertStu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", person.Id);
                cmd.Parameters.AddWithValue("@Name", person.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Age", person.Age);
                cmd.Parameters.AddWithValue("@Salary", person.Salary);


                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return Ok("Inserted successfully.");
                else
                    return BadRequest("Insert failed.");
            }
        }

        [HttpGet]

        public IActionResult GetAllPersons()
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetAllStu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                List<Person> persons = new List<Person>();
                while (reader.Read())
                {
                    Person person = new Person
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                        Age = reader.GetInt32(2),
                        Salary = reader.GetInt32(2)
                    };
                    persons.Add(person);
                }
                return Ok(persons);
            }
        }

        [HttpPut]

        public IActionResult UpdatePerson([FromBody] Person person)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("updateStu", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", person.Id);
                cmd.Parameters.AddWithValue("@Name", person.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Age", person.Age);
                cmd.Parameters.AddWithValue("@Salary", person.Salary);


                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return Ok("Inserted successfully.");
                else
                    return BadRequest("Insert failed.");
            }


        }

        [HttpDelete("{Id}")]

        public IActionResult DeletePerson(int Id)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteStu", con))
                {


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id",Id);

                    con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        return Ok("Rrcord delete successful.");
                    }
                    else
                    {
                        return BadRequest("Record not found.");
                    }
                }





            }
        }
    }
}

