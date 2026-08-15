using System;
using System.Collections.Generic;
using System.Text;

namespace PilotUtilityApi.Shared.Configuration.Models
{
	/// <summary>
	/// Configuration for OpenAPI contact information.
	/// </summary>
	public class OpenApiContactConfiguration
	{
		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		public string Email { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		public string URL { get; set; } = string.Empty;
	}
}
