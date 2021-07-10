using System;
using AutoMapper;
using KIP_server_AUTH.Interfaces;
using KIP_server_AUTH.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.V1.Controllers
{
    public class PersonalInformationControllerTests
    {
        private readonly PersonalInformationController _controller;
        private readonly Mock<ILogger<PersonalInformationController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDeserializeService> _deserializeServiceMock;

        public PersonalInformationControllerTests()
        {
            this._loggerMock = new Mock<ILogger<PersonalInformationController>>();
            this._loggerMock.Setup(logger => logger.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true)
                .Callback(() => this._loggerMock.Verify(logger => logger.IsEnabled(It.IsAny<LogLevel>())));

            this._mapperMock = new Mock<IMapper>();
            this._deserializeServiceMock = new Mock<IDeserializeService>();

            this._controller = new PersonalInformationController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }

        [Fact]
        public void PersonalInformationController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                "logger", () => new PersonalInformationController(null, null, null));

            Assert.Throws<ArgumentNullException>(
                "mapper", () => new PersonalInformationController(Mock.Of<ILogger<PersonalInformationController>>(), null, null));

            Assert.Throws<ArgumentNullException>(
                "deserializeService", () => new PersonalInformationController(
                    Mock.Of<ILogger<PersonalInformationController>>(),
                    this._mapperMock.Object,
                    null));

            _ = new PersonalInformationController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }
    }
}
