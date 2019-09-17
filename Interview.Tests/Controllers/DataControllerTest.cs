using System;
using System.Web.Http;
using System.Web.Http.Results;
using Interview.Controllers;
using Interview.Domain;
using Interview.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interview.Tests.Controllers
{
    [TestClass]
    public class DataControllerTest
    {
        [TestMethod]
        public void Post_DataObjectPassed_OkResultReturned()
        {
            var repositoryMock = new Mock<IDataRepository>();

            // Arrange
            var controller = new DataController(repositoryMock.Object);

            // Act
            var result = controller.Post(new Data()) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void Post_ExceptionThrown_BadRequestReturned()
        {
            var repositoryMock = new Mock<IDataRepository>();

            repositoryMock.Setup(x => x.Add(It.IsAny<Data>())).Throws(new Exception());

            // Arrange
            var controller = new DataController(repositoryMock.Object);

            // Act
            var result = controller.Post(new Data()) as IHttpActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
    }
}
