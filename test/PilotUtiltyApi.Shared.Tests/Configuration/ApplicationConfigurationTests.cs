using NUnit.Framework;
using PilotUtilityApi.Shared.Configuration;
using PilotUtilityApi.Shared.Configuration.Models;
using PilotUtilityApi.Shared.Exceptions;

namespace PilotUtilityApi.Shared.Tests.Configuration
{
	/// <summary>
	/// Unit tests for <see cref="ApplicationConfiguration"/>.
	/// </summary>
	public class ApplicationConfigurationTests : TestBase
	{
		[Test]
		public void ApplicationConfiguration_Validate_WithNoDataSources_ThrowsConfigurationException_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources = []
			};

			var exception = Assert.Throws<ConfigurationException>(() => config.Validate());

			Assert.That(exception, Is.Not.Null);
			Assert.That(exception.Message, Does.Contain("No data sources configured"));
		}

		[Test]
		public void ApplicationConfiguration_Validate_WithNullDataSources_ThrowsConfigurationException_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources = null!
			};

			var exception = Assert.Throws<ConfigurationException>(() => config.Validate());

			Assert.That(exception, Is.Not.Null);
			Assert.That(exception.Message, Does.Contain("No data sources configured"));
		}

		[Test]
		public void ApplicationConfiguration_Validate_WithNoActiveDataSource_ThrowsConfigurationException_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources =
				[
					new DataSourceConfiguration
					{
						Active = false,
						DataSourceType = "SqlServer",
						Host = "localhost",
						DataSource = "TestDb"
					}
				]
			};

			var exception = Assert.Throws<ConfigurationException>(() => config.Validate());

			Assert.That(exception, Is.Not.Null);
			Assert.That(exception.Message, Does.Contain("No active data source configured"));
		}

		[Test]
		public void ApplicationConfiguration_Validate_WithMultipleActiveDataSources_ThrowsConfigurationException_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources =
				[
					new DataSourceConfiguration
					{
						Active = true,
						DataSourceType = "SqlServer",
						Host = "localhost",
						DataSource = "TestDb1"
					},
					new DataSourceConfiguration
					{
						Active = true,
						DataSourceType = "PostgreSQL",
						Host = "localhost",
						DataSource = "TestDb2"
					}
				]
			};

			var exception = Assert.Throws<ConfigurationException>(() => config.Validate());

			Assert.That(exception, Is.Not.Null);
			Assert.That(exception.Message, Does.Contain("Multiple active data sources configured"));
		}

		[Test]
		public void ApplicationConfiguration_Validate_WithMissingDataSourceType_ThrowsConfigurationException_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources =
				[
					new DataSourceConfiguration
					{
						Active = true,
						DataSourceType = "",
						Host = "localhost",
						DataSource = "TestDb"
					}
				]
			};

			var exception = Assert.Throws<ConfigurationException>(() => config.Validate());

			Assert.That(exception, Is.Not.Null);
			Assert.That(exception.Message, Does.Contain("DataSourceType"));
		}

		[Test]
		public void ApplicationConfiguration_Validate_WithMissingHost_ThrowsConfigurationException_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources =
				[
					new DataSourceConfiguration
					{
						Active = true,
						DataSourceType = "SqlServer",
						Host = "",
						DataSource = "TestDb"
					}
				]
			};

			var exception = Assert.Throws<ConfigurationException>(() => config.Validate());

			Assert.That(exception, Is.Not.Null);
			Assert.That(exception.Message, Does.Contain("Host"));
		}

		[Test]
		public void ApplicationConfiguration_Validate_WithMissingDataSource_ThrowsConfigurationException_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources =
				[
					new DataSourceConfiguration
					{
						Active = true,
						DataSourceType = "SqlServer",
						Host = "localhost",
						DataSource = ""
					}
				]
			};

			var exception = Assert.Throws<ConfigurationException>(() => config.Validate());

			Assert.That(exception, Is.Not.Null);
			Assert.That(exception.Message, Does.Contain("DataSource"));
		}

		[Test]
		public void ApplicationConfiguration_Validate_WithValidConfiguration_DoesNotThrow_Test()
		{
			var config = new ApplicationConfiguration
			{
				DataSources =
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
				],
				OpenApi = new OpenApiConfiguration
				{
					Title = "Test API",
					Version = "1.0.0"
				}
			};

			Assert.DoesNotThrow(() => config.Validate());
		}

		[Test]
		public void ApplicationConfiguration_DataSources_DefaultsToEmptyArray_Test()
		{
			var config = new ApplicationConfiguration();

			Assert.That(config.DataSources, Is.Not.Null);
			Assert.That(config.DataSources, Is.Empty);
		}

		[Test]
		public void ApplicationConfiguration_OpenApi_DefaultsToNewInstance_Test()
		{
			var config = new ApplicationConfiguration();

			Assert.That(config.OpenApi, Is.Not.Null);
		}
	}
}
