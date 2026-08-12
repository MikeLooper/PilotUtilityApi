using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using PilotUtilityApi.Domain.Models.Responses;
using PilotUtilityApi.Repositories.Constants;
using PilotUtilityApi.Shared.Configuration;
using PilotUtilityApi.Shared.Exceptions;
using PilotUtilityApi.Shared.Logging;
using Serilog;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PilotUtilityApi.Repositories.Repositories
{
	/// <summary>
	/// Repository for testing management operations.
	/// </summary>
	public class TestingRepository : ITestingRepository
	{
		private readonly IApplicationConfiguration applicationConfiguration;

		/// <summary>
		/// Initializes a new instance of the <see cref="TestingRepository"/> class.
		/// </summary>
		/// <param name="applicationConfiguration">
		/// The application configuration.
		/// </param>
		public TestingRepository(IApplicationConfiguration applicationConfiguration)
		{
			this.applicationConfiguration = applicationConfiguration ?? throw new ArgumentNullException(nameof(applicationConfiguration));
		}

		/// <summary>
		/// Resets testing data in the database by removing test records.
		/// </summary>
		/// <returns>
		/// A <see cref="RetrieveResponse{TReturn}"/> containing the count of deleted rows,
		/// or an error message if the operation fails.
		/// </returns>
		public async Task<RetrieveResponse<int>> ResetTestingAsync()
		{
			try
			{
				var activeDataSource = applicationConfiguration.DataSources.FirstOrDefault(ds => ds.Active);

				if (activeDataSource == null)
				{
					return new RetrieveResponse<int>(0, "No active data source configured.");
				}

				string connectionString = BuildConnectionString(activeDataSource);
				string sqlScript = GetResetScript(activeDataSource.DataSourceType);

				using IDbConnection connection = CreateConnection(activeDataSource.DataSourceType, connectionString);

				var result = await connection.QueryFirstOrDefaultAsync<int>(sqlScript);

				return new RetrieveResponse<int>(result);
			}
			catch (Exception ex)
			{
				var correlationId = LoggingUtilities.GetLoggingCorrelation();

				Log.Error(ex,
					"Error occurred in {ClassName}.{MethodName}. CorrelationId: {CorrelationId}",
					nameof(TestingRepository),
					nameof(ResetTestingAsync),
					correlationId.CorrelationId);

				throw new UserException(correlationId.UserMessage);
			}
		}

		/// <summary>
		/// Builds a connection string for the specified data source.
		/// </summary>
		/// <param name="dataSource">
		/// The data source configuration.
		/// </param>
		/// <returns>
		/// A connection string.
		/// </returns>
		private static string BuildConnectionString(Shared.Configuration.Models.DataSourceConfiguration dataSource)
		{
			return dataSource.DataSourceType.ToLowerInvariant() switch
			{
				"sqlserver" => $"Server={dataSource.Host},{dataSource.Port};" +
							   $"Database={dataSource.DataSource};" +
							   $"User Id={dataSource.UserName};" +
							   $"Password={dataSource.Password};" +
							   $"Connect Timeout={dataSource.ConnectTimeout};" +
							   "TrustServerCertificate=True;",

				"postgresql" => $"Host={dataSource.Host};" +
								$"Port={dataSource.Port};" +
								$"Database={dataSource.DataSource};" +
								$"Username={dataSource.UserName};" +
								$"Password={dataSource.Password};" +
								$"Timeout={dataSource.ConnectTimeout};" +
								$"Search Path={dataSource.Schema};",

				_ => throw new ConfigurationException($"Unsupported data source type: {dataSource.DataSourceType}")
			};
		}

		/// <summary>
		/// Creates a database connection for the specified data source type.
		/// </summary>
		/// <param name="dataSourceType">
		/// The type of data source (e.g., SqlServer, PostgreSQL).
		/// </param>
		/// <param name="connectionString">
		/// The connection string.
		/// </param>
		/// <returns>
		/// A database connection.
		/// </returns>
		private static IDbConnection CreateConnection(string dataSourceType, string connectionString)
		{
			return dataSourceType.ToLowerInvariant() switch
			{
				"sqlserver" => new SqlConnection(connectionString),
				"postgresql" => new NpgsqlConnection(connectionString),
				_ => throw new ConfigurationException($"Unsupported data source type: {dataSourceType}")
			};
		}

		/// <summary>
		/// Gets the reset script for the specified data source type.
		/// </summary>
		/// <param name="dataSourceType">
		/// The type of data source (e.g., SqlServer, PostgreSQL).
		/// </param>
		/// <returns>
		/// The SQL reset script.
		/// </returns>
		private static string GetResetScript(string dataSourceType)
		{
			return dataSourceType.ToLowerInvariant() switch
			{
				"sqlserver" => SqlConstants.SqlServerResetTestingScript,
				"postgresql" => SqlConstants.PostgreSqlResetTestingScript,
				_ => throw new ConfigurationException($"Unsupported data source type: {dataSourceType}")
			};
		}
	}
}
