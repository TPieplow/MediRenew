using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Tests.Services;

public class DoctorService_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
.UseInMemoryDatabase($"{Guid.NewGuid()}")
.Options);


    [Fact]
    public async Task AddDoctorAsync_ShouldAdd_And_Return_ResultEnum_Success()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var doctorDTO = new DoctorDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            DepartmentId = 1
        };

        //Act
        var result = await doctorService.AddDoctorAsync(doctorDTO);


        //Assert
        Assert.Equal(Result.Success, result);

    }

    [Fact]
    public async Task AddDoctorAsync_ShouldNotAdd_BecauseOfExisting_And_Return_ResultEnum_Failure()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var doctorDTO = new DoctorDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            DepartmentId = 1
        };

        //Act
        await doctorService.AddDoctorAsync(doctorDTO);
        var result = await doctorService.AddDoctorAsync(doctorDTO);


        //Assert
        Assert.Equal(Result.Failure, result);

    }

    [Fact]
    public async Task AddDoctorAsync_ShouldNotAdd_And_Return_ResultEnum_Failure()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var doctorDTO = new DoctorDTO();

        //Act
        var result = await doctorService.AddDoctorAsync(doctorDTO);


        //Assert
        Assert.Equal(Result.Failure, result);

    }

    [Fact]
    public async Task GetOneDoctorAsync_ById_And_Return_Entity()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var departmentEntity = new DepartmentEntity
        {
            Id = 1,
            HospitalId = 1,
            DepartmentName = "test"
        };
        await departmentRepository.CreateAsync(departmentEntity);

        var doctorDTO = new DoctorDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            DepartmentId = 1
        };
        await doctorService.AddDoctorAsync(doctorDTO);

        //Act
        var result = await doctorService.GetOneDoctorAsync(doctorDTO.Id);


        //Assert
        Assert.NotNull(result);
        Assert.Equal("test", result.Department.DepartmentName);
    }

    [Fact]
    public async Task GetOneDoctorAsync_ShouldNot_GetOne_ById_And_Return_Null()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var doctorDTO = new DoctorDTO();

        //Act
        var result = await doctorService.GetOneDoctorAsync(doctorDTO.Id);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllDoctorsAsync_Should_GetAll_And_Return_IEnum()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        //Act
        var result = await doctorService.GetAllDoctors();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<DoctorDTO>>(result);
    }

    [Fact]
    public async Task UpdateDoctorAsync_Should_UpdateOneDoctor_AndReturn_ResultEnum_Success()
    {
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var departmentEntity = new DepartmentEntity
        {
            Id = 1,
            HospitalId = 1,
            DepartmentName = "test"
        };
        await departmentRepository.CreateAsync(departmentEntity);

        var doctorDTO = new DoctorDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            DepartmentId = 1
        };
        await doctorService.AddDoctorAsync(doctorDTO);

        //Act
        doctorDTO.FirstName = "Updated name";
        var result = await doctorService.UpdateDoctorAsync(doctorDTO);

        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task DeleteDoctorAsync_Should_DeleteDoctor_ById_AndReturn_ResultEnum_Success()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var doctorDTO = new DoctorDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            DepartmentId = 1
        };
        await doctorService.AddDoctorAsync(doctorDTO);

        //Act
        var result = await doctorService.RemoveDoctorAsync(doctorDTO.Id);

        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task DeleteDoctorAsync_ShouldNotFindId_And_NotDeleteDoctor_AndReturn_ResultEnum_Failure()
    {
        //Arrange
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDoctorService doctorService = new DoctorService(doctorRepository, departmentRepository);

        var doctorDTO = new DoctorDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            DepartmentId = 1
        };

        //Act
        var result = await doctorService.RemoveDoctorAsync(doctorDTO.Id);

        //Assert
        Assert.Equal(Result.Failure, result);
    }
}
