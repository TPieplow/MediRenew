using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories;

public class InvoiceRepository_Tests
{
    private readonly CodeFirstDbContext _context = new(
        new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);


    [Fact]
    public async Task CreateAsync_Should_SaveToDatabase_And_Return_InvoiceEntity_AndInclude_PharmacyAndPatient()
    {
        // Arrange
        IInvoiceRepository invoice = new InvoiceRepository(_context);
        var invoiceEntity = new InvoiceEntity
        {
            Description = "Twice a day",
            Cost = 20m,
            TotalCost = 30m,
            PatientId = 1,
            PharmacyId = 1,
        };

        // Act
        var result = await invoice.CreateAsync(invoiceEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result, invoiceEntity);
    }

    [Fact]
    public async Task CreateAsync_ShouldNot_SaveToDatabase_And_ReturnNull()
    {
        // Arrange
        IInvoiceRepository invoice = new InvoiceRepository(_context);
        var invoiceEntity = new InvoiceEntity();

        // Act
        var result = await invoice.CreateAsync(invoiceEntity);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOneAsync_ShouldGet_OneInvoice_ById_Including_Pharmacy()
    {
        // Arrange
        IPatientRepository patient = new PatientRepository(_context);
        IPharmacyRepository pharmacy = new PharmacyRepository(_context);
        IInvoiceRepository invoice = new InvoiceRepository(_context);

        var patientEntity = new PatientEntity
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Address = "Test",
            Email = "Test",
            City = "Test",
            PhoneNumber = "Test",
            PostalCode = "Test",
            PharmacyId = 1,
        };
        await patient.CreateAsync(patientEntity);

        var pharmacyEntity = new PharmacyEntity
        {
            Id = 1,
            MedicationName = "Test",
        };
        await pharmacy.CreateAsync(pharmacyEntity);

        var invoiceEntity = new InvoiceEntity
        {
            Id = 1,
            Description = "Twice a day",
            Cost = 20,
            TotalCost = 30,
            PatientId = 1,
            PharmacyId = 1,
        };
        await invoice.CreateAsync(invoiceEntity);

        // Act
        var result = await invoice.GetOneAsync(x => x.Id == invoiceEntity.Id);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Patient);
        Assert.NotNull(result.Pharmacy);
        Assert.Equal(result.Id, invoiceEntity.Id);
    }

    [Fact]
    public async Task GetOneAsync_ShouldNotGet_OneInvoice_ById_Including_Pharmacy_AndReturnNull()
    {
        // Arrange
        IPatientRepository patient = new PatientRepository(_context);
        IPharmacyRepository pharmacy = new PharmacyRepository(_context);
        IInvoiceRepository invoice = new InvoiceRepository(_context);

        var patientEntity = new PatientEntity
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test",
            Address = "Test",
            Email = "Test",
            City = "Test",
            PhoneNumber = "Test",
            PostalCode = "Test",
            PharmacyId = 1,
        };

        var pharmacyEntity = new PharmacyEntity
        {
            Id = 1,
            MedicationName = "Test",
        };

        var invoiceEntity = new InvoiceEntity
        {
            Id = 1,
            Description = "Twice a day",
            Cost = 20,
            TotalCost = 30,
            PatientId = 1,
            PharmacyId = 1,
        };

        // Act
        var result = await invoice.GetOneAsync(x => x.Id == invoiceEntity.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_Invoices_ShouldReturn_IEnumerable()
    {
        // Arrange
        IInvoiceRepository invoice = new InvoiceRepository(_context);
        var invoiceEntity = new InvoiceEntity
        {
            Id = 1,
            Description = "Twice a day",
            Cost = 20,
            TotalCost = 30,
            PatientId = 1,
            PharmacyId = 1,
        };
        await invoice.CreateAsync(invoiceEntity);

        var invoiceEntity2 = new InvoiceEntity
        {
            Id = 1,
            Description = "Twice a day",
            Cost = 20,
            TotalCost = 30,
            PatientId = 1,
            PharmacyId = 1,
        };
        await invoice.CreateAsync(invoiceEntity2);

        // Act
        var result = await invoice.GetAllAsync();

        // Assert
        Assert.IsAssignableFrom<IEnumerable<InvoiceEntity>>(result);
    }

    [Fact]
    public async Task UpdateAsync_Should_UpdateInvoice_AndReturn_TheUpdatedEntity()
    {
        // Arrange
        IInvoiceRepository invoice = new InvoiceRepository(_context);
        var invoiceEntity = new InvoiceEntity
        {
            Id = 1,
            Description = "Twice a day",
            Cost = 20,
            TotalCost = 30,
            PatientId = 1,
            PharmacyId = 1,
        };
        await invoice.CreateAsync(invoiceEntity);

        // Act
        invoiceEntity.Description = "UpdateDescription";
        var result = await invoice.UpdateAsync(invoiceEntity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("UpdateDescription", result.Description);
    }

    [Fact]
    public async Task DeleteAsync_Should_RemoveInvoice_UsingId_ThenReturn_True()
    {
        IInvoiceRepository invoice = new InvoiceRepository(_context);
        var invoiceEntity = new InvoiceEntity
        {
            Id = 1,
            Description = "Twice a day",
            Cost = 20,
            TotalCost = 30,
            PatientId = 1,
            PharmacyId = 1,
        };
        await invoice.CreateAsync(invoiceEntity);

        var result = await invoice.DeleteAsync(x => x.Id == invoiceEntity.Id);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNot_RemoveInvoice_UsingId_ThenReturn_False()
    {
        IInvoiceRepository invoice = new InvoiceRepository(_context);
        var invoiceEntity = new InvoiceEntity
        {
            Id = 1,
            Description = "Twice a day",
            Cost = 20,
            TotalCost = 30,
            PatientId = 1,
            PharmacyId = 1,
        };
        await invoice.CreateAsync(invoiceEntity);

        var result = await invoice.DeleteAsync(x => x.Id == 2);

        Assert.False(result, "Expected DeleteAsync to return false since the invoice ID is set to 2, with other words, it doesnt exists.");
    }
}
