using System;
using KIP_POST_APP.DB;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.V1.Controllers
{
    public class ProfControllerTests
    {
        private readonly Mock<ILogger<ProfController>> loggerMock;
        private readonly Mock<ServerContext> serverContextMock;

        public ProfControllerTests()
        {
            this.loggerMock = new Mock<ILogger<ProfController>>();
            this.serverContextMock = new Mock<ServerContext>();

            // this.serverContextMock.Setup(c => c.Audience).Returns(new DbSet<Audience>());
        }

        [Fact]
        public void AudienceController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new ProfController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new ProfController(Mock.Of<ILogger<ProfController>>(), null));

            _ = new ProfController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
