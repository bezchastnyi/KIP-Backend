using System;
using AutoMapper;
using KIP_server_Auth.Interfaces;
using KIP_server_Auth.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_Auth.Tests.V1.Controllers
{
    public class SemesterMarksListControllerTests
    {
        private readonly SemesterMarksListController _controller;
        private readonly Mock<ILogger<SemesterMarksListController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDeserializeService> _deserializeServiceMock;

        public SemesterMarksListControllerTests()
        {
            this._loggerMock = new Mock<ILogger<SemesterMarksListController>>();
            this._loggerMock.Setup(logger => logger.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true)
                .Callback(() => this._loggerMock.Verify(logger => logger.IsEnabled(It.IsAny<LogLevel>())));

            this._mapperMock = new Mock<IMapper>();
            this._deserializeServiceMock = new Mock<IDeserializeService>();

            this._controller = new SemesterMarksListController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }

        [Fact]
        public void SemesterMarksListController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                "logger", () => new SemesterMarksListController(null, null, null));

            Assert.Throws<ArgumentNullException>(
                "mapper", () => new SemesterMarksListController(Mock.Of<ILogger<SemesterMarksListController>>(), null, null));

            Assert.Throws<ArgumentNullException>(
                "deserializeService", () => new SemesterMarksListController(
                    Mock.Of<ILogger<SemesterMarksListController>>(),
                    this._mapperMock.Object,
                    null));

            _ = new SemesterMarksListController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }
    }
}
