using Microsoft.AspNetCore.Mvc;
using TODOapi.Models;


namespace TODOapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static readonly List<TaskModel> Tasks = new();

        // Get all tasks
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(Tasks);
        }

        // Add a new task
        [HttpPost]
        public IActionResult AddTask([FromBody] TaskModel task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Tasks.Any(t => t.Name.Equals(task.Name, StringComparison.OrdinalIgnoreCase)))
                return BadRequest("A task with the same name already exists.");
            task.Id = Tasks.Any() ? Tasks.Max(t => t.Id) + 1 : 1;
            Tasks.Add(task);
            return Ok(task);
        }

        // Update a task
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel updatedTask)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Tasks.Any(t => t.Name.Equals(updatedTask.Name, StringComparison.OrdinalIgnoreCase) && t.Id != id))
                return BadRequest("A task with the same name already exists.");

            task.Name = updatedTask.Name;
            task.Priority = updatedTask.Priority;
            task.Status = updatedTask.Status;

            return Ok(task);
        }

        // Delete a completed task
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found.");

            if (task.Status != "Completed")
                return BadRequest("Only completed tasks can be deleted.");

            Tasks.Remove(task);
            return Ok(task);
        }
    }
}
