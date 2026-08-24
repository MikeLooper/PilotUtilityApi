using PilotUtilityApi.Shared.Configuration;
using PilotUtilityApi.Shared.Configuration.Models;
using PilotUtilityApi.TestingShared.Doubles;
using System.Collections.Generic;

namespace PilotUtilityApi.TestingShared.Utilities
{
	/// <summary>
	/// Utility method for working with shared testing doubles in unit tests.
	/// </summary>
	public static class TestingSharedDoublesUtilities
	{
		/// <summary>
		/// Creates a mock application configuration for testing purposes.
		/// </summary>
		/// <param name="dataSource">
		/// A data source value to use in the mock configuration.
		/// Default: "Northwind".
		/// </param>
		/// <returns>
		/// An <see cref="ApplicationConfiguration"/> instance that can be used in unit tests.
		/// </returns>
		public static ApplicationConfiguration GetApplicationConfiguration(string dataSource = "Northwind")
		{
			return new ApplicationConfiguration
			{
				DataSources = new List<DataSourceConfiguration>
				{
					new DataSourceConfiguration
					{
						Active = true,
						ConnectTimeout = 30,
						DataSource = dataSource,
						DataSourceType = "SqlServer",
						Host = "localhost",
						Password = "password",
						Port = 1433,
						Schema = "dbo",
						UserName = "username"
					}
				},
				OpenApi = new OpenApiConfiguration()
			};
		}

		/// <summary>
		/// A mock logger for testing purposes.
		/// </summary>
		/// <returns>
		/// A <see cref="MockLogger"/> instance that can be used in unit tests.
		/// </returns>
		public static MockLogger GetMockLogger()
		{
			return new MockLogger();
		}

		/// <summary>
		/// A mock logger factory for testing purposes.
		/// </summary>
		/// <returns>
		/// A <see cref="MockLoggerFactory"/> instance that can be used in unit tests.
		/// </returns>
		public static MockLoggerFactory GetMockLoggerFactory()
		{
			return new MockLoggerFactory();
		}
	}
}
