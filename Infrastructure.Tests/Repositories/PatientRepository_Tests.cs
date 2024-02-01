using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class PatientRepository_Tests
{
    private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);


    [Fact]
    public async Task CreateAsync_Patient_Should_SaveToDatabase_And_Return_Entity()
    {
        //Arrange
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

        //Act
        var result = await patientRepository.CreateAsync(patientEntity);


        //Assert
        Assert.NotNull(result);
        Assert.Equal(result, patientEntity);
    }

    [Fact]
    public async Task CreateAsync_Patient_ShouldNot_SaveToDatabase_And_Return_Null()
    {
        //Arrange
        IPatientRepository patientRepository = new PatientRepository(_context);
        var patientEntity = new PatientEntity();

        //Act
        var result = await patientRepository.CreateAsync(patientEntity);


        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOneAsync_Patient_ShouldFindByPatientId_ReturnOnePatient()
    {
        //Arrange
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

        //Act
        var result = await patientRepository.GetOneAsync(x => x.Id == patientEntity.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(patientEntity.Id, result.Id);
    }

    //[Fact]
    //public async Task GetOneAsync_Patient_IncludingPrescriptions_ShouldFindByPatientId_ReturnOnePrescription_ForPatient()
    //{
    //    //Arrange

    //    IPatientRepository patientRepository = new PatientRepository(_context);
    //    var patientEntity = new PatientEntity
    //    {
    //        Id = 1,
    //        FirstName = "test",
    //        LastName = "Test2",
    //        PhoneNumber = "0000",
    //        Address = "test address",
    //        Email = "test@email.com",
    //        City = "test city",
    //        PostalCode = "12112",
    //        PharmacyId = 1
    //    };
    //    await patientRepository.CreateAsync(patientEntity);

    //    //Act
    //    var pres =  patientRepository.GetByIdIncludePrescription(patientEntity.Id);

    //    //Assert
    //    Assert.NotNull(pres);
    //    Assert.Equal(patientEntity.Id, pres.Id);
    //}

    [Fact]
    public async Task GetOneAsync_Patient_ShouldNotFindById_ReturnNull()
    {
        //Arrange
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

        //Act
        var result = await patientRepository.GetOneAsync(x => x.Id == patientEntity.Id);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Patients_ShouldReturn_IEnumerable()
    {
        //Arrange
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

        //Act
        var result = await patientRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<PatientEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateOnePatient_AndReturn_UpdatedEntity()
    {
        //Arrange
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

        //Act
        patientEntity.FirstName = "Update test name";
        var result = await patientRepository.UpdateAsync(patientEntity);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Update test name", result.FirstName);
    }

    [Fact]
    public async Task DeleteAsync_Should_DeleteOnePatient_ById_AndReturn_True()
    {
        //Arrange
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

        //Act
        var result = await patientRepository.DeleteAsync(x => x.Id == patientEntity.Id);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_Should_NotFindPatient_ById_Return_False()
    {
        //Arrange
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

        //Act
        var result = await patientRepository.DeleteAsync(x => x.Id == patientEntity.Id);

        //Assert
        Assert.False(result);
    }
}
