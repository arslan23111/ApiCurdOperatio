using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using TaskManagementmini.Models;
using System.Threading.Tasks;
namespace TaskManagementmini.DBLayer
{
    public class DBService : IDBService
    {
        private readonly IConfiguration _configuration;




        public DBService(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public async Task<IEnumerable<TaskFeilds>> GetAllTaskFeildsAsync()
        {
            var tasks = new List<TaskFeilds>();
                    string? connString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tasks1", conn);
                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {

                    tasks.Add(new TaskFeilds
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString() ?? string.Empty,
                        IsComplete =  reader["IsComplete"].ToString() == "1"
                    });



                }

                return tasks;
            }

        }
        public async Task AddTaskAsync(TaskFeilds task)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var query = "INSERT INTO Tasks1 (Title, IsComplete) VALUES (@Title, @IsComplete)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@IsComplete", task.IsComplete);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<TaskFeilds> GetTaskByIdAsync(int Id)
        {
            string? connString = _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tasks1 WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", Id);

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    return new TaskFeilds
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString() ?? string.Empty,
                        IsComplete = Convert.ToBoolean(reader["IsComplete"])
                    };
                }

                return null; // Return null if no task is found
            }
        }
        public async Task DeleteTaskAsync(int id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var query = "DELETE FROM Tasks1 WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
                    