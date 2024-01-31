using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class AppointmentRepository_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);


    [Fact]
    public async Task Create_Appointment_Should_SaveToDatabase_And_Return_Entity()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };

        //Act
        var result = await appointmentRepository.CreateAsync(appointmentEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result, appointmentEntity);
    }

    [Fact]
    public async Task CreateAsync_Appointment_ShouldNot_SaveToDatabase_And_Return_Null()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity();

        //Act
        var result = await appointmentRepository.CreateAsync(appointmentEntity);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOneAsync_Appointment_ShouldFindByPatientId_ReturnOneAppointment()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };
        await appointmentRepository.CreateAsync(appointmentEntity);

        //Act
        var result = await appointmentRepository.GetOneAsync(x => x.PatientId == appointmentEntity.PatientId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(appointmentEntity.PatientId, result.PatientId);
    }

    [Fact]
    public async Task GetOneAsync_Appointment_ShouldNotFindByPatientId_ReturnNull()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };

        //Act
        var result = await appointmentRepository.GetOneAsync(x => x.PatientId == appointmentEntity.PatientId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Appointments_ShouldReturn_IEnumerable()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };
        await appointmentRepository.CreateAsync(appointmentEntity);

        //Act
        var result = await appointmentRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<AppointmentEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateOneAppointment_AndReturn_UpdatedEntity()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };
        appointmentEntity = await appointmentRepository.CreateAsync(appointmentEntity);

        //Act
        appointmentEntity.Comments = "Updated comment";
        var result = await appointmentRepository.UpdateAsync(appointmentEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Updated comment", result.Comments);
    }

    [Fact]
    public async Task DeleteAsync_Should_DeleteOneAppointment_ByPatientId_AndReturn_True()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };
        await appointmentRepository.CreateAsync(appointmentEntity);

        //Act
        var result = await appointmentRepository.DeleteAsync(x => x.PatientId == appointmentEntity.PatientId);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_Should_NotFind_ByPatientId_Return_False()
    {
        //Arrange
        IAuthentcationRepository appointmentRepository = new AppointmentRepository(_context);
        var appointmentEntity = new AppointmentEntity { Comments = "test", Date = DateTime.Now, DoctorId = 1, PatientId = 1 };

        //Act
        var result = await appointmentRepository.DeleteAsync(x => x.PatientId == appointmentEntity.PatientId);

        //Assert
        Assert.False(result);
    }
}
