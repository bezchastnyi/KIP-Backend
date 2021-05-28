using System;
using KIP_server_GET.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<ILogger<HomeController>> loggerMock;
        private readonly Mock<IConfiguration> configuration;

        public HomeControllerTests()
        {
            this.loggerMock = new Mock<ILogger<HomeController>>();
            this.configuration = new Mock<IConfiguration>();
        }

        [Fact]
        public void HomeController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new HomeController(null, null));

            Assert.Throws<ArgumentNullException>("configuration",
                () => new HomeController(Mock.Of<ILogger<HomeController>>(), null));

            _ = new HomeController(this.loggerMock.Object, this.configuration.Object);
        }
    }
}
