using TaskManagement.Models;

namespace TaskManagement.Iservices
{
    public interface ITaskservices
    {
        Task<List<TaskItem>> GetAllAsync();
    }
}
