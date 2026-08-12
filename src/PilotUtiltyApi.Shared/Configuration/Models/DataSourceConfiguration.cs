namespace PilotUtilityApi.Shared.Configuration.Models
{
	/// <summary>
	/// Configuration for a data source.
	/// </summary>
	public class DataSourceConfiguration
	{
		/// <summary>
		/// Gets or sets a value indicating whether this data source is active.
		/// </summary>
		public bool Active { get; set; }

		/// <summary>
		/// Gets or sets the connect timeout in seconds.
		/// </summary>
		public int ConnectTimeout { get; set; }

		/// <summary>
		/// Gets or sets the data source name.
		/// </summary>
		public string DataSourceName { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the data source (database name).
		/// </summary>
		public string DataSource { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the data source type (e.g., SqlServer, PostgreSQL).
		/// </summary>
		public string DataSourceType { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the host.
		/// </summary>
		public string Host { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the schema.
		/// </summary>
		public string Schema { get; set; } = string.Empty;
	}
}
