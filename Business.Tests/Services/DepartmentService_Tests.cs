using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services;

public class DepartmentService_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task GetAllDepartmentsAsync_Should_GetAll_And_Return_IEnum()
    {
        //Arrange
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);
        IDepartmentService departmentService = new DepartmentService(departmentRepository);

        //Act
        var result = await departmentService.GetAllDepartments();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<DepartmentDTO>>(result);
    }
}
