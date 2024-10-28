using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.User;

public class UpdateStatusDto
{
    [Required]
    public bool Status { get; set; }
}