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

public class PrescriptionService_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async Task AddPrescriptionAsync_ShouldAdd_And_Return_ResultEnum_Success()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);

        var prescriptionDTO = new PrescriptionDTO
        {
            Date = DateTime.Now,
            DoctorId = 1,
            PatientId = 1,
            Dosage = "test dosage",
            Cost = 100,
            PatientName = "test patient name",
            DoctorName = "test doctor name",
            PharmacyId = 1
        };

        //Act
        var result = await prescriptionService.AddPrescriptionAsync(prescriptionDTO);


        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task AddPrescriptionAsync_ShouldNotAdd_And_Return_ResultEnum_Failure()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);

        var prescriptionDTO = new PrescriptionDTO();

        //Act
        var result = await prescriptionService.AddPrescriptionAsync(prescriptionDTO);

        //Assert
        Assert.Equal(Result.Failure, result);

    }

    [Fact]
    public async Task GetAllPrescriptionsAsync_ForOnePatient_ByPatientId_And_Return_IEnum()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);
        IPatientRepository patientRepository = new PatientRepository(_context);
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

        var prescriptionDTO = new PrescriptionDTO
        {
            Date = DateTime.Now,
            DoctorId = 1,
            PatientId = 1,
            Dosage = "test dosage",
            Cost = 100,
            PatientName = "test patient name",
            DoctorName = "test doctor name",
            PharmacyId = 1
        };

        await prescriptionService.AddPrescriptionAsync(prescriptionDTO);


        //Act
        var result = await prescriptionService.GetPatientPrescriptions(patientDTO.Id);


        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<PrescriptionEntity>>(result);
    }

    [Fact]
    public async Task GetAllPrescriptionsAsync_Should_GetAll_And_Return_IEnum()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);

        //Act
        var result = await prescriptionService.GetAllPrescriptions();

        //Assert
        Assert.IsAssignableFrom<IEnumerable<PrescriptionDTO>>(result);
    }

    [Fact]
    public async Task DeletePrescriptionAsync_Should_DeletePrescription_ById_AndReturn_ResultEnum_Success()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);

        var prescriptionDTO = new PrescriptionDTO
        {
            Id = 1,
            Date = DateTime.Now,
            DoctorId = 1,
            PatientId = 1,
            Dosage = "test dosage",
            Cost = 100,
            PatientName = "test patient name",
            DoctorName = "test doctor name",
            PharmacyId = 1
        };
        await prescriptionService.AddPrescriptionAsync(prescriptionDTO);

        //Act
        var result = await prescriptionService.RemovePrescriptionAsync(prescriptionDTO.Id);

        //Assert
        Assert.Equal(Result.Success, result);
    }

    [Fact]
    public async Task DeletePrescriptionAsync_ShouldNotFindId_And_NotDeletePrescirption_AndReturn_ResultEnum_Failure()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        IPrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);

        var prescriptionDTO = new PrescriptionDTO
        {
            Id = 1,
            Date = DateTime.Now,
            DoctorId = 1,
            PatientId = 1,
            Dosage = "test dosage",
            Cost = 100,
            PatientName = "test patient name",
            DoctorName = "test doctor name",
            PharmacyId = 1
        };

        //Act
        var result = await prescriptionService.RemovePrescriptionAsync(prescriptionDTO.Id);

        //Assert
        Assert.Equal(Result.Failure, result);
    }
}
