using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable;
using Moq;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Tests.Services;

[TestClass]
[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    private UserService _service;
    private Mock<UserManager<AppUser>> _userManagerMock;

    [TestInitialize]
    public void Setup()
    {
        var store = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(
            store.Object, null, null, null, null, null, null, null, null);

        _service = new UserService(_userManagerMock.Object);
    }

    [TestMethod]
    public async Task GetAllActiveUsers_ShouldReturnActiveUsers()
    {
        var users = new List<AppUser>
        {
            new AppUser { UserName = "testuser1", Active = true },
            new AppUser { UserName = "testuser2", Active = true },
            new AppUser { UserName = "testuser3", Active = false }
        }.AsQueryable().BuildMock();

        _userManagerMock.Setup(x => x.Users).Returns(users);
        
        var result = await _service.GetAllActiveUsers();
        
        Assert.AreEqual(2, result.Count);
    }
    
    [TestMethod]
    public async Task RegisterUser_ShouldCallCreateAsync()
    {
        var user = new AppUser { UserName = "testuser" };
        var password = "Test@123";
        var identityResult = IdentityResult.Success;

        _userManagerMock.Setup(x => x.CreateAsync(user, password))
            .ReturnsAsync(identityResult);

        var result = await _service.RegisterUser(user, password);

        Assert.AreEqual(identityResult, result);
        _userManagerMock.Verify(x => x.CreateAsync(user, password), Times.Once);
    }
    
    [TestMethod]
    public async Task UpdateActiveStatus_ShouldUpdateUserStatus()
    {
        var userId = "testuser1";
        var newStatus = false;
        var user = new AppUser { Id = userId, UserName = "testuser", Active = true };

        _userManagerMock.Setup(x => x.FindByIdAsync(userId))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.UpdateAsync(user))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _service.UpdateActiveStatus(newStatus, userId);

        Assert.IsNotNull(result);
        Assert.AreEqual(newStatus, result.Active);
        _userManagerMock.Verify(x => x.FindByIdAsync(userId), Times.Once);
        _userManagerMock.Verify(x => x.UpdateAsync(user), Times.Once);
    }
    
    [TestMethod]
    public async Task Delete_ShouldRemoveUser()
    {
        var userId = "testuser1";
        var user = new AppUser { Id = userId, UserName = "testuser" };

        _userManagerMock.Setup(x => x.FindByIdAsync(userId))
            .ReturnsAsync(user);
        _userManagerMock.Setup(x => x.DeleteAsync(user))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _service.Delete(userId);

        Assert.IsNotNull(result);
        Assert.AreEqual(userId, result.Id);
        _userManagerMock.Verify(x => x.FindByIdAsync(userId), Times.Once);
        _userManagerMock.Verify(x => x.DeleteAsync(user), Times.Once);
    }
}
