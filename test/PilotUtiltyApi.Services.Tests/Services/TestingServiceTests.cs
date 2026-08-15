using Moq;
using NUnit.Framework;
using PilotUtilityApi.Domain.Models.Responses;
using PilotUtilityApi.Repositories.Repositories;
using PilotUtilityApi.Services.Services;
using System;
using System.Threading.Tasks;

namespace PilotUtilityApi.Services.Tests.Services
{
	/// <summary>
	/// Unit tests for <see cref="TestingService"/>.
	/// </summary>
	public class TestingServiceTests : TestBase
	{
		[Test]
		public void TestingService_Constructor_WithNullRepository_ThrowsArgumentNullException_Test()
		{
			Assert.Throws<ArgumentNullException>(() => new TestingService(null!));
		}

		[Test]
		public void TestingService_Constructor_WithValidRepository_DoesNotThrow_Test()
		{
			var mockRepository = new Mock<ITestingRepository>();

			Assert.DoesNotThrow(() => new TestingService(mockRepository.Object));
		}

		[Test]
		public async Task TestingService_ResetTestingAsync_CallsRepository_ReturnsResult_Test()
		{
			var expectedResult = new RetrieveResponse<int>(5);
			var mockRepository = new Mock<ITestingRepository>();
			mockRepository.Setup(r => r.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var service = new TestingService(mockRepository.Object);

			var result = await service.ResetTestingAsync();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Result, Is.EqualTo(5));
			Assert.That(result.IsError, Is.False);
			mockRepository.Verify(r => r.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingService_ResetTestingAsync_WithRepositoryError_ReturnsError_Test()
		{
			var errorMessage = "Database connection failed";
			var expectedResult = new RetrieveResponse<int>(0, errorMessage);
			var mockRepository = new Mock<ITestingRepository>();
			mockRepository.Setup(r => r.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var service = new TestingService(mockRepository.Object);

			var result = await service.ResetTestingAsync();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.IsError, Is.True);
			Assert.That(result.ErrorMessage, Is.EqualTo(errorMessage));
			Assert.That(result.Result, Is.EqualTo(0));
			mockRepository.Verify(r => r.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingService_ResetTestingAsync_WithZeroDeletedRows_ReturnsZero_Test()
		{
			var expectedResult = new RetrieveResponse<int>(0);
			var mockRepository = new Mock<ITestingRepository>();
			mockRepository.Setup(r => r.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var service = new TestingService(mockRepository.Object);

			var result = await service.ResetTestingAsync();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Result, Is.EqualTo(0));
			Assert.That(result.IsError, Is.False);
			mockRepository.Verify(r => r.ResetTestingAsync(), Times.Once);
		}

		[Test]
		public async Task TestingService_ResetTestingAsync_WithMultipleDeletedRows_ReturnsCount_Test()
		{
			var expectedResult = new RetrieveResponse<int>(8);
			var mockRepository = new Mock<ITestingRepository>();
			mockRepository.Setup(r => r.ResetTestingAsync())
				.ReturnsAsync(expectedResult);

			var service = new TestingService(mockRepository.Object);

			var result = await service.ResetTestingAsync();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Result, Is.EqualTo(8));
			Assert.That(result.IsError, Is.False);
			mockRepository.Verify(r => r.ResetTestingAsync(), Times.Once);
		}
	}
}
