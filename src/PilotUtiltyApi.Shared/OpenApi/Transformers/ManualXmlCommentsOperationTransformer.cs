using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PilotUtilityApi.Shared.OpenApi.Transformers
{
	/// <summary>
	/// Custom Operation Transformer that manually parses the XML file 
	/// to bind endpoint summaries and descriptions.
	/// </summary>
	public class ManualXmlCommentsOperationTransformer : IOpenApiOperationTransformer
	{
		/// <summary>
		/// An XML document.
		/// </summary>
		protected readonly XDocument _xmlDoc;

		/// <summary>
		/// Instantiate a <see cref="ManualXmlCommentsOperationTransformer"/> object.
		/// </summary>
		/// <param name="xmlPath">
		/// The path of the assembly XML file.
		/// </param>
		public ManualXmlCommentsOperationTransformer(string xmlPath)
		{
			if (xmlPath == null)
			{
				throw new ArgumentNullException(nameof(xmlPath));
			}

			if (string.IsNullOrWhiteSpace(xmlPath))
			{
				throw new InvalidOperationException($"The {nameof(xmlPath)} argument cannot be empty ({nameof(ManualXmlCommentsOperationTransformer)})");
			}

			_xmlDoc = XDocument.Load(xmlPath);
		}

		/// <inheritdoc/>
		public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
		{
			// Safely pull the ActionDescriptor to access the underlying controller/minimal API method info
			MethodInfo? methodInfo = null;
			if (context.Description.ActionDescriptor is ControllerActionDescriptor cad)
			{
				methodInfo = cad.MethodInfo;
			}

			if (methodInfo == null)
			{
				return Task.CompletedTask;
			}

			// Generate the standard XML documentation member ID format for the method
			// Format: M:Namespace.ClassName.MethodName(ParamType1,ParamType2)
			string memberName = GetXmlMemberKey(methodInfo);

			// Query the loaded XML for the corresponding member tag
			var memberNode = _xmlDoc.Descendants("member")
				.FirstOrDefault(m => m.Attribute("name")?.Value == memberName);

			if (memberNode != null)
			{
				var summaryText = memberNode.Element("summary")?.Value?.Trim();
				var remarksText = memberNode.Element("remarks")?.Value?.Trim();

				if (!string.IsNullOrEmpty(summaryText))
				{
					operation.Summary = summaryText;
				}

				if (!string.IsNullOrEmpty(remarksText))
				{
					operation.Description = remarksText;
				}
			}

			return Task.CompletedTask;
		}

		/// <summary>
		/// Using the method info object, build the name of the current class method.
		/// </summary>
		/// <param name="methodInfo"></param>
		/// <returns></returns>
		protected static string GetXmlMemberKey(MethodInfo methodInfo)
		{
			var typeName = methodInfo.DeclaringType?.FullName?.Replace("+", ".");
			var methodName = methodInfo.Name;

			var parameters = methodInfo.GetParameters();
			if (parameters.Length == 0)
			{
				return $"M:{typeName}.{methodName}";
			}

			// Format parameter type names to perfectly match standard XML compiler outputs
			var paramStrings = parameters.Select(p => p.ParameterType.FullName ?? p.ParameterType.Name);
			return $"M:{typeName}.{methodName}({string.Join(",", paramStrings)})";
		}
	}
}
