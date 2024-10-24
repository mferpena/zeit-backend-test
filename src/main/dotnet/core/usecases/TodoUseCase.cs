using Core.Domain.Models;

namespace Core.UseCases
{
    public interface TodoUseCase
    {
        Todo GetTaskById(string id);
        IEnumerable<Todo> GetAllTasks();
        void CreateTask(Todo todo);
        void UpdateTask(string id, Todo todo);
        void DeleteTask(string id);
    }
}
