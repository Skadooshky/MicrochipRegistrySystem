
using Xunit;
using Moq;
using NationalMicrochipRegistry.Business.Services;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Data.Models;

public class UserServiceTests
{
    [Fact]
    public void AddUser_ShouldCallRepositoryAdd()
    {
        var mockRepo = new Mock<IUserRepository>();
        var service = new UserService(mockRepo.Object);
        var user = new User { Id = 1, Username = "admin", PasswordHash = "hash" };

        service.AddUser(user);

        mockRepo.Verify(r => r.Add(user), Times.Once);
    }

    [Fact]
    public void Authenticate_ShouldReturnTrue_WhenPasswordMatches()
    {
        var mockRepo = new Mock<IUserRepository>();
        var user = new User { Username = "admin", PasswordHash = "1234" };
        mockRepo.Setup(r => r.GetByUsername("admin")).Returns(user);

        var service = new UserService(mockRepo.Object);
        var result = service.Authenticate("admin", "1234");

        Assert.True(result);
    }
}
