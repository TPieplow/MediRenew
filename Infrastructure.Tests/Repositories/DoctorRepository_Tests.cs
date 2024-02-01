using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class DoctorRepository_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);


    [Fact]
    public async Task CreateAsync_Doctor_Should_SaveToDatabase_And_Return_Entity()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        var doctorEntity = new DoctorEntity 
        {
            Id = 1, 
            FirstName = "test", 
            LastName = "Test2", 
            DepartmentId = 1, 
            PhoneNumber = "0000"
        };

        //Act
        var result = await doctorRepository.CreateAsync(doctorEntity);


        //Assert
        Assert.NotNull(result);
        Assert.Equal(result, doctorEntity);
    }

    [Fact]
    public async Task CreateAsync_Doctor_ShouldNot_SaveToDatabase_And_Return_Null()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        var doctorEntity = new DoctorEntity();

        //Act
        var result = await doctorRepository.CreateAsync(doctorEntity);


        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOneAsync_Doctor_ShouldFindByDoctorId_ReturnOneDoctor()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = new DepartmentEntity { Id = 1, DepartmentName = "test" };
        await departmentRepository.CreateAsync(departmentEntity);

        var doctorEntity = new DoctorEntity 
        { 
            Id = 1, 
            FirstName = "test", 
            LastName = "Test2", 
            DepartmentId = 1, 
            PhoneNumber = "0000"
        };

        var response = await doctorRepository.CreateAsync(doctorEntity);

        //Act
        var result = await doctorRepository.GetOneAsync(x => x.Id == doctorEntity.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(doctorEntity.Id, result.Id);
    }

    [Fact]
    public async Task GetOneAsync_Doctor_ShouldNotFindById_ReturnNull()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        var doctorEntity = new DoctorEntity 
        { 
            Id = 1, 
            FirstName = "test", 
            LastName = "Test2", 
            DepartmentId = 1, 
            PhoneNumber = "0000" 
        };

        //Act
        var result = await doctorRepository.GetOneAsync(x => x.Id == doctorEntity.Id);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Doctors_ShouldReturn_IEnumerable()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        var doctorEntity = new DoctorEntity 
        { 
            Id = 1, 
            FirstName = "test", 
            LastName = "Test2", 
            PhoneNumber = "0000", 
            DepartmentId = 1 
        };

        await doctorRepository.CreateAsync(doctorEntity);

        //Act
        var result = await doctorRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<DoctorEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateOneDoctor_AndReturn_UpdatedEntity()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        var doctorEntity = new DoctorEntity 
        { 
            Id = 1,
            FirstName = "test", 
            LastName = "Test2", 
            PhoneNumber = "0000", 
            DepartmentId = 1 
        };

        await doctorRepository.CreateAsync(doctorEntity);

        //Act
        doctorEntity.FirstName = "Update test name";
        var result = await doctorRepository.UpdateAsync(doctorEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Update test name", result.FirstName);
    }

    [Fact]
    public async Task DeleteAsync_Should_DeleteOneDoctor_ById_AndReturn_True()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        var doctorEntity = new DoctorEntity 
        { 
            Id = 1, 
            FirstName = "test", 
            LastName = "Test2", 
            PhoneNumber = "0000", 
            DepartmentId = 1 
        };

        await doctorRepository.CreateAsync(doctorEntity);

        //Act
        var result = await doctorRepository.DeleteAsync(x => x.Id == doctorEntity.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_Should_NotFindDoctor_ById_Return_False()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        var doctorEntity = new DoctorEntity 
        { 
            Id = 1,
            FirstName = "test", 
            LastName = "Test2", 
            PhoneNumber = "0000", 
            DepartmentId = 1 
        };

        //Act
        var result = await doctorRepository.DeleteAsync(x => x.Id == doctorEntity.Id);

        //Assert
        Assert.False(result);
    }
}
