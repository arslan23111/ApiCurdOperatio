using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskManagementmini.Models;

namespace TaskManagementmini.IService

{
    public interface ITaskService
    {
        Task<IEnumerable<TaskFeilds>> GetAllTaskAsync();
        Task AddTaskAsync(TaskFeilds task);

        Task DeleteTaskAsync(int Id);

    }
}
