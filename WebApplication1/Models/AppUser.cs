using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models;

public class AppUser : IdentityUser
{
    public DateTime BirthDate { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;
}