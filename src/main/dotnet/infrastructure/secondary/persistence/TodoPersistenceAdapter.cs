using Core.Domain.Models;
using Core.Ports;

namespace Infrastructure.Persistence
{
    public class TodoPersistenceAdapter : TodoPersistencePort
    {
        private readonly List<Todo> _todos;

        public TodoPersistenceAdapter()
        {
            _todos = new List<Todo>();
        }

        public Todo GetTaskById(string id)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Todo> GetAllTasks()
        {
            return _todos.ToList();
        }

        public void AddTask(Todo todo)
        {
            _todos.Add(todo);
        }

        public void UpdateTask(Todo todo)
        {
            var existingTask = GetTaskById(todo.Id);
            if (existingTask != null)
            {
                existingTask.Title = todo.Title;
                existingTask.Description = todo.Description;
                existingTask.Completed = todo.Completed;
            }
        }

        public void DeleteTask(string id)
        {
            var task = GetTaskById(id);
            if (task != null)
            {
                _todos.Remove(task);
            }
        }
    }
}
