using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PilotUtilityApi.Shared.OpenApi.Transformers
{
	/// <summary>
	/// Global OpenApi operations.
	/// </summary>
	public class GlobalOperationTransformer : IOpenApiOperationTransformer
	{
		/// <inheritdoc/>
		public async Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
		{
			// remove query parameter
			if (operation.Parameters != null)
			{
				var versionParameter = operation.Parameters
							.FirstOrDefault(p =>
										!string.IsNullOrWhiteSpace(p.Name) &&
										p.Name.Equals("api-version", StringComparison.OrdinalIgnoreCase));

				if (versionParameter != null)
				{
					operation.Parameters.Remove(versionParameter);
				}
			}
			return;
		}
	}
}
