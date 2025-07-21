using WebAppToDo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WebAppToDo.IService
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllTaskAsync();
        Task AddTaskAsync(TaskItem task);
        Task DeleteTaskAsyc(int Id);
        Task UpdateTaskAsync(TaskItem task);
        Task<TaskItem> GetTaskByIdAsync(int id); // Added missing method definition
    }
}
