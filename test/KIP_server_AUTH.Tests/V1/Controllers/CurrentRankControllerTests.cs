using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KIP_Backend.Models.Auth;
using KIP_server_Auth.Interfaces;
using KIP_server_Auth.Models.KhPI;
using KIP_server_Auth.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_Auth.Tests.V1.Controllers
{
    public class CurrentRankControllerTests
    {
        private readonly CurrentRankController _controller;
        private readonly Mock<ILogger<CurrentRankController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IDeserializeService> _deserializeServiceMock;

        public CurrentRankControllerTests()
        {
            this._loggerMock = new Mock<ILogger<CurrentRankController>>();
            this._loggerMock.Setup(logger => logger.IsEnabled(It.IsAny<LogLevel>()))
                .Returns(true)
                .Callback(() => this._loggerMock.Verify(logger => logger.IsEnabled(It.IsAny<LogLevel>())));

            this._mapperMock = new Mock<IMapper>();
            this._deserializeServiceMock = new Mock<IDeserializeService>();

            this._controller = new CurrentRankController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }

        [Fact]
        public void CurrentRankController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>(
                "logger", () => new CurrentRankController(null, null, null));

            Assert.Throws<ArgumentNullException>(
                "mapper", () => new CurrentRankController(Mock.Of<ILogger<CurrentRankController>>(), null, null));

            Assert.Throws<ArgumentNullException>(
                "deserializeService", () => new CurrentRankController(Mock.Of<ILogger<CurrentRankController>>(), this._mapperMock.Object, null));

            _ = new CurrentRankController(
                this._loggerMock.Object, this._mapperMock.Object, this._deserializeServiceMock.Object);
        }

        [Fact]
        public async Task CurrentRank_DataProcessed_LogsVerified()
        {
            // Arange
            var email = "email";
            var password = "password";
            var url = "url";

            var curentRankKhPIList = new List<CurrentRankKhPI>()
            {
                new CurrentRankKhPI()
                {
                    n = "n",
                    studid = "studid",
                    fio = "fio",
                    group = "group",
                    sbal100 = "sbal100",
                    sbal5 = "sbal5",
                    rating = "rating",
                },

                new CurrentRankKhPI()
                {
                    n = "n",
                    studid = "studid",
                    fio = "fio",
                    group = "group",
                    sbal100 = "sbal100",
                    sbal5 = "sbal5",
                    rating = "rating",
                },

                new CurrentRankKhPI()
                {
                    n = "n",
                    studid = "studid",
                    fio = "fio",
                    group = "group",
                    sbal100 = "sbal100",
                    sbal5 = "sbal5",
                    rating = "rating",
                },
            };

            var curentRankList = new List<CurrentRank>()
            {
                new CurrentRank(),
                new CurrentRank(),
                new CurrentRank(),
            };

            this._deserializeServiceMock.Setup(service => service.ExecuteAsync<CurrentRankKhPI>(url))
                .ReturnsAsync(curentRankKhPIList);

            this._mapperMock.Setup(service => service.Map<ICollection<CurrentRank>>(curentRankKhPIList))
                .Returns(curentRankList);

            // Act
            var result = await this._controller.CurrentRank(email, password);
        }
    }
}
