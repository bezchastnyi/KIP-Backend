using System;
using KIP_POST_APP.DB;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.V1.Controllers
{
    public class AudienceScheduleControllerTests
    {
        private readonly Mock<ILogger<AudienceScheduleController>> loggerMock;
        private readonly Mock<POSTContext> serverContextMock;

        public AudienceScheduleControllerTests()
        {
            this.loggerMock = new Mock<ILogger<AudienceScheduleController>>();
            this.serverContextMock = new Mock<POSTContext>();
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
