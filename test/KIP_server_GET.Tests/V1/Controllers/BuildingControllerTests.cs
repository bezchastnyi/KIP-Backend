using System;
using KIP_POST_APP.DB;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.V1.Controllers
{
    public class BuildingControllerTests
    {
        private readonly Mock<ILogger<BuildingController>> loggerMock;
        private readonly Mock<ServerContext> serverContextMock;

        public BuildingControllerTests()
        {
            this.loggerMock = new Mock<ILogger<BuildingController>>();
            this.serverContextMock = new Mock<ServerContext>();

            // this.serverContextMock.Setup(c => c.Audience).Returns(new DbSet<Audience>());
        }

        [Fact]
        public void AudienceController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new BuildingController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new BuildingController(Mock.Of<ILogger<BuildingController>>(), null));

            _ = new BuildingController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
