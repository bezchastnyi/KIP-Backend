using System;
using AutoMapper;
using KIP_server_AUTH.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.Controllers
{
    public class PersonalInformationControllerTests
    {
        private readonly Mock<ILogger<PersonalInformationController>> loggerMock;
        private readonly Mock<IMapper> mapperMock;

        public PersonalInformationControllerTests()
        {
            this.loggerMock = new Mock<ILogger<PersonalInformationController>>();
            this.mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public void PersonalInformationController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new PersonalInformationController(null, null));

            Assert.Throws<ArgumentNullException>("mapper",
                () => new PersonalInformationController(Mock.Of<ILogger<PersonalInformationController>>(), null));

            _ = new PersonalInformationController(this.loggerMock.Object, this.mapperMock.Object);
        }
    }
}
