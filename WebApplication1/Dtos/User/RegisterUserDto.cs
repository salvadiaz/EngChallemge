using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.User;

public class RegisterUserDto
{
    [Required]
    public string? Username { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
    
    // public void SetBirthDateFromDateTime(DateTime dateTime)
    // {
    //     BirthDate = DateOnly.FromDateTime(dateTime);
    // }
}