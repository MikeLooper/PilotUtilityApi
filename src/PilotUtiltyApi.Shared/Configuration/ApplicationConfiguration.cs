using PilotUtilityApi.Shared.Configuration.Models;
using PilotUtilityApi.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PilotUtilityApi.Shared.Configuration
{
	/// <summary>
	/// Configuration for the application.
	/// </summary>
	public class ApplicationConfiguration : IApplicationConfiguration
	{
		/// <summary>
		/// Instatiate a <see cref="ApplicationConfiguration"/> object.
		/// </summary>
		public ApplicationConfiguration()
		{
			this.DataSources = new List<DataSourceConfiguration>();
			this.OpenApi = new OpenApiConfiguration();
		}

		/// <summary>
		/// Instantiate a <see cref="ApplicationConfiguration"/> object.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// A source configuration object to copy values from.
		/// </param>
		/// <param name="suppressSensitiveValues">
		/// A flag that indicates whether sensitive values should be suppressed when copying values from the source configuration.
		/// </param>
		public ApplicationConfiguration(
			IApplicationConfiguration sourceConfiguration,
			bool suppressSensitiveValues = false)
			: this()
		{
			this.Initialize(sourceConfiguration, suppressSensitiveValues);
		}

		/// <summary>
		/// Gets or sets the data sources configuration.
		/// </summary>
		public List<DataSourceConfiguration> DataSources { get; set; }

		/// <summary>
		/// Gets or sets the OpenApi configuration.
		/// </summary>
		public OpenApiConfiguration OpenApi { get; set; } = new OpenApiConfiguration();

		/// <inheritdoc/>>
		public override string ToString()
		{
			return $"{nameof(this.DataSources)}=[{this.DataSources}], " +
				$"{nameof(this.OpenApi)}=[{this.OpenApi}]";
		}

		/// <summary>
		/// Validates the configuration.
		/// </summary>
		/// <exception cref="ConfigurationException">
		/// Thrown when the configuration is invalid.
		/// </exception>
		public void Validate()
		{
			if (DataSources == null || DataSources.Count == 0)
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

		/// <summary>
		/// Initialize the current object with values from the source configuration.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// The source <see cref="ApplicationConfiguration"/> to copy values from.
		/// </param>
		/// <param name="suppressSensitiveValues">
		/// A flag that indicates whether sensitive values should be suppressed when copying values from the source configuration.
		/// </param>
		protected void Initialize(
			IApplicationConfiguration sourceConfiguration,
			bool suppressSensitiveValues = false)
		{
			if (sourceConfiguration == null)
			{
				throw new ArgumentException($"Invalid argument: {nameof(sourceConfiguration)}");
			}

#pragma warning disable CS8601 // Possible null reference assignment.
			this.DataSources = sourceConfiguration.DataSources
				?.Select(s => new DataSourceConfiguration(s, suppressSensitiveValues))
				.ToList();
#pragma warning restore CS8601 // Possible null reference assignment.

			this.OpenApi = new OpenApiConfiguration(sourceConfiguration.OpenApi, suppressSensitiveValues);
		}
	}
}
