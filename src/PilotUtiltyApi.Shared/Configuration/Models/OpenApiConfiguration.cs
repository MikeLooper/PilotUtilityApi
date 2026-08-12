using System;

namespace PilotUtilityApi.Shared.Configuration.Models
{
	/// <summary>
	/// Configuration for OpenAPI.
	/// </summary>
	public class OpenApiConfiguration
	{
		/// <summary>
		/// Instatiate a <see cref="OpenApiConfiguration"/> object.
		/// </summary>
		public OpenApiConfiguration()
		{
		}

		/// <summary>
		/// Instantiate a <see cref="OpenApiConfiguration"/> object.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// A source configuration object to copy values from.
		/// </param>
		/// <param name="suppressSensitiveValues">
		/// A flag that indicates whether sensitive values should be suppressed when copying values from the source configuration.
		/// </param>
		public OpenApiConfiguration(
			OpenApiConfiguration sourceConfiguration,
			bool suppressSensitiveValues = false)
			: this()
		{
			this.Initialize(sourceConfiguration, suppressSensitiveValues);
		}

		/// <summary>
		/// Gets or sets the contact information.
		/// </summary>
		public OpenApiContactConfiguration Contact { get; set; } = new OpenApiContactConfiguration();

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the license.
		/// </summary>
		public string License { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the summary.
		/// </summary>
		public string Summary { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		public string Version { get; set; } = string.Empty;

		/// <inheritdoc/>>
		public override string ToString()
		{
			return $"{nameof(this.Contact)}={this.Contact}, " +
				$"{nameof(this.Description)}={this.Description}, " +
				$"{nameof(this.License)}={this.License}, " +
				$"{nameof(this.Summary)}={this.Summary}, " +
				$"{nameof(this.Title)}={this.Title}, " +
				$"{nameof(this.Version)}={this.Version}";
		}

		/// <summary>
		/// Initialize the current object with values from the source configuration.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// The source <see cref="OpenApiConfiguration"/> to copy values from.
		/// </param>
		/// <param name="suppressSensitiveValues">
		/// A flag that indicates whether sensitive values should be suppressed when copying values from the source configuration.
		/// </param>
		protected void Initialize(
			OpenApiConfiguration sourceConfiguration,
			bool suppressSensitiveValues = false)
		{
			if (sourceConfiguration == null)
			{
				throw new ArgumentException($"Invalid argument: {nameof(sourceConfiguration)}");
			}

			this.Contact = sourceConfiguration.Contact;
			this.Description = sourceConfiguration.Description;
			this.License = sourceConfiguration.License;
			this.Summary = sourceConfiguration.Summary;
			this.Title = sourceConfiguration.Title;
			this.Version = sourceConfiguration.Version;
		}
	}
}
