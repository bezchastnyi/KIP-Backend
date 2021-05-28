using System;
using KIP_POST_APP.DB;
using KIP_server_GET.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.Controllers
{
    public class AudienceScheduleControllerTests
    {
        private readonly Mock<ILogger<AudienceScheduleController>> loggerMock;
        private readonly Mock<ServerContext> serverContextMock;

        public AudienceScheduleControllerTests()
        {
            this.loggerMock = new Mock<ILogger<AudienceScheduleController>>();
            this.serverContextMock = new Mock<ServerContext>();
        }

        [Fact]
        public void AudienceScheduleController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new AudienceScheduleController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new AudienceScheduleController(Mock.Of<ILogger<AudienceScheduleController>>(), null));

            _ = new AudienceScheduleController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
