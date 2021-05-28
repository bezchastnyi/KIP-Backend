using System;
using AutoMapper;
using KIP_server_AUTH.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.Controllers
{
    public class SemesterStudyingPlanControllerTests
    {
        private readonly Mock<ILogger<SemesterStudyingPlanController>> loggerMock;
        private readonly Mock<IMapper> mapperMock;

        public SemesterStudyingPlanControllerTests()
        {
            this.loggerMock = new Mock<ILogger<SemesterStudyingPlanController>>();
            this.mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void SemesterStudyingPlanController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new SemesterStudyingPlanController(null, null));

            Assert.Throws<ArgumentNullException>("mapper",
                () => new SemesterStudyingPlanController(Mock.Of<ILogger<SemesterStudyingPlanController>>(), null));

            _ = new SemesterStudyingPlanController(this.loggerMock.Object, this.mapperMock.Object);
        }
    }
}
