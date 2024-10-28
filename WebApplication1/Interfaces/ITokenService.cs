using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}