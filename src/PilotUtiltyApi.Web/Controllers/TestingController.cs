using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PilotUtilityApi.Services.Services;
using System;
using System.Threading.Tasks;

namespace PilotUtilityApi.Web.Controllers
{
	/// <summary>
	/// A controller for testing management operations.
	/// </summary>
	[ApiVersionNeutral]
	[AllowAnonymous]
	[ApiController]
	public class TestingController : ControllerBase
	{
		private readonly ITestingService testingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="TestingController"/> class.
		/// </summary>
		/// <param name="testingService">
		/// The testing service.
		/// </param>
		public TestingController(ITestingService testingService)
		{
			this.testingService = testingService ?? throw new ArgumentNullException(nameof(testingService));
		}

		/// <summary>
		/// Resets testing data in the database by removing test records.
		/// </summary>
		/// <returns>
		/// An <see cref="IActionResult"/> containing:
		/// - 200 OK with the count of deleted rows if successful and rows were deleted.
		/// - 204 NoContent if successful but no rows were deleted.
		/// - 400 BadRequest with a Warning header if an error occurred.
		/// </returns>
		[HttpPost]
		[Route("testing/reset")]
		[ProducesResponseType<int>(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> ResetTesting()
		{
			var response = await testingService.ResetTestingAsync();

			if (response.IsError)
			{
				Response.Headers.Append("Warning", response.ErrorMessage ?? "An error occurred");
				return BadRequest();
			}

			if (response.Result == null || response.Result == 0)
			{
				return NoContent();
			}

			return Ok(response.Result);
		}
	}
}
