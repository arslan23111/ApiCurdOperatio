using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagementmini.IService;

using TaskManagementmini.Models;


namespace TaskManagementmini.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTaskAsync();
            return View(tasks);
        }

        // GET: Task/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskFeilds task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }

            await _taskService.AddTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }

}

