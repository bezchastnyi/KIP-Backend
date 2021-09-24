using System;
using AutoMapper;
using KIP_server_Auth.Interfaces;
using KIP_server_Auth.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_Auth.Tests.V1.Controllers
{
    public class DebtListControllerTests
    {
        private readonly DebtListController _controller;
        private readonly Mock<ILogger<DebtListController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDeserializeService> _deserializeServiceMock;

        public DebtListControllerTests()
        {
            this._loggerMock = new Mock<ILogger<DebtListController>>();
            this._loggerMock.Setup(logger => logger.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true)
                .Callback(() => this._loggerMock.Verify(logger => logger.IsEnabled(It.IsAny<LogLevel>())));

            this._mapperMock = new Mock<IMapper>();
            this._deserializeServiceMock = new Mock<IDeserializeService>();

            this._controller = new DebtListController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }

        [Fact]
        public void DebtListController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                "logger", () => new DebtListController(null, null, null));

            Assert.Throws<ArgumentNullException>(
                "mapper", () => new DebtListController(Mock.Of<ILogger<DebtListController>>(), null, null));

            Assert.Throws<ArgumentNullException>(
                "deserializeService", () => new DebtListController(Mock.Of<ILogger<DebtListController>>(), this._mapperMock.Object, null));

            _ = new DebtListController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }
    }
}
