using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    
    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityResult> RegisterUser(AppUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }
    
    public async Task<IdentityResult> AddUserToRole(AppUser user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }
    
    public async Task<AppUser> UpdateActiveStatus(bool status, string userId)
    {
        var existentUser = await GetUserById(userId);
        if (existentUser == null) return null;
        
        existentUser.Active = status;
        await _userManager.UpdateAsync(existentUser);

        return existentUser;
    }
    
    private async Task<AppUser?> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<AppUser> Delete(string userId)
    {
        var existentUser = await GetUserById(userId);
        if (existentUser == null) return null;
        
        await _userManager.DeleteAsync(existentUser);
        return existentUser;
    }
    
    public async Task<List<AppUser>> GetAllActiveUsers()
    {
        return await _userManager.Users.Where(u => u.Active == true).ToListAsync();
    }

    public async Task<AppUser?> GetByUsername(string username)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}