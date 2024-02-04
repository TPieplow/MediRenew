
using Infrastructure.Contexts;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories
{
    public class PharmacyRepository_Tests
    {
        private readonly CodeFirstDbContext _context = new(
            new DbContextOptionsBuilder<CodeFirstDbContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

        private PharmacyEntity CreateTestPharmacyEntity()
        {
            return new PharmacyEntity
            {
                Id = 1,
                MedicationName = "Test",
            };
        }

        [Fact]
        public async Task CreateAsync_ShouldCreate_NewPharmacy_AndReturn_Entity()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = CreateTestPharmacyEntity();

            // Act
            var result = await repo.CreateAsync(pharmacyEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, pharmacyEntity.Id);
            Assert.Equal(result.MedicationName, pharmacyEntity.MedicationName);
        }

        [Fact]
        public async Task CreateAsync_ShouldNotCreate_NewPharmacy_AndReturnNull()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = new PharmacyEntity();

            // Act
            var result = await repo.CreateAsync(pharmacyEntity);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetOneAsync_ShouldGet_OnePharmacyEntity_ById_AndReturnEntity()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = CreateTestPharmacyEntity();
            await repo.CreateAsync(pharmacyEntity);

            // Act
            var result = await repo.GetOneAsync(x => x.Id == pharmacyEntity.Id);

            // Arrange
            Assert.NotNull(result);
            Assert.Equal(pharmacyEntity.Id, result.Id);
        }

        [Fact]
        public async Task GetOneAsync_ShouldNotGet_OnePharmacyEntity_ById()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = new PharmacyEntity();

            // Act
            var result = await repo.GetOneAsync(x => x.Id == 1);

            // Arrange
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldGet_AllPharmacyEntities_AndReturnIEnumerable()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = CreateTestPharmacyEntity();
            await repo.CreateAsync(pharmacyEntity);

            var pharmacyEntity2 = new PharmacyEntity
            {
                Id = 2,
                MedicationName = "TestMedication",
            };
            await repo.CreateAsync(pharmacyEntity2);

            // Act 
            var result = await repo.GetAllAsync();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<PharmacyEntity>>(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_UpdatePharmacy_AndReturn_UpdatedEntity()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = CreateTestPharmacyEntity();
            await repo.CreateAsync(pharmacyEntity);

            // Act
            pharmacyEntity.MedicationName = "Updated";
            var result = await repo.UpdateAsync(pharmacyEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated", result.MedicationName);
        }

        [Fact]
        public async Task DeleteAsync_Should_DeletePharmacyEntity_UsingId_ThenReturn_True()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = CreateTestPharmacyEntity();
            await repo.CreateAsync(pharmacyEntity);

            // Act
            var result = await repo.DeleteAsync(x => x.Id == pharmacyEntity.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_Should_NotDeletePharmacyEntity_ThenReturn_False()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = CreateTestPharmacyEntity();
            await repo.CreateAsync(pharmacyEntity);

            // Act
            var result = await repo.DeleteAsync(x => x.Id == 2);

            // Assert
            Assert.False(result, "Expected false since id 2 doesnt exist in the context.");
        }

        [Fact]
        public async Task ExistsAsync_Should_ReturnTrue_IfAlreadyExist()
        {
            // Arrange
            IPharmacyRepository repo = new PharmacyRepository(_context);
            var pharmacyEntity = CreateTestPharmacyEntity();
            await repo.CreateAsync(pharmacyEntity);

            // Act
            bool result = repo.Exists(x => x.Id == 1);

            // Assert
            Assert.True(result);
        }
    }
}
