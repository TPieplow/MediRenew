using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class PrescriptionRepository_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);


    [Fact]
    public async Task CreateAsync_Prescription_Should_SaveToDatabase_And_Return_Entity()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            PharmacyId =1,
            Dosage = "Test",
            Cost = 0000
        };

        //Act
        var result = await prescriptionRepository.CreateAsync(prescriptionEntity);


        //Assert
        Assert.NotNull(result);
        Assert.Equal(result, prescriptionEntity);
    }

    [Fact]
    public async Task CreateAsync_Prescription_ShouldNot_SaveToDatabase_And_Return_Null()
    {
        //Arrange
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity();

        //Act
        var result = await prescriptionRepository.CreateAsync(prescriptionEntity);


        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOneAsync_Prescription_ShouldFindById_ReturnOnePrescription()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            PharmacyId = 1,
            Dosage = "Test",
            Cost = 0000
        };

        await prescriptionRepository.CreateAsync(prescriptionEntity);

        //Act
        var result = await prescriptionRepository.GetOneAsync(x => x.Id == prescriptionEntity.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(prescriptionEntity.Id, result.Id);
    }

    [Fact]
    public async Task GetOneAsync_Prescription_ShouldNotFindById_ReturnNull()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            PharmacyId = 1,
            Dosage = "Test",
            Cost = 0000
        };

        //Act
        var result = await prescriptionRepository.GetOneAsync(x => x.Id == prescriptionEntity.Id);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Prescriptions_ShouldReturn_IEnumerable()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            PharmacyId = 1,
            Dosage = "Test",
            Cost = 0000
        };

        await prescriptionRepository.CreateAsync(prescriptionEntity);

        //Act
        var result = await prescriptionRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<PrescriptionEntity>>(result);
    }

    [Fact]
    public async Task GetAllAsync_PrescriptionsForOnePatient_ShouldReturn_IEnumerable()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);

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
        var patient = await patientRepository.CreateAsync(patientEntity);

        //Act
        var result = await prescriptionRepository.GetAllForPatient(patient.Id);

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<PrescriptionEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateOnePrescription_AndReturn_UpdatedEntity()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            PharmacyId = 1,
            Dosage = "Test",
            Cost = 0000
        };

        await prescriptionRepository.CreateAsync(prescriptionEntity);

        //Act
        prescriptionEntity.Dosage = "Updated dosage";
        var result = await prescriptionRepository.UpdateAsync(prescriptionEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Updated dosage", result.Dosage);
    }

    [Fact]
    public async Task DeleteAsync_Should_DeleteOnePrescription_ById_AndReturn_True()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            PharmacyId = 1,
            Dosage = "Test",
            Cost = 0000
        };

        await prescriptionRepository.CreateAsync(prescriptionEntity);

        //Act
        var result = await prescriptionRepository.DeleteAsync(x => x.Id == prescriptionEntity.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_Should_NotFindPrescription_ById_Return_False()
    {
        //Arrange
        IPrescriptionRepository prescriptionRepository = new PrescriptionRepository(_context);
        var prescriptionEntity = new PrescriptionEntity
        {
            Id = 1,
            PatientId = 1,
            DoctorId = 1,
            PharmacyId = 1,
            Dosage = "Test",
            Cost = 0000
        };

        //Act
        var result = await prescriptionRepository.DeleteAsync(x => x.Id == prescriptionEntity.Id);

        //Assert
        Assert.False(result);
    }
}
