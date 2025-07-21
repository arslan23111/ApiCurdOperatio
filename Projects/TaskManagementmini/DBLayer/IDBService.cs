using System;
using System.Collections.Generic;
using TaskManagementmini.Models;
using System.Threading.Tasks;

namespace TaskManagementmini.DBLayer
{
    public interface IDBService
    {
        Task<IEnumerable<TaskFeilds>> GetAllTaskFeildsAsync();
        Task<TaskFeilds> GetTaskByIdAsync(int Id);
        Task AddTaskAsync(TaskFeilds task);
        //Task UpdateTaskAsync(TaskFeilds task); // Uncommented the method
        Task DeleteTaskAsync(int Id);
    }
}
