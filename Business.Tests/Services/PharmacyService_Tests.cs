using Business.DTOs;
using Business.Interfaces;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services
{
    public class PharmacyService_Tests
    {
        private readonly CodeFirstDbContext _context = new(
        new DbContextOptionsBuilder<CodeFirstDbContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);


        [Fact]
        public async Task ViewAllPharmacy_Should_CreateNew_PharmacyDTO_AndReturn_DTO()
        {
            // Arrange
            IPharmacyRepository pharmacyRepository = new PharmacyRepository(_context);
            IPharmacyService pharmacyService = new PharmacyService(pharmacyRepository);

            // Act
            var result = await pharmacyService.ViewAllPharmacy();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<PharmacyDTO>>(result);
        }
    }
}
