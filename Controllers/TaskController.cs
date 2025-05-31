using Microsoft.AspNetCore.Mvc;
using TaskTracker.Models;

namespace TaskTracker.Controllers
{
    public class TaskController : Controller
    {
        private static List<TaskItem> _tasks = new();
        private static int _idCounter = 1;

        public IActionResult Index() => View(_tasks);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(string description)
        {
            _tasks.Add(new TaskItem { Id = _idCounter++, Description = description, IsCompleted = false });
            return RedirectToAction("Index");
        }

        public IActionResult Complete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.IsCompleted = true;

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, string description)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Description = description;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            _tasks.Remove(task);
            return RedirectToAction("Index");
        }
    }
}
