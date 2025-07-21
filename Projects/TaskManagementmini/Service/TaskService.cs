using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskManagementmini.Models;
using TaskManagementmini.IService;
using TaskManagementmini.DBLayer;

    namespace TaskManagementmini.Service
{
    public class TaskService : ITaskService
    {
        private readonly IDBService _dbService;

        public TaskService(IDBService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IEnumerable<TaskFeilds>> GetAllTaskAsync()
        {
            return await _dbService.GetAllTaskFeildsAsync();
        }

        public async Task AddTaskAsync(TaskFeilds task)
        {
            await _dbService.AddTaskAsync(task);
        }

        public async Task DeleteTaskAsync(int Id)
        {
            await _dbService.DeleteTaskAsync(Id);
        }
    }
}
