using System;
using KIP_POST_APP.DB;
using KIP_server_GET.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.Controllers
{
    public class GroupControllerTests
    {
        private readonly Mock<ILogger<GroupController>> loggerMock;
        private readonly Mock<ServerContext> serverContextMock;

        public GroupControllerTests()
        {
            this.loggerMock = new Mock<ILogger<GroupController>>();
            this.serverContextMock = new Mock<ServerContext>();

            // this.serverContextMock.Setup(c => c.Audience).Returns(new DbSet<Audience>());
        }

        [Fact]
        public void AudienceController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new GroupController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new GroupController(Mock.Of<ILogger<GroupController>>(), null));

            _ = new GroupController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
