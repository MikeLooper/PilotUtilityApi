using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PilotUtilityApi.Shared.Configuration;
using System;

namespace PilotUtilityApi.Services.Extensions
{
	/// <summary>
	/// Extension methods for the services layer.
	/// </summary>
	public static class ServicesInjectionExtensions
	{
		/// <summary>
		/// Register configuration injection objects.
		/// </summary>
		/// <param name="builder">
		/// A <see cref="WebApplicationBuilder"/> object.
		/// </param>
		/// <example>
		/// Example usage:
		/// <code>
		/// // app: create
		/// var webAppBuilder = WebApplication.CreateBuilder(args);
		/// 
		/// // custom: setup
		/// webAppBuilder.Services.ServicesConfiguration();
		/// </code>
		/// </example>
		public static void ServicesConfiguration(this WebApplicationBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentException($"Invalid argument : {nameof(builder)}. "
					+ $"A valid object type of: '{typeof(WebApplicationBuilder)}' is needed to continue. ({nameof(ServicesInjectionExtensions)})");
			}

			// read configuration
			var applicationSettings = new ApplicationConfiguration();
			builder.Configuration.GetSection("Application").Bind(applicationSettings);
			applicationSettings.Validate();

			builder.Services.AddSingleton<IApplicationConfiguration>(applicationSettings);
		}

		/// <summary>
		/// Register injection objects.
		/// </summary>
		/// <param name="builder">
		/// A <see cref="WebApplicationBuilder"/> object.
		/// </param>
		/// <example>
		/// Example usage:
		/// <code>
		/// // app: create
		/// var webAppBuilder = WebApplication.CreateBuilder(args);
		/// 
		/// // custom: setup
		/// webAppBuilder.Services.ServicesRegistration();
		/// </code>
		/// </example>
		public static void ServicesRegistration(this WebApplicationBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentException($"Invalid argument : {nameof(builder)}. "
					+ $"A valid object type of: '{typeof(WebApplicationBuilder)}' is needed to continue. ({nameof(ServicesInjectionExtensions)})");
			}

			// register services
			builder.Services.AddTransient<Repositories.Repositories.ITestingRepository, Repositories.Repositories.TestingRepository>();

			builder.Services.AddTransient<Services.ITestingService, Services.TestingService>();
		}
	}
}
