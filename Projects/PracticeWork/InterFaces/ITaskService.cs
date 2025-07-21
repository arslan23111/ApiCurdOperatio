using System;
using SimpleTaskApp.Models;
namespace SimpleTaskApp.InterFaces
{
    public interface ITaskService
    {
        Task<List<TaskItme>> GetAllTasksAsync(); // Retrieves all tasks asynchronously YE method sub task ko lis me return karega
    }
}
