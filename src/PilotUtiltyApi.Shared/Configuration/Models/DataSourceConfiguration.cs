using System;

namespace PilotUtilityApi.Shared.Configuration.Models
{
	/// <summary>
	/// Configuration for a data source.
	/// </summary>
	public class DataSourceConfiguration
	{
		/// <summary>
		/// Instatiate a <see cref="DataSourceConfiguration"/> object.
		/// </summary>
		public DataSourceConfiguration()
		{
		}

		/// <summary>
		/// Instantiate a <see cref="DataSourceConfiguration"/> object.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// A source configuration object to copy values from.
		/// </param>
		/// <param name="suppressSensitiveValues">
		/// A flag that indicates whether sensitive values should be suppressed when copying values from the source configuration.
		/// </param>
		public DataSourceConfiguration(
			DataSourceConfiguration sourceConfiguration,
			bool suppressSensitiveValues = false)
			: this()
		{
			this.Initialize(sourceConfiguration, suppressSensitiveValues);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this data source is active.
		/// </summary>
		public bool Active { get; set; }

		/// <summary>
		/// Gets or sets the connect timeout in seconds.
		/// </summary>
		public int ConnectTimeout { get; set; }

		/// <summary>
		/// Gets or sets the data source (database name).
		/// </summary>
		public string DataSource { get; set; }

		/// <summary>
		/// Gets or sets the data source type (e.g., SqlServer, PostgreSQL).
		/// </summary>
		public string DataSourceType { get; set; }

		/// <summary>
		/// Gets or sets the host.
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		/// Gets or sets the schema.
		/// </summary>
		public string Schema { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		public string UserName { get; set; }

		/// <inheritdoc/>>
		public override string ToString()
		{
			return $"{nameof(this.Active)}={this.Active}, " +
				$"{nameof(this.ConnectTimeout)}={this.ConnectTimeout}, " +
				$"{nameof(this.DataSource)}={this.DataSource}, " +
				$"{nameof(this.DataSourceType)}={this.DataSourceType}, " +
				$"{nameof(this.Host)}={this.Host}, " +
				$"{nameof(this.Password)}={(string.IsNullOrEmpty(this.Password) ? "--Empty--" : "[Redacted]")}, " +
				$"{nameof(this.Port)}={this.Port}, " +
				$"{nameof(this.Schema)}={this.Schema}, " +
				$"{nameof(this.UserName)}={this.UserName}";
		}

		/// <summary>
		/// Initialize the current object with values from the source configuration.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// The source <see cref="DataSourceConfiguration"/> to copy values from.
		/// </param>
		/// <param name="suppressSensitiveValues">
		/// A flag that indicates whether sensitive values should be suppressed when copying values from the source configuration.
		/// </param>
		protected void Initialize(
			DataSourceConfiguration sourceConfiguration,
			bool suppressSensitiveValues = false)
		{
			if (sourceConfiguration == null)
			{
				throw new ArgumentException($"Invalid argument: {nameof(sourceConfiguration)}");
			}

			this.Active = sourceConfiguration.Active;
			this.ConnectTimeout = sourceConfiguration.ConnectTimeout;
			this.DataSource = sourceConfiguration.DataSource;
			this.DataSourceType = sourceConfiguration.DataSourceType;
			this.Host = sourceConfiguration.Host;
			this.Password = suppressSensitiveValues? "[Redacted]" : sourceConfiguration.Password;
			this.Port = sourceConfiguration.Port;
			this.Schema = sourceConfiguration.Schema;
			this.UserName = sourceConfiguration.UserName;
		}
	}
}
