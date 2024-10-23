namespace API.Models
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Company {  get; set; }
    }
}
