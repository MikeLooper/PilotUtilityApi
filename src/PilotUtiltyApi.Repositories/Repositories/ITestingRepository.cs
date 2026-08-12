using PilotUtilityApi.Domain.Models.Responses;
using System.Threading.Tasks;

namespace PilotUtilityApi.Repositories.Repositories
{
	/// <summary>
	/// Repository interface for testing management operations.
	/// </summary>
	public interface ITestingRepository
	{
		/// <summary>
		/// Resets testing data in the database by removing test records.
		/// </summary>
		/// <returns>
		/// A <see cref="RetrieveResponse{TReturn}"/> containing the count of deleted rows,
		/// or an error message if the operation fails.
		/// </returns>
		Task<RetrieveResponse<int>> ResetTestingAsync();
	}
}
