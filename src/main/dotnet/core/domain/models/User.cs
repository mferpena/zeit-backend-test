namespace Core.Domain.Models
{
    public class User
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public List<string>? Roles { get; set; }
    }
}
