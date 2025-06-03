
using Xunit;
using Moq;
using NationalMicrochipRegistry.Business.Services;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Data.Models;
using NationalMicrochipRegistry.Data.Enums;

public class MicrochipServiceTests
{
    [Fact]
    public void AddMicrochip_ShouldCallRepositoryAdd()
    {
        var mockRepo = new Mock<IMicrochipRepository>();
        var service = new MicrochipService(mockRepo.Object);
        var chip = new Microchip { ChipNumber = "ABC123", Status = ChipStatus.Available };

        service.AddMicrochip(chip);

        mockRepo.Verify(r => r.Add(chip), Times.Once);
    }
}
