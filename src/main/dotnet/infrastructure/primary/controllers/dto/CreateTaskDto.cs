namespace Infrastructure.Primary.DTO
{
    public class CreateTaskRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
