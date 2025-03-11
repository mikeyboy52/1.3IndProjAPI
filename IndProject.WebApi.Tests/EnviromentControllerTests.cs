using IndProject.WebApi.Controllers;
using IndProject.WebApi.Interfaces;
using IndProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IndProject.WebApi.Tests
{
    [TestClass]
    public class EnviromentControllerTests
    {
        [TestMethod]
        public async Task Add_WhenEnviromentValid_ShouldCreateEnviromentAndReturnOk()
        {
            Enviroment enviroment = new Enviroment()
            {
                Id = Guid.NewGuid(),
                Name = "TestingWorld",
                Email = "testmail@bbbb.nl",
                MaxHeight = 120,
                MaxLength = 120
            };
            var mockRepository = new Mock<IEnviromentRepository>();
            var mockLogger = new Mock<ILogger<EnviromentController>>();

            var enviromentController = new EnviromentController(mockRepository.Object, mockLogger.Object);

            var response = await enviromentController.Add(enviroment);
            Assert.IsInstanceOfType(response, typeof(CreatedResult));
            mockRepository.Verify(x => x.InsertEnviroment(enviroment), Times.Once);
        }
        [TestMethod]
        public async Task Delete_WhenEnviromentExists_ShouldDeleteEnviromentAndReturnOk()
        {
            var EnviromentId = Guid.NewGuid();
            var mockRepository = new Mock<IEnviromentRepository>();
            var mockLogger = new Mock<ILogger<EnviromentController>>();
            mockRepository.Setup(x => x.ReadEnviroment(EnviromentId)).ReturnsAsync(new Enviroment());

            var enviromentController = new EnviromentController(mockRepository.Object, mockLogger.Object);

            var response = await enviromentController.Update(EnviromentId);
            Assert.IsInstanceOfType(response, typeof(OkResult));
            mockRepository.Verify(x => x.DeleteEnviroment(EnviromentId), Times.Once);
        }
    }
}
