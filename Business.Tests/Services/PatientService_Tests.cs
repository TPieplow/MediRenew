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

public class PatientService_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);


    [Fact]
    public async Task AddPatientAsync_ShouldAdd_And_Return_ResultEnum_Success()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            Address = "test street",
            City = "test city",
            Email = "test@email.com",
            PostalCode = "00000"
        };

        //Act
        var result = await patientService.AddPatientAsync(patientDTO);


        //Assert
        Assert.Equal(Result.Success, result);

    }

    [Fact]
    public async Task AddPatientAsync_ShouldNotAdd_BecauseOfExisting_And_Return_ResultEnum_Failure()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            Address = "test street",
            City = "test city",
            Email = "test@email.com",
            PostalCode = "00000"
        };

        //Act
        await patientService.AddPatientAsync(patientDTO);
        var result = await patientService.AddPatientAsync(patientDTO);


        //Assert
        Assert.Equal(Result.Failure, result);

    }

    [Fact]
    public async Task AddPatientAsync_ShouldNotAdd_And_Return_ResultEnum_Failure()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO();

        //Act
        var result = await patientService.AddPatientAsync(patientDTO);


        //Assert
        Assert.Equal(Result.Failure, result);

    }

    [Fact]
    public async Task GetOnePatientAsync_ById_And_Return_Entity()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            Address = "test street",
            City = "test city",
            Email = "test@email.com",
            PostalCode = "00000"
        };
        await patientService.AddPatientAsync(patientDTO);

        //Act
        var result = await patientService.GetOnePatient(patientDTO.Id);


        //Assert
        Assert.NotNull(result);
        Assert.Equal("test", result.FirstName);
    }

    [Fact]
    public async Task GetOnePatientAsync_ShouldNot_GetOne_ById_And_Return_Null()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO();

        //Act
        var result = await patientService.GetOnePatient(patientDTO.Id);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllPatientsAsync_Should_GetAll_And_Return_IEnum()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        //Act
        var result = await patientService.GetAllPatients();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<PatientDTO>>(result);
    }

    [Fact]
    public async Task UpdatePatientAsync_Should_UpdateOnePatient_AndReturn_ResultEnum_Success()
    {
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            Address = "test street",
            City = "test city",
            Email = "test@email.com",
            PostalCode = "00000"
        };
        await patientService.AddPatientAsync(patientDTO);

        //Act
        patientDTO.FirstName = "test3";
        var result = await patientService.UpdatePatientAsync(patientDTO);

        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task DeletePatientAsync_Should_DeletePatient_ById_AndReturn_ResultEnum_Success()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            Address = "test street",
            City = "test city",
            Email = "test@email.com",
            PostalCode = "00000"
        };
        await patientService.AddPatientAsync(patientDTO);

        //Act
        var result = await patientService.RemovePatientAsync(patientDTO.Id);

        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task DeleteDoctorAsync_ShouldNotFindId_And_NotDeleteDoctor_AndReturn_ResultEnum_Failure()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPatientService patientService = new PatientService(patientRepository, prescriptionRepository);

        var patientDTO = new PatientDTO
        {
            Id = 1,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            Address = "test street",
            City = "test city",
            Email = "test@email.com",
            PostalCode = "00000"
        };

        //Act
        var result = await patientService.RemovePatientAsync(patientDTO.Id);

        //Assert
        Assert.Equal(Result.Failure, result);
    }

    [Fact]
    public async Task UpdateStaffAsync_Should_UpdateOneStaff_AndReturn_ResultEnum_Success()
    {
        // Arrange
        IStaffRepository staffRepository = new StaffRepository(_context);
        IStaffService staffService = new StaffService(staffRepository);
        IDepartmentRepository departmentRepository = new DepartmentRepository(_context);

        var departmentEntity = new DepartmentEntity
        {
            Id = 1,
            HospitalId = 1,
            DepartmentName = "test"
        };
        await departmentRepository.CreateAsync(departmentEntity);

        var staffDTO = new StaffDTO
        {
            Id = 1,
            Department = departmentEntity,
            FirstName = "test",
            LastName = "test 2",
            PhoneNumber = "000",
            DepartmentId = 1,
            RoleName = "tast"
        };
        await staffService.AddStaffMember(staffDTO);

        //Act
        staffDTO.FirstName = "Updated name";
        var result = await staffService.UpdateStaffAsync(staffDTO);

        //Assert
        Assert.Equal(Result.Success, result);
    }
}
