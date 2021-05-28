using System;
using AutoMapper;
using KIP_server_AUTH.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.Controllers
{
    public class DebtListControllerTests
    {
        private readonly Mock<ILogger<DebtListController>> loggerMock;
        private readonly Mock<IMapper> mapperMock;

        public DebtListControllerTests()
        {
            this.loggerMock = new Mock<ILogger<DebtListController>>();
            this.mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void DebtListController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new DebtListController(null, null));

            Assert.Throws<ArgumentNullException>("mapper",
                () => new DebtListController(Mock.Of<ILogger<DebtListController>>(), null));

            _ = new DebtListController(this.loggerMock.Object, this.mapperMock.Object);
        }
    }
}
