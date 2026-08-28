using Newtonsoft.Json;
using PilotUtilityApi.Shared.Exceptions;
using System;
using System.Collections.Generic;

namespace PilotUtilityApi.Shared.Configuration.Models
{
	/// <inheritdoc/>
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class OpenTelemetryConfiguration	
	{
		/// <summary>
		/// Instatiate a <see cref="OpenTelemetryConfiguration"/> object.
		/// </summary>
		public OpenTelemetryConfiguration()
		{
		}

		/// <summary>
		/// Instantiate a <see cref="OpenTelemetryConfiguration"/> object.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// A source configuration object to copy values from.
		/// </param>
		public OpenTelemetryConfiguration(OpenTelemetryConfiguration sourceConfiguration)
			: this()
		{
			this.Initialize(sourceConfiguration);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this data source is active.
		/// </summary>
		public bool Active { get; set; }

		/// <inheritdoc/>
		[JsonProperty]
		public int? Port { get; set; }

		/// <inheritdoc/>
		[JsonProperty]
		public string? Server { get; set; }

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"{nameof(this.Active)}={this.Active}, " +
				$"{nameof(this.Server)}={this.Server}, " +
				$"{nameof(this.Port)}={this.Port}";

		}

		/// <inheritdoc/>
		public void Validate()
		{
			if (string.IsNullOrWhiteSpace(this.Server))
			{
				throw new ConfigurationException($"The {nameof(this.Server)} value is required and cannot be null or empty ({this.GetType().Name})");
			}

			if (this.Port <= 0)
			{
				throw new ConfigurationException($"The {nameof(this.Port)} value is required and must be greater than zero ({this.GetType().Name})");
			}
		}

		/// <summary>
		/// Initialize the current object with values from the source configuration.
		/// </summary>
		/// <param name="sourceConfiguration">
		/// The source <see cref="OpenTelemetryConfiguration"/> to copy values from.
		/// </param>
		protected void Initialize(OpenTelemetryConfiguration sourceConfiguration)
		{
			if (sourceConfiguration == null)
			{
				throw new ArgumentException($"Invalid argument: {nameof(sourceConfiguration)}");
			}

			this.Active = sourceConfiguration.Active;
			this.Server = sourceConfiguration.Server;
			this.Port = sourceConfiguration.Port;
		}
	}
}
