using System;
using KIP_POST_APP.DB;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.V1.Controllers
{
    public class AudienceControllerTests
    {
        private readonly Mock<ILogger<AudienceController>> loggerMock;
        private readonly Mock<POSTContext> serverContextMock;

        public AudienceControllerTests()
        {
            this.loggerMock = new Mock<ILogger<AudienceController>>();
            this.serverContextMock = new Mock<POSTContext>();

            // this.serverContextMock.Setup(c => c.Audience).Returns(new DbSet<Audience>());
        }

        [Fact]
        public void AudienceController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new AudienceController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new AudienceController(Mock.Of<ILogger<AudienceController>>(), null));

            _ = new AudienceController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
