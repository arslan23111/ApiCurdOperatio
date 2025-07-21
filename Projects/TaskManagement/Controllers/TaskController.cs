using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using TaskManagement.Iservices;
using TaskManagement.Models;
using TaskManagement.services;

public class TaskController : Controller
{
    private readonly ITaskservices _taskservices;

    public TaskController(ITaskservices taskservices)
    {
        _taskservices = taskservices;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var tasks = await _taskservices.GetAllAsync();

            if (tasks == null || tasks.Count == 0)
                return NotFound("No tasks found.");

            return View(tasks);
        }
        catch (Exception ex)
        {
            // log exception here if needed
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    //[HttpGet]
    //public IActionResult Index()
    //{
    //    List<TaskItem> tasks = new List<TaskItem>();
    //    string conStr = _configuration.GetConnectionString("DefaultConnection");

    //    using (SqlConnection con = new SqlConnection(conStr))
    //    {
    //        string query = "SELECT * FROM Tasks";
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        con.Open();
    //        SqlDataReader reader = cmd.ExecuteReader();

    //        while (reader.Read())
    //        {
    //            tasks.Add(new TaskItem
    //            {
    //                Id = Convert.ToInt32(reader["Id"]),
    //                Title = reader["Title"].ToString(),
    //                Description = reader["Description"].ToString(),
    //                Date = Convert.ToDateTime(reader["Date"]),
    //                Status =  reader["Status"].ToString()
    //            });
    //        }
    //    }

    //    return View(tasks);
    //}

    //[HttpPost]

    //public IActionResult Create(TaskItem tmk)
    //{

    //    if (ModelState.IsValid)
    //    {
    //        string conStr = _configuration.GetConnectionString("DefaultConnection");
    //        using (SqlConnection con = new SqlConnection(conStr))
    //        {
    //            string query = "INSERT INTO Tasks (Title, Description, Date, Status) VALUES (@Title, @Description, @Date, @Status)";
    //            SqlCommand cmd = new SqlCommand(query, con);
    //            cmd.Parameters.AddWithValue("@Title", tmk.Title);
    //            cmd.Parameters.AddWithValue("@Description", tmk.Description);
    //            cmd.Parameters.AddWithValue("@Date", tmk.Date);
    //            cmd.Parameters.AddWithValue("@Status", tmk.Status);
    //            con.Open();
    //            cmd.ExecuteNonQuery();
    //        }
    //        return RedirectToAction("Create");
    //    }
    //    return View(tmk);

    //}
    //[HttpGet]
    //public IActionResult Create()
    //{
    //    return View();
    //}
    //[HttpGet]
    //public IActionResult Edit(int id)
    //{
    //    TaskItem task = new TaskItem();
    //    string conStr = _configuration.GetConnectionString("DefaultConnection");

    //    using (SqlConnection con = new SqlConnection(conStr))
    //    {
    //        string query = "SELECT * FROM Tasks WHERE Id = @Id";
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        cmd.Parameters.AddWithValue("@Id", id);
    //        con.Open();
    //        SqlDataReader reader = cmd.ExecuteReader();

    //        if (reader.Read())
    //        {
    //            task.Id = Convert.ToInt32(reader["Id"]);
    //            task.Title = reader["Title"].ToString();
    //            task.Description = reader["Description"].ToString();
    //            task.Date = Convert.ToDateTime(reader["Date"]);
    //            task.Status = reader["Status"].ToString();
    //        }
    //    }

    //    return View(task);
    //}

    //[HttpPost]
    //public IActionResult Edit(TaskItem task)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        string conStr = _configuration.GetConnectionString("DefaultConnection");

    //        using (SqlConnection con = new SqlConnection(conStr))
    //        {
    //            string query = @"UPDATE Tasks SET 
    //                         Title = @Title,
    //                         Description = @Description,
    //                         Date = @Date,
    //                         Status = @Status
    //                         WHERE Id = @Id";
    //            SqlCommand cmd = new SqlCommand(query, con);
    //            cmd.Parameters.AddWithValue("@Title", task.Title);
    //            cmd.Parameters.AddWithValue("@Description", task.Description);
    //            cmd.Parameters.AddWithValue("@Date", task.Date);
    //            cmd.Parameters.AddWithValue("@Status", task.Status);
    //            cmd.Parameters.AddWithValue("@Id", task.Id);
    //            con.Open();
    //            cmd.ExecuteNonQuery();
    //        }

    //        return RedirectToAction("Index");
    //    }

    //    return View(task);


    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Delete(int id)
    //{
    //    TaskItem task = new TaskItem();
    //    string conStr = _configuration.GetConnectionString("DefaultConnection");

    //    using (SqlConnection con = new SqlConnection(conStr))
    //    {
    //        string query = "DELETE FROM Tasks WHERE Id = @Id";
    //        SqlCommand cmd = new SqlCommand(query, con);
    //        cmd.Parameters.AddWithValue("@Id", id);
    //        con.Open();
    //        cmd.ExecuteNonQuery();


    //    }
    //    return RedirectToAction("Index");







    //}
}

