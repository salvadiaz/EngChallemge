namespace WebApplication1.Dtos.User;

public class NewUserDto
{
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public bool Active { get; set; }
}