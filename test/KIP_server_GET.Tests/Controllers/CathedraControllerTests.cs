using System;
using KIP_POST_APP.DB;
using KIP_server_GET.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.Controllers
{
    public class CathedraControllerTests
    {
        private readonly Mock<ILogger<CathedraController>> loggerMock;
        private readonly Mock<ServerContext> serverContextMock;

        public CathedraControllerTests()
        {
            this.loggerMock = new Mock<ILogger<CathedraController>>();
            this.serverContextMock = new Mock<ServerContext>();

            // this.serverContextMock.Setup(c => c.Audience).Returns(new DbSet<Audience>());
        }

        [Fact]
        public void AudienceController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new CathedraController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new CathedraController(Mock.Of<ILogger<CathedraController>>(), null));

            _ = new CathedraController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
