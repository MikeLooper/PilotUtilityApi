using Microsoft.AspNetCore.Builder;
using System;

namespace PilotUtilityApi.Shared.Swagger.Extensions
{
	/// <summary>
	/// Extension methods for Swagger.
	/// </summary>
	public static class SwaggerExtensions
	{
		/// <summary>
		/// Swagger usage for the application.
		/// </summary>
		/// <param name="webApp">
		/// A web application object.
		/// </param>
		/// <example>
		/// Example usage:
		/// <code>
		/// // app: create
		/// var webAppBuilder = WebApplication.CreateBuilder(args);
		/// 
		/// // app: build
		/// var webApp = webAppBuilder.Build();
		/// 
		/// // shared: setups
		/// webApp.UseLogging();
		/// </code>
		/// </example>
		public static void SwaggerWebApplication(this WebApplication webApp)
		{
			if (webApp == null)
			{
				throw new ArgumentException($"Invalid argument : {nameof(webApp)}. "
					+ $"A valid object type of: '{typeof(WebApplication)}' is needed to continue. ({nameof(SwaggerExtensions)})");
			}

			webApp.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/openapi/v1.json", "v1");
			});
		}
	}
}
