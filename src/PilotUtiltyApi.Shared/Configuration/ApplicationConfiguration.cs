using PilotUtilityApi.Shared.Configuration.Models;
using PilotUtilityApi.Shared.Exceptions;
using System.Linq;

namespace PilotUtilityApi.Shared.Configuration
{
	/// <summary>
	/// Configuration for the application.
	/// </summary>
	public class ApplicationConfiguration : IApplicationConfiguration
	{
		/// <summary>
		/// Gets or sets the data sources configuration.
		/// </summary>
		public DataSourceConfiguration[] DataSources { get; set; } = [];

		/// <summary>
		/// Gets or sets the OpenApi configuration.
		/// </summary>
		public OpenApiConfiguration OpenApi { get; set; } = new OpenApiConfiguration();

		/// <summary>
		/// Validates the configuration.
		/// </summary>
		/// <exception cref="ConfigurationException">
		/// Thrown when the configuration is invalid.
		/// </exception>
		public void Validate()
		{
			if (DataSources == null || DataSources.Length == 0)
			{
				throw new ConfigurationException("No data sources configured.");
			}

			var activeDataSources = DataSources.Where(ds => ds.Active).ToArray();

			if (activeDataSources.Length == 0)
			{
				throw new ConfigurationException("No active data source configured. At least one data source must be marked as Active.");
			}

			if (activeDataSources.Length > 1)
			{
				throw new ConfigurationException($"Multiple active data sources configured ({activeDataSources.Length}). Only one data source can be marked as Active.");
			}

			var activeDataSource = activeDataSources[0];

			if (string.IsNullOrWhiteSpace(activeDataSource.DataSourceType))
			{
				throw new ConfigurationException("Active data source must have a DataSourceType specified.");
			}

			if (string.IsNullOrWhiteSpace(activeDataSource.Host))
			{
				throw new ConfigurationException("Active data source must have a Host specified.");
			}

			if (string.IsNullOrWhiteSpace(activeDataSource.DataSource))
			{
				throw new ConfigurationException("Active data source must have a DataSource (database name) specified.");
			}
		}
	}
}
