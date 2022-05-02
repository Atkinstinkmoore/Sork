using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Sork.SorkLog;
using Sork.SorkLog.Core;
using Sork.SorkLog.Implementations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Sork.Test.Unittests
{
    public class SorkLogTest
    {

        [Fact]
        public async Task SorkLog_logs_while_information_and_status_200()
        {
            var logger = new Mock<ILogger<SorkLogFilter>>();
            var httpContext = new DefaultHttpContext();
            var actionResult = new Mock<IActionResult>();
            logger.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(true);
            
            actionResult.Object.Equals(actionResult.Object);

            using Activity activity = new Activity("unitTest");
            activity.Start();
            

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            actionContext.HttpContext.Response.StatusCode = 200;

            var metadata = new List<IFilterMetadata>();

            var context = new ResultExecutedContext(
                actionContext,
                metadata,
                actionResult.Object,
                new object());


            var sut = new SorkLogFilter(logger.Object);
            sut.OnResultExecuted(context);
            activity.Stop();

            Assert.Equal(2, logger.Invocations.Count);
            //logger.Verify(logger => logger.Log(
            //    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
            //    It.Is<EventId>(eventId => eventId.Id == 0),
            //    It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "myMessage" && @type.Name == "FormattedLogValues"),
            //    It.IsAny<Exception>(),
            //    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            //    Times.Once);

        }
        [Fact]
        public async Task SorkLog_skips_logging_while_warning_and_status_200()
        {
            var logger = new Mock<ILogger<SorkLogFilter>>();
            var httpContext = new DefaultHttpContext();
            var actionResult = new Mock<IActionResult>();
            logger.Setup(x => x.IsEnabled(LogLevel.Information)).Returns(false);
            logger.Setup(x => x.IsEnabled(LogLevel.Warning)).Returns(true);
            
            actionResult.Object.Equals(actionResult.Object);

            var actionContext = new ActionContext
            {
                HttpContext = httpContext,
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor(),
            };

            actionContext.HttpContext.Response.StatusCode = 200;

            var metadata = new List<IFilterMetadata>();

            var context = new ResultExecutedContext(
                actionContext,
                metadata,
                actionResult.Object,
                new object());


            var sut = new SorkLogFilter(logger.Object);
            sut.OnResultExecuted(context);

            Assert.Equal(1, logger.Invocations.Count);
            
        }

        [Fact]
        public async Task GetLogCommand_status_200_returns_informationcommand()
        {
            var logger = new Mock<ILogger<SorkLogFilter>>();

            var response = logger.Object.GetLogCommand(200);

            Assert.IsType<LogInformationCommand>(response);
        }
        [Fact]
        public async Task GetLogCommand_status_400_returns_warningcommand()
        {
            var logger = new Mock<ILogger<SorkLogFilter>>();
            
            var response = logger.Object.GetLogCommand(400);

            Assert.IsType<LogWarningCommand>(response);
        }
        [Fact]
        public async Task GetLogCommand_status_500_returns_errorcommand()
        {
            var logger = new Mock<ILogger<SorkLogFilter>>();
           
            var response = logger.Object.GetLogCommand(500);

            Assert.IsType<LogErrorCommand>(response);
        }

    }
}
