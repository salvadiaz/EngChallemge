using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUser(AppUser user, string password);
    Task<IdentityResult> AddUserToRole(AppUser user, string role);
    Task<AppUser> UpdateActiveStatus(bool status, string userId);
    Task<AppUser> Delete(string userId);
    Task<List<AppUser>> GetAllActiveUsers();
    Task<AppUser?> GetByUsername(string username);
}