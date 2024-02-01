using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class DepartmentRepository_Tests
{

    //This repository has a seeder which inputs information to database using the base repo, can not be accessed from application.


    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);


    [Fact]
    public async Task CreateAsync_Department_Should_SaveToDatabase_And_Return_Entity()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { DepartmentName = "Test department", HospitalId = 1 };

        //Act
        var result = await departmentRepository.CreateAsync(departmentEntity); 


        //Assert
        Assert.NotNull(result);
        Assert.Equal(result, departmentEntity);
    }

    [Fact]
    public async Task CreateAsync_Department_ShouldNot_SaveToDatabase_And_Return_Null()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity();

        //Act
        var result = await departmentRepository.CreateAsync(departmentEntity);


        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOneAsync_Department_ShouldFindByDepartmentId_ReturnOneDepartment()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { DepartmentName = "Test name", HospitalId = 1, Id = 1};
        await departmentRepository.CreateAsync(departmentEntity);

        //Act
        var result = await departmentRepository.GetOneAsync(x => x.Id == departmentEntity.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(departmentEntity.Id, result.Id);
    }

    [Fact]
    public async Task GetOneAsync_Department_ShouldNotFindById_ReturnNull()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { DepartmentName = "Test name", HospitalId = 1, Id = 1 };

        //Act
        var result = await departmentRepository.GetOneAsync(x => x.Id == departmentEntity.Id);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Departments_ShouldReturn_IEnumerable()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { DepartmentName = "Test name", HospitalId = 1, Id = 1 };
        await departmentRepository.CreateAsync(departmentEntity);

        //Act
        var result = await departmentRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<DepartmentEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateOneDepartment_AndReturn_UpdatedEntity()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { DepartmentName = "Test name", HospitalId = 1, Id = 1 };
        await departmentRepository.CreateAsync(departmentEntity);

        //Act
        departmentEntity.DepartmentName = "Update test name";
        var result = await departmentRepository.UpdateAsync(departmentEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Update test name", result.DepartmentName);
    }

    [Fact]
    public async Task DeleteAsync_Should_DeleteOneDepartment_ById_AndReturn_True()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { DepartmentName = "Test name", HospitalId = 1, Id = 1 };
        await departmentRepository.CreateAsync(departmentEntity);

        //Act
        var result = await departmentRepository.DeleteAsync(x => x.Id == departmentEntity.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_Should_NotFindDepartment_ById_Return_False()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { DepartmentName = "Test name", HospitalId = 1, Id = 1 };

        //Act
        var result = await departmentRepository.DeleteAsync(x => x.Id == departmentEntity.Id);

        //Assert
        Assert.False(result);
    }

}
