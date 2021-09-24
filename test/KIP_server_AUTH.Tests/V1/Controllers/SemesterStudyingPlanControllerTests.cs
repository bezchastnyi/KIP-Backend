using System;
using AutoMapper;
using KIP_server_Auth.Interfaces;
using KIP_server_Auth.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_Auth.Tests.V1.Controllers
{
    public class SemesterStudyingPlanControllerTests
    {
        private readonly SemesterStudyingPlanController _controller;
        private readonly Mock<ILogger<SemesterStudyingPlanController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDeserializeService> _deserializeServiceMock;

        public SemesterStudyingPlanControllerTests()
        {
            this._loggerMock = new Mock<ILogger<SemesterStudyingPlanController>>();
            this._loggerMock.Setup(logger => logger.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true)
                .Callback(() => this._loggerMock.Verify(logger => logger.IsEnabled(It.IsAny<LogLevel>())));

            this._mapperMock = new Mock<IMapper>();
            this._deserializeServiceMock = new Mock<IDeserializeService>();

            this._controller = new SemesterStudyingPlanController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }

        [Fact]
        public void SemesterStudyingPlanController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                "logger", () => new SemesterStudyingPlanController(null, null, null));

            Assert.Throws<ArgumentNullException>(
                "mapper", () => new SemesterStudyingPlanController(Mock.Of<ILogger<SemesterStudyingPlanController>>(), null, null));

            Assert.Throws<ArgumentNullException>(
                "deserializeService", () => new SemesterStudyingPlanController(
                    Mock.Of<ILogger<SemesterStudyingPlanController>>(),
                    this._mapperMock.Object,
                    null));

            _ = new SemesterStudyingPlanController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }
    }
}
