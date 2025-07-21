using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebAppToDo.IService;
using WebAppToDo.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;

namespace WebAppToDo.DBLayer
{
    public class DBServices : IDBServices
    {
        private readonly IConfiguration _configuration;
        public DBServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<TaskItem>> GetAllTaskAsync()
        {
            var tasks = new List<TaskItem>();
            string? connString = _configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connString))
            {
                throw new InvalidOperationException("Connection string is null or empty.");
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students", conn);
                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    tasks.Add(new TaskItem
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"]),
                    });
                }
            }
            return tasks;

        }
        public async Task AddTaskAsync(TaskItem task)
        {


            String connString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {


                var query = "INSERT INTO Students (Title, IsCompleted) VALUES (@Title, @IsCompleted)";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                await cmd.ExecuteNonQueryAsync();

            }

        }


        public async Task DeleteTaskAsyc(int Id)
        {

            String connString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {


                SqlCommand cmd = new SqlCommand("Delete FROM Students Where Id = @Id", conn);

                conn.Open();
                cmd.Parameters.AddWithValue("@Id", Id);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            string connString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                var query = "UPDATE Students SET Title = @Title, IsCompleted = @IsCompleted WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                cmd.Parameters.AddWithValue("@Id", task.Id);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            TaskItem task = null;
            string connString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.Read())
                {
                    task = new TaskItem
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString(),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                    };
                }
            }

            return task;
        }

    }


}

