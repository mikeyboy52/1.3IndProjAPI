using IndProject.WebApi.Controllers;
using IndProject.WebApi.Interfaces;
using IndProject.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace IndProject.WebApi.Tests
{
    [TestClass]
    public class ObjectControllerTests
    {
        [TestMethod]
        public async Task Add_WhenObjectValid_ShouldCreateObjectAndReturnOk()
        {
            Object2D object2D = new Object2D()
            {
                Id = Guid.NewGuid(),
                EnviromentId = Guid.NewGuid(),
                PrefabId = 1,
                PositionX = 1,
                PositionY = 1,
                ScaleX = 1,
                ScaleY = 1,
                RotationZ = 1,
                SortingLayer = "Default"
            };
            var mockRepository = new Mock<IObjectRepository>();
            var mockLogger = new Mock<ILogger<Object2DController>>();

            var enviromentController = new Object2DController(mockRepository.Object, mockLogger.Object);

            var response = await enviromentController.Add(object2D);
            Assert.IsInstanceOfType(response, typeof(CreatedResult));
            mockRepository.Verify(x => x.InsertObject(object2D), Times.Once);
        }
    }
}
