using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using static Infrastructure.Utils.ResultEnums;

namespace Business.Tests.Services
{
    public class InvoiceService_Tests
    {
        private readonly CodeFirstDbContext _context = new(
            new DbContextOptionsBuilder<CodeFirstDbContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

        private InvoiceDTO CreateTestInvoiceEntity()
        {
            return new InvoiceDTO
            {
                Id = 1,
                Description = "Test",
                Cost = 1,
                TotalCost = 1,
                PatientId = 1,
                PharmacyId = 1,
            };
        }


        [Fact]
        public async Task AddInvoiceAsync_Should_Add_AndReturn_ResultEnumSuccess()
        {
            // Arrange
            IInvoiceRepository invoiceRepository = new InvoiceRepository(_context);
            IInvoiceService invoiceService = new InvoiceService(invoiceRepository);
            var invoiceDTO = CreateTestInvoiceEntity();

            // Act
            var result = await invoiceService.AddInvoiceAsync(invoiceDTO);

            // Assert
            Assert.Equal(ResultEnums.Result.Success, result);
        }

        [Fact]
        public async Task AddInvoice_ShouldNot_AddInvoice_AndReturn_ResultEnumFailure()
        {
            // Arrange
            IInvoiceRepository invoiceRepository = new InvoiceRepository(_context);
            IInvoiceService invoiceService = new InvoiceService(invoiceRepository);
            var invoiceDTO = CreateTestInvoiceEntity();

            // Act
            await invoiceService.AddInvoiceAsync (invoiceDTO);
            var result = await invoiceService.AddInvoiceAsync(invoiceDTO);

            // Assert
            Assert.Equal(ResultEnums.Result.Failure, result);
        }


        [Fact]
        public async Task GetOneAsync_Should_GetOneInvoice_IncludingPatientAndPharmacy_AndReturn_DTO()
        {
            // Arrange
            IInvoiceRepository invoiceRepository = new InvoiceRepository(_context);
            IInvoiceService invoiceService = new InvoiceService(invoiceRepository);
            var invoiceDTO = CreateTestInvoiceEntity();

            // Act
            await invoiceService.AddInvoiceAsync(invoiceDTO);
            var result = invoiceService.GetOneInvoiceAsync(invoiceDTO.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetOneAsync_ShouldNot_GetOneInvoice_IncludingPatientAndPharmacy_AndReturn_Null()
        {
            // Arrange
            IInvoiceRepository invoiceRepository = new InvoiceRepository(_context);
            IInvoiceService invoiceService = new InvoiceService(invoiceRepository);
            var invoiceDTO = CreateTestInvoiceEntity();

            var result = await invoiceService.GetOneInvoiceAsync(invoiceDTO.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ViewPatientInvoiceAsync_Should_GetAllInvoices_AndReturn_IEnumerableOfDTO()
        {
            // Arrange
            IInvoiceRepository invoiceRepository = new InvoiceRepository(_context);
            IInvoiceService invoiceService = new InvoiceService(invoiceRepository);
            
            var invoiceDTO = CreateTestInvoiceEntity();
            var invoiceDTO2 = new InvoiceDTO
            {
                Id = 2,
                Description = "Test",
                Cost = 2,
                TotalCost = 2,
                PatientId = 2,
                PharmacyId = 2,
            };

            await invoiceService.AddInvoiceAsync(invoiceDTO);
            await invoiceService.AddInvoiceAsync(invoiceDTO2);

            // Act
            var result = await invoiceService.ViewPatientInvoicesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<InvoiceDTO>>(result);
        }

        [Fact]
        public async Task RemoveInvoiceAsync_Should_Delete_Invoice_ById_AndReturn_ResultEnumSuccess()
        {
            // Arrange
            IInvoiceRepository invoiceRepository = new InvoiceRepository(_context);
            IInvoiceService invoiceService = new InvoiceService(invoiceRepository);

            var invoiceDTO = CreateTestInvoiceEntity();
            await invoiceService.AddInvoiceAsync(invoiceDTO);

            // Act
            var result = await invoiceService.RemoveInvoiceAsync(invoiceDTO.Id);

            // Assert
            Assert.Equal(Result.Success, result);
        }

        [Fact]
        public async Task RemoveInvoiceAsync_ShouldNot_Delete_Invoice_ById_AndReturn_ResultEnumFailure()
        {
            // Arrange
            IInvoiceRepository invoiceRepository = new InvoiceRepository(_context);
            IInvoiceService invoiceService = new InvoiceService(invoiceRepository);

            var invoiceDTO = CreateTestInvoiceEntity();

            // Act
            var result = await invoiceService.RemoveInvoiceAsync(invoiceDTO.Id);

            // Assert
            Assert.Equal(Result.Failure, result);
        }
    }
}
