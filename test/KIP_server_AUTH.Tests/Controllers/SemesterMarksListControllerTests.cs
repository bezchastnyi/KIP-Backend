using System;
using AutoMapper;
using KIP_server_AUTH.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.Controllers
{
    public class SemesterMarksListControllerTests
    {
        private readonly Mock<ILogger<SemesterMarksListController>> loggerMock;
        private readonly Mock<IMapper> mapperMock;

        public SemesterMarksListControllerTests()
        {
            this.loggerMock = new Mock<ILogger<SemesterMarksListController>>();
            this.mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void SemesterMarksListController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new SemesterMarksListController(null, null));

            Assert.Throws<ArgumentNullException>("mapper",
                () => new SemesterMarksListController(Mock.Of<ILogger<SemesterMarksListController>>(), null));

            _ = new SemesterMarksListController(this.loggerMock.Object, this.mapperMock.Object);
        }
    }
}
