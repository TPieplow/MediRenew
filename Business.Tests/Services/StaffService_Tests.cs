using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Tests.Services;

public class StaffService_Tests
{
    private readonly CodeFirstDbContext _context = new(
        new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    private StaffDTO CreateTestStaffDTO()
    {
        return new StaffDTO
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            RoleName = "Test",
            PhoneNumber = "000",
            DepartmentId = 1,
            DepartmentName = "Test",
        };
    }

    private DepartmentEntity CreateTestDepartmentEntity()
    {
        return new DepartmentEntity
        {
            Id = 1,
            DepartmentName = "Test",
        };
    }

    [Fact]
    public async Task AddStaffMember_Should_AddNewDTO_AndReturn_ResultEnumSuccess()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        var staffDTO = CreateTestStaffDTO();

        // Act
        var result = await staffService.AddStaffMemberAsync(staffDTO);

        // Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task AddStaffMember_ShouldNot_AddNewDTO_AndReturn_ResultEnumFailure_SinceMember_AlreadyExists()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        var staffDTO = CreateTestStaffDTO();

        // Act
        await staffService.AddStaffMemberAsync(staffDTO);
        var result = await staffService.AddStaffMemberAsync(staffDTO);

        // Assert

        Assert.Equal(Result.Failure, result);
    }

    [Fact]
    public async Task AddStaffMember_ShouldNot_AddNewDTO_AndReturnFailure_SinceDTO_IsNull()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        var staffDTO = new StaffDTO();

        // Act
        var result = await staffService.AddStaffMemberAsync(staffDTO);

        // Assert
        Assert.Equal(Result.Failure, result);
    }

    [Fact]
    public async Task GetOneStaffMember_Should_GetOneStaffMember_ByID_AndReturn_StaffEntity()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);

        var departmentEntity = CreateTestDepartmentEntity();
        await departmentRepository.CreateAsync(departmentEntity);

        var staffDTO = CreateTestStaffDTO();
        await staffService.AddStaffMemberAsync(staffDTO);

        // Act
        var result = await staffService.GetOneStaffMemberAsync(staffDTO.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(staffDTO.Id, result.Id);
    }

    [Fact]
    public async Task GetOneStaffMember_ShouldNot_GetOneStaffMember_ByID_AndReturn_NullSinceMember_DoesntExist()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        var departmentEntity = CreateTestDepartmentEntity();
        await departmentRepository.CreateAsync(departmentEntity);
        var staffDTO = CreateTestStaffDTO();

        // Act
        var result = await staffService.GetOneStaffMemberAsync(staffDTO.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllStaff_Should_Return_IEnumerable_OfStaffDTOs()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);

        var departmentEntity = CreateTestDepartmentEntity();
        await departmentRepository.CreateAsync(departmentEntity);

        var staffDTO = CreateTestStaffDTO();
        await staffService.AddStaffMemberAsync(staffDTO);

        var staffDTO2 = new StaffDTO
        {
            Id = 2,
            FirstName = "Test",
            LastName = "Test",
            RoleName = "Test",
            PhoneNumber = "Test",
            DepartmentId = 2,
            DepartmentName = "Test",
        };
        await staffService.AddStaffMemberAsync(staffDTO2);

        // Act
        var result = await staffService.GetAllStaffAsync();

        // Assert
        Assert.True(result.Any());
        Assert.IsAssignableFrom<IEnumerable<StaffDTO>>(result);
    }


    [Fact]
    public async Task UpdateStaffAsync_Should_Update_StaffMember_AndReturn_ResultEnumSuccess_SinceDepartmentDoentExist()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        var staffDTO = new StaffDTO();

        // Act
        await staffService.AddStaffMemberAsync(staffDTO);
        staffDTO.FirstName = "Test";

        var result = await staffService.UpdateStaffAsync(staffDTO);

        // Assert
        Assert.Equal(Result.Failure, result);
    }

    [Fact]
    public async Task DeleteStaffMember_ShouldDelete_StaffMember_GivenACertainId_AndReturn_ResultEnumSuccess()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        var staffDTO = CreateTestStaffDTO();
        await staffService.AddStaffMemberAsync (staffDTO);

        // Act
        var result = await staffService.DeleteStaffMemberAsync(staffDTO.Id);

        // Assert
        Assert.Equal (Result.Success, result);
    }

    [Fact]
    public async Task DeleteStaffMember_ShouldNotDelete_StaffMember_AndReturn_ResultEnumFailure()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        var staffDTO = CreateTestStaffDTO();

        // Act
        var result = await staffService.DeleteStaffMemberAsync(staffDTO.Id);

        // Assert
        Assert.Equal(Result.Failure, result);
    }
}
