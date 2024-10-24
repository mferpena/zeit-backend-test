using Core.Domain.Models;

namespace Core.Ports
{
    public interface TodoPersistencePort
    {
        Todo GetTaskById(string id);
        IEnumerable<Todo> GetAllTasks();
        void AddTask(Todo todo);
        void UpdateTask(Todo todo);
        void DeleteTask(string id);
    }
}