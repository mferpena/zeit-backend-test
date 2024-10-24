using Core.Domain.Models;
using Core.Ports;

namespace Core.UseCases.Impl
{
    public class TodoServiceImpl : TodoUseCase
    {
        private readonly TodoPersistencePort _todoRepository;

        public TodoServiceImpl(TodoPersistencePort todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void CreateTask(Todo todo)
        {
            if (string.IsNullOrEmpty(todo.Title))
            {
                throw new ArgumentException("El título no puede estar vacío.");
            }

            todo.Id = Guid.NewGuid().ToString();
            todo.Completed = false;

            _todoRepository.AddTask(todo);
        }

        public void DeleteTask(string id)
        {
            var task = _todoRepository.GetTaskById(id);
            if (task == null)
            {
                throw new KeyNotFoundException("La tarea no existe.");
            }

            _todoRepository.DeleteTask(id);
        }

        public IEnumerable<Todo> GetAllTasks()
        {
            return _todoRepository.GetAllTasks();
        }

        public Todo GetTaskById(string id)
        {
            var task = _todoRepository.GetTaskById(id);
            if (task == null)
            {
                throw new KeyNotFoundException("La tarea no existe.");
            }

            return task;
        }

        public void UpdateTask(string id, Todo todo)
        {
            var existingTask = _todoRepository.GetTaskById(id);
            if (existingTask == null)
            {
                throw new KeyNotFoundException("La tarea no existe.");
            }

            existingTask.Title = todo.Title;
            existingTask.Description = todo.Description;
            existingTask.Completed = todo.Completed;

            _todoRepository.UpdateTask(existingTask);
        }
    }
}