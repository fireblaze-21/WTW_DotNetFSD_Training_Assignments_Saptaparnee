using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class TasksController : Controller
    {
        private static List<TaskItem> _tasks = new List<TaskItem>();
        private static int _nextId = 1;

        // GET: /Tasks
        public IActionResult Index()
        {
            return View(_tasks);
        }

        // GET: /Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                task.Id = _nextId++;
                _tasks.Add(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: /Tasks/Edit/5
        public IActionResult Edit(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TaskItem updatedTask)
        {
            if (id != updatedTask.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(updatedTask);

            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.DueDate = updatedTask.DueDate;
            task.Priority = updatedTask.Priority;

            return RedirectToAction(nameof(Index));
        }

        // GET: /Tasks/Delete/5
        public IActionResult Delete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            return View(task);
        }

        // POST: /Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                _tasks.Remove(task);

            return RedirectToAction(nameof(Index));
        }
    }
}

