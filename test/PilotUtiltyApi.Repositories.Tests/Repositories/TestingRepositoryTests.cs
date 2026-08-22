using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PilotUtilityApi.Repositories.Repositories;
using PilotUtilityApi.Shared.Configuration;
using PilotUtilityApi.Shared.Configuration.Models;
using PilotUtilityApi.Shared.Exceptions;
using PilotUtilityApi.TestingShared.Utilities;
using System;
using System.Threading.Tasks;

namespace PilotUtilityApi.Repositories.Tests.Repositories
{
	/// <summary>
	/// Unit tests for <see cref="TestingRepository"/>.
	/// </summary>
	public class TestingRepositoryTests : TestBase
	{
		[Test]
		public void TestingRepository_Constructor_WithNullConfiguration_ThrowsArgumentNullException_Test()
		{
			var loggerFactory = TestingSharedDoublesUtilities.GetMockLoggerFactory();
			Assert.Throws<ArgumentNullException>(() => new TestingRepository(loggerFactory, null!));
		}

		[Test]
		public void TestingRepository_Constructor_WithValidConfiguration_DoesNotThrow_Test()
		{
			var loggerFactory = TestingSharedDoublesUtilities.GetMockLoggerFactory();
			var mockConfig = new Mock<IApplicationConfiguration>();
			mockConfig.Setup(c => c.DataSources).Returns(
				[
					new DataSourceConfiguration
					{
						Active = true,
						DataSourceType = "SqlServer",
						Host = "localhost",
						DataSource = "TestDb",
						Port = 1433,
						UserName = "sa",
						Password = "password",
						ConnectTimeout = 30,
						Schema = "dbo"
					}
				]);

			Assert.DoesNotThrow(() => new TestingRepository(loggerFactory, mockConfig.Object));
		}

		[Test]
		public async Task TestingRepository_ResetTestingAsync_WithNoActiveDataSource_ReturnsError_Test()
		{
			var mockConfig = new Mock<IApplicationConfiguration>();
			mockConfig.Setup(c => c.DataSources).Returns(
				[
					new DataSourceConfiguration
					{
						Active = false,
						DataSourceType = "SqlServer",
						Host = "localhost",
						DataSource = "TestDb"
					}
				]);

			var loggerFactory = TestingSharedDoublesUtilities.GetMockLoggerFactory();
			var repository = new TestingRepository(loggerFactory, mockConfig.Object);

			var result = await repository.ResetTestingAsync();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.IsError, Is.True);
			Assert.That(result.ErrorMessage, Does.Contain("No active data source configured"));
		}

		[Test]
		public async Task TestingRepository_ResetTestingAsync_WithEmptyDataSources_ReturnsError_Test()
		{
			var mockConfig = new Mock<IApplicationConfiguration>();
			mockConfig.Setup(c => c.DataSources).Returns([]);

			var loggerFactory = TestingSharedDoublesUtilities.GetMockLoggerFactory();
			var repository = new TestingRepository(loggerFactory, mockConfig.Object);

			var result = await repository.ResetTestingAsync();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.IsError, Is.True);
			Assert.That(result.ErrorMessage, Does.Contain("No active data source configured"));
		}

		[Test]
		public void TestingRepository_ResetTestingAsync_WithUnsupportedDataSourceType_ThrowsUserException_Test()
		{
			var mockConfig = new Mock<IApplicationConfiguration>();
			mockConfig.Setup(c => c.DataSources).Returns(
				[
					new DataSourceConfiguration
					{
						Active = true,
						DataSourceType = "UnsupportedDb",
						Host = "localhost",
						DataSource = "SqlServer",
						Port = 1234,
						UserName = "user",
						Password = "password",
						ConnectTimeout = 30
					}
				]);

			var loggerFactory = TestingSharedDoublesUtilities.GetMockLoggerFactory();
			var repository = new TestingRepository(loggerFactory, mockConfig.Object);

			Assert.ThrowsAsync<UserException>(async () => await repository.ResetTestingAsync());
		}
	}
}
