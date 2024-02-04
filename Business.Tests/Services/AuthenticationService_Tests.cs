using Business.Interfaces;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.DatabaseFirstEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Tests.Services;

public class AuthenticationService_Tests
{
    private readonly DatabaseFirstDbContext _context = new(new DbContextOptionsBuilder<DatabaseFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    private AuthenticationEntity CreateTestAuthenticationEntity()
    {
        return new AuthenticationEntity
        {
            Username = "TestUser",
            PasswordHash = Guid.NewGuid().ToString(),
        };
    }

    private Mock<IAuthenticationRepository> _authenticationRepositoryMock = new Mock<IAuthenticationRepository>();

    [Fact]
    public async Task CreateUserAndLoginAsync_Should_Return_Success_When_NewUserAdded_WithMock()
    {
        // Arrange
        var authService = new AuthenticationService(_authenticationRepositoryMock.Object);
        _authenticationRepositoryMock.Setup(x => x.Exists(It.IsAny<Expression<Func<AuthenticationEntity, bool>>>()))
            .Returns(false);
        _authenticationRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<AuthenticationEntity>()))
        .ReturnsAsync(new AuthenticationEntity());
        var authEntity = CreateTestAuthenticationEntity();

        // Act
        var result = await authService.CreateUserAndLoginAsync(authEntity.Username, authEntity.PasswordHash);

        // Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task CreateUserAndLoginAsync_Should_Return_Success_When_NewUserAdded_WithOutMock()
    {
        // Arrange
        IAuthenticationRepository authenticationRepository = new AuthenticationRepository(_context);
        IAuthenticationService authenticationService = new AuthenticationService(authenticationRepository);
        var authenticationEntity = CreateTestAuthenticationEntity();

        // Act
        var result = await authenticationService.CreateUserAndLoginAsync(authenticationEntity.Username, authenticationEntity.PasswordHash);

        // Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task CreateUserAndLoginAsync_Should_Return_Failure_When_Adding_NewUser_That_AlreadyExists()
    {
        // Arrange
        IAuthenticationRepository authenticationRepository = new AuthenticationRepository(_context);
        IAuthenticationService authenticationService = new AuthenticationService(authenticationRepository);
        var authenticationEntity = CreateTestAuthenticationEntity();

        // Act
        await authenticationService.CreateUserAndLoginAsync(authenticationEntity.Username, authenticationEntity.PasswordHash);
        var result = await authenticationService.CreateUserAndLoginAsync(authenticationEntity.Username, authenticationEntity.PasswordHash);

        // Assert
        Assert.Equal(Result.Failure, result);
    }

    [Fact]
    public async Task ValidateUser_Should_Validate_AndReturnTrue()
    {
        // Arrange
        IAuthenticationRepository authenticationRepository = new AuthenticationRepository(_context);
        IAuthenticationService authenticationService = new AuthenticationService(authenticationRepository);
        var authenticationEntity = CreateTestAuthenticationEntity();

        // Act
        await authenticationService.CreateUserAndLoginAsync(authenticationEntity.Username, authenticationEntity.PasswordHash);
        var validate = await authenticationService.ValidateUserAsync(authenticationEntity.Username, authenticationEntity.PasswordHash);

        // Assert
        Assert.True(validate);
    }

    [Fact]
    public async Task ValidateUser_Should_NotValidate_AndReturnFalse()
    {
        // Arrange
        IAuthenticationRepository authenticationRepository = new AuthenticationRepository(_context);
        IAuthenticationService authenticationService = new AuthenticationService(authenticationRepository);
        var authenticationEntity = CreateTestAuthenticationEntity();

        // Act
        await authenticationService.CreateUserAndLoginAsync(authenticationEntity.Username, authenticationEntity.PasswordHash);
        var maliciousUser = new AuthenticationEntity
        {
            Username = "FakeUser",
            PasswordHash = "123"
        };
        var validate = await authenticationService.ValidateUserAsync(maliciousUser.Username, maliciousUser.PasswordHash);

        // Assert
        Assert.False(validate);
    }

    [Fact]
    public async Task CreateUserAndLoginAsync_Should_Notvalidate_WithIncorrectPassword()
    {
        // Arrange
        IAuthenticationRepository authenticationRepository = new AuthenticationRepository(_context);
        IAuthenticationService authenticationService = new AuthenticationService(authenticationRepository);
        var authenticationEntity = CreateTestAuthenticationEntity();

        // Act
        await authenticationService.CreateUserAndLoginAsync(authenticationEntity.Username, authenticationEntity.PasswordHash);
        var validate = await authenticationService.ValidateUserAsync(authenticationEntity.Username, "FakePassword");

        // Assert
        Assert.False(validate);
    }
}
