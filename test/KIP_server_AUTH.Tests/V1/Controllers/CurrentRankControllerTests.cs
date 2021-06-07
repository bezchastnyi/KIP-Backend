using System;
using AutoMapper;
using KIP_server_AUTH.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.V1.Controllers
{
    public class CurrentRankControllerTests
    {
        private readonly Mock<ILogger<CurrentRankController>> loggerMock;
        private readonly Mock<IMapper> mapperMock;

        public CurrentRankControllerTests()
        {
            this.loggerMock = new Mock<ILogger<CurrentRankController>>();
            this.mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void CurrentRankController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new CurrentRankController(null, null));

            Assert.Throws<ArgumentNullException>("mapper",
                () => new CurrentRankController(Mock.Of<ILogger<CurrentRankController>>(), null));

            _ = new CurrentRankController(this.loggerMock.Object, this.mapperMock.Object);
        }
    }
}
