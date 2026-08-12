using PilotUtilityApi.Shared.Configuration.Models;
using System.Collections.Generic;

namespace PilotUtilityApi.Shared.Configuration
{
	/// <summary>
	/// Configuration interface for the application.
	/// </summary>
	public interface IApplicationConfiguration
	{
		/// <summary>
		/// Gets the data sources configuration.
		/// </summary>
		List<DataSourceConfiguration> DataSources { get; }

		/// <summary>
		/// Gets the OpenApi configuration.
		/// </summary>
		OpenApiConfiguration OpenApi { get; }

		/// <summary>
		/// Validates the configuration.
		/// </summary>
		void Validate();
	}
}
