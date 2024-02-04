using Azure.Identity;
using Infrastructure.Contexts;
using Infrastructure.DatabaseFirstEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Tests.Repositories;

public class AuthenticationRepository_Tests
{
    private readonly DatabaseFirstDbContext _context = new(
        new DbContextOptionsBuilder<DatabaseFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    private AuthenticationEntity CreateTestAuthenticationEntity()
    {
        return new AuthenticationEntity
        {
            Username = "username",
            PasswordHash = "password"
        };
    }

    [Fact]
    public async Task CreateAsync_Should_SaveToDatabase_And_Return_Entity()
    {
        // Arrange
        IAuthenticationRepository repo = new AuthenticationRepository(_context);
        var authenticationEntity = CreateTestAuthenticationEntity();

        // Act
        var result = await repo.CreateAsync(authenticationEntity);

        // Assert
        Assert.True(result != null, "Test should not be null");
        Assert.Equal(authenticationEntity.Username, result.Username);
    }

    [Fact]
    public async Task CreateAsync_Should_NotSaveToDatabase_IfEntity_IsEmpty_And_ReturnNull()
    {
        // Arrange
        IAuthenticationRepository repo = new AuthenticationRepository(_context);
        var authenticationEntity = new AuthenticationEntity();

        // Act
        var result = await repo.CreateAsync(authenticationEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOneAsync_Should_UsePredicate_And_ReturnUser()
    {
        // Arrange
        IAuthenticationRepository repo = new AuthenticationRepository(_context);
        var testUser = CreateTestAuthenticationEntity();
        await repo.CreateAsync(testUser);

        Expression<Func<AuthenticationEntity, bool>> predicate = user => user.Username == "username";

        //Act
        var result = await repo.GetOneAsync(predicate);
        Debug.WriteLine($"Result: {result?.Username}");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(testUser.Username, result?.Username);
    }

    [Fact]
    public async Task GetOneAsync_Should_UsePredicate_And_NotReturnUser()
    {
        //Arrange
        IAuthenticationRepository repo = new AuthenticationRepository(_context);
        var testUser = CreateTestAuthenticationEntity();
        await repo.CreateAsync(testUser);
        var wrongUserInput = "fakeuser";

        Expression<Func<AuthenticationEntity, bool>> predicate = user => user.Username == wrongUserInput;

        // Act
        var result = await repo.GetOneAsync(predicate);

        //Assert
        Assert.Null(result);
        Assert.NotEqual(testUser.Username, result?.Username);
    }

    [Fact]
    public async Task GetOneAsync_Should_Handle_SqlInjection()
    {
        // Arrange
        IAuthenticationRepository repo = new AuthenticationRepository(_context);
        var testUser = CreateTestAuthenticationEntity();
        await repo.CreateAsync(testUser);
        // Simulates a simple SQL-injection ('; = end of query AND -- will comment everything after it. That leaves us with the CREATE query).
        var maliciousInput = "'; CREATE TABLE MaliciousAuthentications (id INT); --";

        Expression<Func<AuthenticationEntity, bool>> predicate = user => user.Username == maliciousInput;

        // Act
        var result = await repo.GetOneAsync(predicate);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Exists_Should_FindUser_And_ReturnTrue()
    {
        // Arrange
        IAuthenticationRepository repo = new AuthenticationRepository(_context);
        var testUser = CreateTestAuthenticationEntity();
        await repo.CreateAsync(testUser);

        Expression<Func<AuthenticationEntity, bool>> predicate = user => user.Username == "username";

        // Act
        bool result = repo.Exists(predicate);

        // Assert
        Assert.True(result);
    }
}
