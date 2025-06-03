
using Xunit;
using Moq;
using NationalMicrochipRegistry.Business.Services;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Data.Models;
using System.Collections.Generic;

public class ClinicServiceTests
{
    [Fact]
    public void AddClinic_ShouldCallRepositoryAdd()
    {
        var mockRepo = new Mock<IClinicRepository>();
        var service = new ClinicService(mockRepo.Object);
        var clinic = new Clinic { Id = 1, Name = "Vet Clinic" };

        service.AddClinic(clinic);

        mockRepo.Verify(r => r.Add(clinic), Times.Once);
    }
}
