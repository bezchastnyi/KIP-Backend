using System;
using KIP_server_AUTH.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> loggerMock;

        public HomeControllerTests()
        {
            this.loggerMock = new Mock<ILogger<HomeController>>();
        }

        [Fact]
        public void HomeController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new HomeController(null));

            _ = new HomeController(this.loggerMock.Object);
        }
    }
}
