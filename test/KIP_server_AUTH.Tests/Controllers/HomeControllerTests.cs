using System;
using System.Net;
using KIP_server_AUTH.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KIP_server_AUTH.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller;
        private readonly Mock<ILogger<HomeController>> _loggerMock;

        public HomeControllerTests()
        {
            this._loggerMock = new Mock<ILogger<HomeController>>();

            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(p => p.Features).Returns(new FeatureCollection());

            this._controller = new HomeController(this._loggerMock.Object)
            {
                ControllerContext = new ControllerContext(
                    new ActionContext(httpContext.Object, new RouteData(), new ControllerActionDescriptor())),
            };
        }

        [Fact]
        public void HomeController_NullArgumentsPassed_ExceptionThrown()
        {
            //Act & Assert
            Assert.Throws<ArgumentNullException>("logger",
                () => new HomeController(null));

            _ = new HomeController(this._loggerMock.Object);
        }

        [Fact]
        public void HomeController_Home_ReturnsOkResult()
        {
            var result = this._controller.Home();
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
            var value = Assert.IsAssignableFrom<string>(okObjectResult.Value);

            Assert.Contains("testhost", value);
        }

        [Fact]
        public void HomeController_Error_ReturnsBadRequestResult()
        {
            var result = this._controller.Error();
            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
            Assert.NotNull(objectResult.Value);
        }
    }
}
