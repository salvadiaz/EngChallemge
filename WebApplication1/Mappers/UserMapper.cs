using WebApplication1.Dtos.User;
using WebApplication1.Models;

namespace WebApplication1.Mappers;

public static class UserMapper
{
    public static NewUserDto toDtoFromCreated(this AppUser user)
    {
        return new NewUserDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            BirthDate = user.BirthDate,
            Active = user.Active
        };
    }
    
    public static NewUserDto toDto(this AppUser user)
    {
        return new NewUserDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            BirthDate = user.BirthDate,
            Active = user.Active
        };
    }
    
    public static LoggedUserDto ToLoggedUserDto(this AppUser user, string token)
    {
        return new LoggedUserDto
        {
            Username = user.UserName,
            Token = token
        };
    }
    
    public static AppUser toEntity(this RegisterUserDto userDto)
    {
        return new AppUser
        {
            UserName = userDto.Username,
            Email = userDto.Email,
            BirthDate = userDto.BirthDate
        };
    }
}