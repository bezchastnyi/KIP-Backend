using System;
using KIP_POST_APP.DB;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.V1.Controllers
{
    public class ProfScheduleControllerTests
    {
        private readonly Mock<ILogger<ProfScheduleController>> loggerMock;
        private readonly Mock<POSTContext> serverContextMock;

        public ProfScheduleControllerTests()
        {
            this.loggerMock = new Mock<ILogger<ProfScheduleController>>();
            this.serverContextMock = new Mock<POSTContext>();
        }

        [Fact]
        public void ProfScheduleController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new ProfScheduleController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new ProfScheduleController(Mock.Of<ILogger<ProfScheduleController>>(), null));

            _ = new ProfScheduleController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
