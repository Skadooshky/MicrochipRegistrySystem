using Moq;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Business.Services;
using NationalMicrochipRegistry.Data.Enums;
using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;
using Xunit;

public class AnimalServiceTests
{
    [Fact]
    public void AddAnimal_ShouldCallRepositoryAdd()
    {
        var mockAnimalRepo = new Mock<IAnimalRepository>();
        var mockChipRepo = new Mock<IMicrochipRepository>();
        var service = new AnimalService(mockAnimalRepo.Object, mockChipRepo.Object);
        var animal = new Animal { Id = 1, Name = "Buddy", Type = AnimalType.Dog };

        service.AddAnimal(animal);

        mockAnimalRepo.Verify(r => r.Add(animal), Times.Once);
    }

    [Fact]
    public void GetAllAnimals_ShouldReturnList()
    {
        var mockAnimalRepo = new Mock<IAnimalRepository>();
        var mockChipRepo = new Mock<IMicrochipRepository>();
        mockAnimalRepo.Setup(r => r.GetAll()).Returns(new List<Animal> { new Animal { Name = "Buddy" } });

        var service = new AnimalService(mockAnimalRepo.Object, mockChipRepo.Object);
        var result = service.GetAllAnimals();

        Assert.Single(result);
        Assert.Equal("Buddy", result[0].Name);
    }
}
