using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PilotUtilityApi.Domain.Models.Responses;
using PilotUtilityApi.Services.Services;
using PilotUtilityApi.Web.Controllers;
using System;
using System.Threading.Tasks;

namespace PilotUtilityApi.Web.Tests.Controllers
{
	/// <summary>
	/// Unit tests for <see cref="TestingController"/>.
	/// </summary>
	public class TestingControllerTests : TestBase
	{
		[Test]
		public void TestingController_Constructor_WithNullService_ThrowsArgumentNullException_Test()
		{
			Assert.Throws<ArgumentNullException>(() => new TestingController(null!));
		}

		[Test]
		public void TestingController_Constructor_WithValidService_DoesNotThrow_Test()
		{
			var mockService = new Mock<ITestingService>();

			Assert.DoesNotThrow(() => new TestingController(mockService.Object));
		}

		[Test]
		public async Task TestingController_ResetTesting_WithSuccessAndDeletedRows_ReturnsOkWithCount_Test()
		{
			var expectedResult = new RetrieveResponse<int>(5);
			var mockService = new Mock<ITestingService>();
			mockService.Setup(s => s.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var controller = new TestingController(mockService.Object);

			var result = await controller.ResetTesting();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			var okResult = result as OkObjectResult;
			Assert.That(okResult, Is.Not.Null);
			Assert.That(okResult!.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
			Assert.That(okResult.Value, Is.EqualTo(5));
			mockService.Verify(s => s.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingController_ResetTesting_WithSuccessAndZeroDeletedRows_ReturnsNoContent_Test()
		{
			var expectedResult = new RetrieveResponse<int>(0);
			var mockService = new Mock<ITestingService>();
			mockService.Setup(s => s.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var controller = new TestingController(mockService.Object);

			var result = await controller.ResetTesting();

			Assert.That(result, Is.InstanceOf<NoContentResult>());
			var noContentResult = result as NoContentResult;
			Assert.That(noContentResult, Is.Not.Null);
			Assert.That(noContentResult!.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
			mockService.Verify(s => s.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingController_ResetTesting_WithNullResult_ReturnsNoContent_Test()
		{
			var expectedResult = new RetrieveResponse<int>(0, null);
			expectedResult.Result = 0;
			var mockService = new Mock<ITestingService>();
			mockService.Setup(s => s.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var controller = new TestingController(mockService.Object);

			var result = await controller.ResetTesting();

			Assert.That(result, Is.InstanceOf<NoContentResult>());
			mockService.Verify(s => s.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingController_ResetTesting_WithError_ReturnsBadRequestWithWarningHeader_Test()
		{
			var errorMessage = "Database connection failed";
			var expectedResult = new RetrieveResponse<int>(0, errorMessage);
			var mockService = new Mock<ITestingService>();
			mockService.Setup(s => s.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var controller = new TestingController(mockService.Object);
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext()
			};

			var result = await controller.ResetTesting();

			Assert.That(result, Is.InstanceOf<BadRequestResult>());
			var badRequestResult = result as BadRequestResult;
			Assert.That(badRequestResult, Is.Not.Null);
			Assert.That(badRequestResult!.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
			Assert.That(controller.Response.Headers.ContainsKey("Warning"), Is.True);
			Assert.That(controller.Response.Headers["Warning"].ToString(), Is.EqualTo(errorMessage));
			mockService.Verify(s => s.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingController_ResetTesting_WithErrorAndNullMessage_ReturnsBadRequestWithDefaultWarning_Test()
		{
			var expectedResult = new RetrieveResponse<int>(0, null);
			expectedResult.ErrorMessage = null;
			var mockService = new Mock<ITestingService>();
			mockService.Setup(s => s.ResetTestingAsync())
				.ReturnsAsync(new RetrieveResponse<int>(0, "test error"));

			var controller = new TestingController(mockService.Object);
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext()
			};

			var result = await controller.ResetTesting();

			Assert.That(result, Is.InstanceOf<BadRequestResult>());
			Assert.That(controller.Response.Headers.ContainsKey("Warning"), Is.True);
			mockService.Verify(s => s.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingController_ResetTesting_WithMultipleDeletedRows_ReturnsOkWithCorrectCount_Test()
		{
			var expectedResult = new RetrieveResponse<int>(8);
			var mockService = new Mock<ITestingService>();
			mockService.Setup(s => s.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var controller = new TestingController(mockService.Object);

			var result = await controller.ResetTesting();

			Assert.That(result, Is.InstanceOf<OkObjectResult>());
			var okResult = result as OkObjectResult;
			Assert.That(okResult!.Value, Is.EqualTo(8));
			mockService.Verify(s => s.ResetTestingAsync(), Times.Once);
		}
	}
}
