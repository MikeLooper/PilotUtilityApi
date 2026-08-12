namespace PilotUtilityApi.Shared.Configuration.Models
{
	/// <summary>
	/// Configuration for OpenAPI.
	/// </summary>
	public class OpenApiConfiguration
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		public string Title { get; set; } = string.Empty;

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
		/// Gets or sets the version.
		/// </summary>
		public string Version { get; set; } = string.Empty;
	}
}
