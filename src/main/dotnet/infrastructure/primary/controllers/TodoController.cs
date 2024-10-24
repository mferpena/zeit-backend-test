using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using Core.Domain.Models;
using Infrastructure.Primary.DTO;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoUseCase _todoUseCase;

        public TodoController(TodoUseCase todoUseCase)
        {
            _todoUseCase = todoUseCase;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _todoUseCase.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(string id)
        {
            var task = _todoUseCase.GetTaskById(id);
            if (task == null)
            {
                return NotFound(new { Message = "La tarea no existe." });
            }
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] CreateTaskRequest request)
        {
            var todo = new Todo
            {
                Title = request.Title,
                Description = request.Description
            };

            _todoUseCase.CreateTask(todo);
            return Ok(new { Message = "Tarea creada exitosamente." });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(string id, [FromBody] UpdateTaskRequest request)
        {
            var task = _todoUseCase.GetTaskById(id);
            if (task == null)
            {
                return NotFound(new { Message = "La tarea no existe." });
            }

            var updatedTask = new Todo
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Completed = request.Completed
            };

            _todoUseCase.UpdateTask(id, updatedTask);
            return Ok(new { Message = "Tarea actualizada exitosamente." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(string id)
        {
            var task = _todoUseCase.GetTaskById(id);
            if (task == null)
            {
                return NotFound(new { Message = "La tarea no existe." });
            }

            _todoUseCase.DeleteTask(id);
            return Ok(new { Message = "Tarea eliminada exitosamente." });
        }
    }
}