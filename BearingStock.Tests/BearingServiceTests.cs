using Moq;
using BearingStock.Core.Services;
using BearingStock.Core.Repositories.Interfaces;
using BearingStock.Core.Repositories.UnitOfWork;
using BearingStock.Domain.Entityes;
using BearingStock.Domain.Enums;

namespace BearingStock.Tests
{
    public class BearingServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBearingRepository> _mockBearingRepo;
        private readonly BearingService _bearingService;

        public BearingServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBearingRepo = new Mock<IBearingRepository>();
            _mockUnitOfWork.Setup(uow => uow.Bearings).Returns(_mockBearingRepo.Object);
            _bearingService = new BearingService(_mockUnitOfWork.Object);
        }

        // ✅ Test Case 1: Getting All Bearings
        [Fact]
        public async Task GetAllBearings_Should_Return_Bearings()
        {
            // Arrange
            var bearings = new List<Bearing>
            {
                new Bearing { Id = 1, Name = "Bearing A", Type = "Ball", Manufacturer = "Company X", Size = 20.5m, SizeType = BearingSizeType.Diameter },
                new Bearing { Id = 2, Name = "Bearing B", Type = "Roller", Manufacturer = "Company Y", Size = 30.0m, SizeType = BearingSizeType.Diameter }
            };

            _mockBearingRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(bearings);

            // Act
            var result = await _bearingService.GetAllBearingsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            _mockBearingRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        // ✅ Test Case 2: Getting a Bearing by ID
        [Fact]
        public async Task GetBearingById_Should_Return_Correct_Bearing()
        {
            // Arrange
            var bearing = new Bearing { Id = 1, Name = "Bearing A", Type = "Ball", Manufacturer = "Company X", Size = 20.5m, SizeType = BearingSizeType.Diameter };

            _mockBearingRepo.Setup(repo => repo.GetByIdAsync(1))
                .ReturnsAsync(bearing);

            // Act
            var result = await _bearingService.GetBearingByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _mockBearingRepo.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        // ✅ Test Case 3: Creating a Bearing
        [Fact]
        public async Task AddBearing_Should_Call_Repository_And_SaveChanges()
        {
            // Arrange
            var newBearing = new Bearing { Id = 3, Name = "Bearing C", Type = "Tapered", Manufacturer = "Company Z", Size = 40.0m, SizeType = BearingSizeType.Diameter };

            _mockBearingRepo.Setup(repo => repo.AddAsync(It.IsAny<Bearing>()))
                .Returns(Task.CompletedTask);

            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            await _bearingService.AddBearingAsync(newBearing);

            // Assert
            _mockBearingRepo.Verify(repo => repo.AddAsync(It.IsAny<Bearing>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        // ✅ Test Case 4: Updating a Bearing
        [Fact]
        public async Task UpdateBearing_Should_Call_Update_And_SaveChanges()
        {
            // Arrange
            var existingBearing = new Bearing { Id = 4, Name = "Bearing D", Type = "Cylindrical", Manufacturer = "Company W", Size = 50.0m, SizeType = BearingSizeType.Diameter };

            _mockBearingRepo.Setup(repo => repo.Update(It.IsAny<Bearing>()));

            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            await _bearingService.UpdateBearingAsync(existingBearing);

            // Assert
            _mockBearingRepo.Verify(repo => repo.Update(It.IsAny<Bearing>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        // ✅ Test Case 5: Deleting an Existing Bearing
        [Fact]
        public async Task DeleteBearing_Should_Call_Delete_And_SaveChanges_If_Bearing_Exists()
        {
            // Arrange
            var bearingId = 5;
            var existingBearing = new Bearing { Id = bearingId, Name = "Bearing E", Type = "Magnetic", Manufacturer = "Company V", Size = 60.0m };

            _mockBearingRepo.Setup(repo => repo.GetByIdAsync(bearingId))
                .ReturnsAsync(existingBearing);

            _mockBearingRepo.Setup(repo => repo.Delete(It.IsAny<Bearing>()));

            _mockUnitOfWork.Setup(uow => uow.SaveAsync()).ReturnsAsync(1);

            // Act
            await _bearingService.DeleteBearingAsync(bearingId);

            // Assert
            _mockBearingRepo.Verify(repo => repo.Delete(It.IsAny<Bearing>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Once);
        }

        // ✅ Test Case 6: Deleting a Non-Existing Bearing (No Action Taken)
        [Fact]
        public async Task DeleteBearing_Should_Not_Call_Delete_If_Bearing_Does_Not_Exist()
        {
            // Arrange
            var bearingId = 6;
            _mockBearingRepo.Setup(repo => repo.GetByIdAsync(bearingId))
                .ReturnsAsync((Bearing)null); // Simulate non-existing bearing

            // Act
            await _bearingService.DeleteBearingAsync(bearingId);

            // Assert
            _mockBearingRepo.Verify(repo => repo.Delete(It.IsAny<Bearing>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Never);
        }
    }
}

