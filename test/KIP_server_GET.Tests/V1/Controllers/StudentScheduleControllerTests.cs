using System;
using KIP_POST_APP.DB;
using KIP_server_GET.V1.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_GET.Tests.V1.Controllers
{
    public class StudentScheduleControllerTests
    {
        private readonly Mock<ILogger<StudentScheduleController>> loggerMock;
        private readonly Mock<POSTContext> serverContextMock;

        public StudentScheduleControllerTests()
        {
            this.loggerMock = new Mock<ILogger<StudentScheduleController>>();
            this.serverContextMock = new Mock<POSTContext>();
        }

        [Fact]
        public void StudentScheduleControllerr_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new StudentScheduleController(null, null));

            Assert.Throws<ArgumentNullException>("context",
                () => new StudentScheduleController(Mock.Of<ILogger<StudentScheduleController>>(), null));

            _ = new StudentScheduleController(this.loggerMock.Object, this.serverContextMock.Object);
        }
    }
}
