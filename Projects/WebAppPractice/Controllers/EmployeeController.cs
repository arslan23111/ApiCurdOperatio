using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebAppPractice.Models;  // replace with actual namespace

public class EmployeeController : Controller
{
    private readonly IConfiguration _configuration;

    public EmployeeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Create()
    {
        List<Employee> data = new List<Employee>();
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM EmployeeAttendance";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Employee
                {
                    
                    Name = dr["Name"].ToString(),
                    Date = Convert.ToDateTime(dr["Date"]),
                    Status = dr["Status"].ToString(),

                    Id= Convert.ToInt32(dr["id"])
                });
            }
        }

        return View(data); // Pass data to view
    }

    [HttpPost]
    public IActionResult Create(Employee emp)
    {
        if (ModelState.IsValid)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO EmployeeAttendance (Name, Date, Status) VALUES (@Name, @Date, @Status)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Date", emp.Date);
                cmd.Parameters.AddWithValue("@Status", emp.Status);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Create");
        }

        return View(new List<Employee>());
    }

    [HttpPost]
    
    public IActionResult Delete(int id)
    {
        string conStr = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "DELETE FROM EmployeeAttendance WHERE Id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        return RedirectToAction("Create");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        Employee emp = new Employee();
        string conStr = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT * FROM EmployeeAttendance WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                emp.Id = Convert.ToInt32(dr["Id"]);
                emp.Name = dr["Name"].ToString();
                emp.Date = Convert.ToDateTime(dr["Date"]);
                emp.Status = dr["Status"].ToString();
            }
        }

        return View(emp);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Employee emp)
    {
        string conStr = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "UPDATE EmployeeAttendance SET Name=@Name, Date=@Date, Status=@Status WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Date", emp.Date);
            cmd.Parameters.AddWithValue("@Status", emp.Status);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        return RedirectToAction("Create"); // List me wapas le jao
    }


}
