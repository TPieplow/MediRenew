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

public class AppointmentService_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
    .UseInMemoryDatabase($"{Guid.NewGuid()}")
    .Options);


    [Fact]
    public async Task AddAppointmentAsync_ShouldAdd_And_Return_ResultEnum_Success()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);

        var appointmentDTO = new AppointmentDTO
        {
            Comments = "test",
            Date = DateTime.Now,
            DoctorId = 1,
            PatientId = 1
        };

        //Act
        var result = await appointmentService.AddApointment(appointmentDTO);


        //Assert
        Assert.Equal(Result.Success, result);

    }

    [Fact]
    public async Task AddAppointmentAsync_ShouldNotAdd_BecauseOfExisting_And_Return_ResultEnum_Failure()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);

        var appointmentDTO = new AppointmentDTO
        {
            Comments = "test",
            Date = DateTime.Now,
            DoctorId = 1,
            PatientId = 1
        };

        //Act
        await appointmentService.AddApointment(appointmentDTO);
        var result = await appointmentService.AddApointment(appointmentDTO);


        //Assert
        Assert.Equal(Result.Failure, result);

    }

    [Fact]
    public async Task AddAppointmentAsync_ShouldNotAdd_And_Return_ResultEnum_Failure()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);

        var appointmentDTO = new AppointmentDTO();

        //Act
        var result = await appointmentService.AddApointment(appointmentDTO);

        //Assert
        Assert.Equal(Result.Failure, result);

    }

    [Fact]
    public async Task GetOneAppointmentAsync_ByPatientId_And_Return_Entity()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);
        IDoctorRepository doctorRepository = new DoctorRepository(_context);
        IPatientRepository patientRepository = new PatientRepository(_context);

        var patientEntity = new PatientEntity
        {
            Id = 1,
            FirstName = "test",
            LastName = "Test2",
            PhoneNumber = "0000",
            Address = "test address",
            Email = "test@email.com",
            City = "test city",
            PostalCode = "12112",
            PharmacyId = 1
        };
        await patientRepository.CreateAsync(patientEntity);

        var doctorEntity = new DoctorEntity
        {
            Id = 1,
            FirstName = "test",
            LastName = "Test2",
            DepartmentId = 1,
            PhoneNumber = "0000"
        };

        await doctorRepository.CreateAsync(doctorEntity);

        var appointmentDTO = new AppointmentDTO
        {
            Comments = "test",
            Date = DateTime.Now,
            DoctorId = 1,
            PatientId = 1
        };
        await appointmentService.AddApointment(appointmentDTO);

        //Act
        var result = await appointmentService.GetOneAppointment(appointmentDTO.PatientId);


        //Assert
        Assert.NotNull(result);
        Assert.Equal("test", result.Comments);
    }

    [Fact]
    public async Task GetOneAppointmentAsync_ShouldNot_GetOne_ByPatientId_And_Return_Null()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);

        var appointmentDTO = new AppointmentDTO();

        //Act
        var result = await appointmentService.GetOneAppointment(appointmentDTO.PatientId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAppointmentsAsync_Should_GetAll_And_Return_IEnum()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);

        //Act
        var result = await appointmentService.GetAllAppointments();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<AppointmentDTO>>(result);
    }

    [Fact]
    public async Task UpdateAppointmentAsync_Should_UpdateOneAppointment_AndReturn_ResultEnum_Success()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);
        var appointmentDTO = new AppointmentDTO { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };
        await appointmentService.AddApointment(appointmentDTO);

        //Act
        appointmentDTO.Comments = "Updated comment";
        var result = await appointmentService.UpdateAppointment(appointmentDTO);

        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task DeleteAppointmentAsync_Should_DeleteAppointment_ByPatientId_AndReturn_ResultEnum_Success()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);
        var appointmentDTO = new AppointmentDTO { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };
        await appointmentService.AddApointment(appointmentDTO);

        //Act
        var result = await appointmentService.RemoveAppointmentAsync(appointmentDTO.PatientId);

        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task DeleteAppointmentAsync_ShouldNotFindId_And_NotDeleteAppointment_AndReturn_ResultEnum_Failure()
    {
        //Arrange
        IAppointmentRepository appointmentRepository = new AppointmentRepository(_context);
        IAppointmentService appointmentService = new AppointmentService(appointmentRepository);
        var appointmentDTO = new AppointmentDTO { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };

        //Act
        var result = await appointmentService.RemoveAppointmentAsync(appointmentDTO.PatientId);

        //Assert
        Assert.Equal(Result.Failure, result);
    }
}
