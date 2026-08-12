using PilotUtilityApi.Domain.Models.Responses;
using PilotUtilityApi.Repositories.Repositories;
using System;
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
		/// <param name="testingRepository">
		/// The testing repository.
		/// </param>
		public TestingService(ITestingRepository testingRepository)
		{
			this.testingRepository = testingRepository ?? throw new ArgumentNullException(nameof(testingRepository));
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
			return await testingRepository.ResetTestingAsync();
		}
	}
}
