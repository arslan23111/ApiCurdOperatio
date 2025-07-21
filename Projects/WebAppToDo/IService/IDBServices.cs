using WebAppToDo.Models;

namespace WebAppToDo.IService
{
    public interface IDBServices
    {
       
        Task<IEnumerable<TaskItem>> GetAllTaskAsync();

        Task AddTaskAsync(TaskItem task);

        Task DeleteTaskAsyc(int Id);

        Task UpdateTaskAsync(TaskItem task);

        Task<TaskItem> GetTaskByIdAsync(int id);


    }
}
