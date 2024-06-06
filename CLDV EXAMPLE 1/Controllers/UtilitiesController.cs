using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace KhumaloCraft.Controllers
{
    public class UtilitiesController : Controller
    {
        private static List<TodoTask> _tasks = new List<TodoTask>();
        private static int _taskIdCounter = 1;

        public IActionResult Index()
        {
            return View(_tasks);
        }

        [HttpPost]
        public IActionResult AddTask(string title)
        {
            TodoTask newTask = new TodoTask
            {
                Id = _taskIdCounter++,
                Title = title,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            _tasks.Add(newTask);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkTaskAsCompleted(int id)
        {
            TodoTask task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            TodoTask task = _tasks.Find(t => t.Id == id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
            return RedirectToAction("Index");
        }
    }
}
