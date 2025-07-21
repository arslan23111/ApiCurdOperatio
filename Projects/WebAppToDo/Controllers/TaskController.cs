using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppToDo.IService;
using WebAppToDo.Models;

namespace WebAppToDo.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        // Constructor: Dependency Injection se service milegi
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Step 1: Get All Tasks method banayenge 👇
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTaskAsync();
            return View(tasks);
        }

        // Step 2: Add Task method banayenge 👇
        [HttpGet]
        public IActionResult Create()
        {
            return View(new TaskItem());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }

            await _taskService.AddTaskAsync(task);
            return RedirectToAction(nameof(Index));  // Wapis Index pe jao
        }

        // Step 3: Delete Task method banayenge 👇

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            await _taskService.DeleteTaskAsyc(id);
            return RedirectToAction(nameof(Index));  // Wapis Index pe jao

        }
        // GET: Task/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Task/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }

            await _taskService.UpdateTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }

    }
}
// Step 2 me method banayenge 👇
    


   