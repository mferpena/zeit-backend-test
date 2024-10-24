namespace Core.Domain.Models
{
    public class Todo
    {
        public string? Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool Completed { get; set; }
    }
}
