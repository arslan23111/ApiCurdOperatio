using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppToDo.IService;
using WebAppToDo.Models;
using WebAppToDo.DBLayer;

namespace WebAppToDo.Service
{
    public class TaskService : ITaskService
    {
        private readonly IDBServices _dbServices;
        public TaskService(IDBServices dbServices)
        {
            _dbServices = dbServices;
        }
        public async Task<IEnumerable<TaskItem>> GetAllTaskAsync()
        {
            return await _dbServices.GetAllTaskAsync();
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _dbServices.AddTaskAsync(task);
        }
        public async Task DeleteTaskAsyc(int Id)
        {
            await _dbServices.DeleteTaskAsyc(Id);
        }
        public async Task UpdateTaskAsync(TaskItem task)
        {
            await _dbServices.UpdateTaskAsync(task);
        }

        public async Task<TaskItem> GetTaskByIdAsync(int id)
        {
            return await _dbServices.GetTaskByIdAsync(id);
        }
    }
}
