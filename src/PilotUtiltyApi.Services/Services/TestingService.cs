using Microsoft.Extensions.Logging;
using PilotUtilityApi.Domain.Models.Responses;
using PilotUtilityApi.Repositories.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PilotUtilityApi.Services.Services
{
	/// <summary>
	/// Service for testing management operations.
	/// </summary>
	public class TestingService : ITestingService
	{
		private readonly ITestingRepository testingRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="TestingService"/> class.
		/// </summary>
		/// <param name="loggerFactory">
		/// A logger factory for creating loggers.
		/// </param>
		/// <param name="testingRepository">
		/// The testing repository.
		/// </param>
		public TestingService(
			ILoggerFactory loggerFactory,
			ITestingRepository testingRepository)
		{
			this.Logger = loggerFactory.CreateLogger(GetType());
			this.testingRepository = testingRepository ?? throw new ArgumentNullException(nameof(testingRepository));
		}

		/// <summary>
		/// Gets the logger for logging information and errors.
		/// </summary>
		protected ILogger Logger { get; }

		/// <summary>
		/// Resets testing data in the database by removing test records.
		/// </summary>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// A <see cref="RetrieveResponse{TReturn}"/> containing the count of deleted rows,
		/// or an error message if the operation fails.
		/// </returns>
		public async Task<RetrieveResponse<int>> ResetTestingAsync(CancellationToken cancellationToken = default)
		{
			return await testingRepository.ResetTestingAsync(cancellationToken);
		}
	}
}
