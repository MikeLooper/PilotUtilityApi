using Moq;
using NUnit.Framework;
using PilotUtilityApi.Repositories.Repositories;
using PilotUtilityApi.Shared.Configuration;
using PilotUtilityApi.Shared.Configuration.Models;
using PilotUtilityApi.Shared.Exceptions;
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
			Assert.Throws<ArgumentNullException>(() => new TestingRepository(null!));
		}

		[Test]
		public void TestingRepository_Constructor_WithValidConfiguration_DoesNotThrow_Test()
		{
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

			Assert.DoesNotThrow(() => new TestingRepository(mockConfig.Object));
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

			var repository = new TestingRepository(mockConfig.Object);

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

			var repository = new TestingRepository(mockConfig.Object);

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
						DataSource = "TestDb",
						Port = 1234,
						UserName = "user",
						Password = "password",
						ConnectTimeout = 30
					}
				]);

			var repository = new TestingRepository(mockConfig.Object);

			Assert.ThrowsAsync<UserException>(async () => await repository.ResetTestingAsync());
		}
	}
}
