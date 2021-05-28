using System;
using KIP_POST_APP.DB;
using KIP_server_GET.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.Controllers
{
    public class FacultyControllerTests
    {
        private readonly Mock<ILogger<FacultyController>> loggerMock;
        private readonly Mock<ServerContext> serverContextMock;

        public FacultyControllerTests()
        {
            this.loggerMock = new Mock<ILogger<FacultyController>>();
            this.serverContextMock = new Mock<ServerContext>();

            // this.serverContextMock.Setup(c => c.Audience).Returns(new DbSet<Audience>());
        }

        [Fact]
        public void AudienceController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new FacultyController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new FacultyController(Mock.Of<ILogger<FacultyController>>(), null));

            _ = new FacultyController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
