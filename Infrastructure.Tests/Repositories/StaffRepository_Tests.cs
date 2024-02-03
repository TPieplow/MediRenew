using Infrastructure.Contexts;
using Infrastructure.DatabaseFirstEntities;
using Infrastructure.HospitalEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Tests.Repositories
{
    public class StaffRepository_Tests
    {
        private readonly CodeFirstDbContext _context = new(new DbContextOptionsBuilder<CodeFirstDbContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);
        private StaffEntity CreateTestStaffEntity()
        {
            return new StaffEntity
            {
                Id = 1,
                FirstName = "TestFName",
                LastName = "TestLName",
                PhoneNumber = "TestNumber",
                RoleName = "TestRole",
                DepartmentId = 1,
            };
        }

        private DepartmentEntity CreateTestDepartmentEntity()
        {
            return new DepartmentEntity
            {
                Id = 1,
                DepartmentName = "TestName",
            };
        }

        [Fact]
        public async Task CreateAsync_Should_CreateNewStaffMember_ReturnEntity()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            var newStaff = CreateTestStaffEntity();

            // Act
            var result = await staff.CreateAsync(newStaff);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(newStaff.FirstName, result.FirstName);
        }

        [Fact]
        public async Task CreateAsync_ShouldNot_CreateNewStaffMemberIfIdExists_ReturnNull()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            var newStaff = CreateTestStaffEntity();
            await staff.CreateAsync(newStaff);

            var newStaff2 = new StaffEntity
            {
                Id = 1,
                FirstName = "TestFName",
                LastName = "TestLName",
                PhoneNumber = "TestNumber",
                RoleName = "TestRole",
                DepartmentId = 1,
            };
            // Act
            var result = await staff.CreateAsync(newStaff2);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldNot_CreateNewStaffMember_IfEntityIsEmpty_ReturnNull()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            var newStaff = new StaffEntity();

            // Act
            var result = await staff.CreateAsync(newStaff);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetOneAsync_Should_UserPrediacate_ReturnOneStaffMember()
        {
            // Arrange
            IStaffRepository teststaff = new StaffRepository(_context);
            IDepartmentRepository department = new DepartmentRepository(_context);

            var newDepartment = CreateTestDepartmentEntity();
            await department.CreateAsync(newDepartment);

            var newStaff = CreateTestStaffEntity();
            await teststaff.CreateAsync(newStaff);
            Expression<Func<StaffEntity, bool>> predicate = staff => staff.Id == newStaff.Id;

            // Act
            var result = await teststaff.GetOneAsync(predicate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newStaff?.FirstName, result?.FirstName);
        }

        [Fact]
        public async Task GetOneAsync_ShouldNot_ReturnStaffMember_ReturnNull()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            var staffEntity = new StaffEntity();

            // Act
            var result = await staff.GetOneAsync(x => x.Id == staffEntity.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnList_OfAllStaffMembers()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            IDepartmentRepository department = new DepartmentRepository(_context);

            var newDepartment = CreateTestDepartmentEntity();
            await department.CreateAsync(newDepartment);

            var newStaff = CreateTestStaffEntity();
            await staff.CreateAsync(newStaff);
            var newStaff2 = new StaffEntity
            {
                Id = 2,
                FirstName = "TestFName",
                LastName = "TestLName",
                PhoneNumber = "TestNumber",
                RoleName = "TestRole",
                DepartmentId = 1,
            };
            await staff.CreateAsync(newStaff2);

            // Act
            var result = await staff.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<StaffEntity>>(result);
        }

        [Fact]
        public async Task UpdateAsync_Should_UpdateExisting_StaffMember_AndReturn_UpdatedEntity()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            IDepartmentRepository department = new DepartmentRepository(_context);

            var newDepartment = CreateTestDepartmentEntity();
            await department.CreateAsync(newDepartment);

            var newStaff = CreateTestStaffEntity();
            await staff.CreateAsync(newStaff);

            // Act
            newStaff.FirstName = "TestUpdate";
            var result = await staff.UpdateAsync(newStaff);

            Assert.NotNull(result);
            Assert.Equal(newStaff.FirstName, result.FirstName);
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Existing_StaffMember_AndReturnTrue()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            IDepartmentRepository department = new DepartmentRepository(_context);

            var newDepartment = CreateTestDepartmentEntity();
            await department.CreateAsync(newDepartment);

            var newStaff = CreateTestStaffEntity();
            await staff.CreateAsync(newStaff);

            // Act
            var result = await staff.DeleteAsync(x => x.Id == newStaff.Id);

            // Assert
            Assert.True(result, "Expected true since a new staff member with Id one was created before removing it");
        }

        [Fact]
        public async Task DeleteAsync_ShouldNot_RemoveInvoice_ThenReturnFalse()
        {
            // Arrange
            IStaffRepository staff = new StaffRepository(_context);
            IDepartmentRepository department = new DepartmentRepository(_context);

            var newDepartment = CreateTestDepartmentEntity();
            await department.CreateAsync(newDepartment);

            var newStaff = CreateTestStaffEntity();
            await staff.CreateAsync(newStaff);

            // Act
            var result = await staff.DeleteAsync(x => x.Id == 2);

            // Assert
            Assert.False(result, "Expected false since the ID is set to 2, thus it doesnt exists in the current context");
        }
    }
}
