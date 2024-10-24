namespace Infrastructure.Primary.DTO
{
    public class UpdateTaskRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool Completed { get; set; }
    }
}