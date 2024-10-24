namespace Infrastructure.Primary.DTO
{
    public class SignUpDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required List<string> Roles { get; set; }
    }
}
