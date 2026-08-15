using System;

namespace PilotUtilityApi.Shared.Exceptions
{
	/// <summary>
	/// A custom exception for working with configurations.
	/// </summary>
	[Serializable]
	public class ConfigurationException : Exception
	{
		/// <summary>
		/// Instantiate a <see cref="ConfigurationException"/> object.
		/// </summary>
		public ConfigurationException()
		{
		}

		/// <summary>
		/// Instantiate a <see cref="ConfigurationException"/> object.
		/// </summary>
		/// <param name="message">
		/// A message to include in the exception.
		/// </param>
		public ConfigurationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Instantiate a <see cref="ConfigurationException"/> object.
		/// </summary>
		/// <param name="message">
		/// A message to include in the exception.
		/// </param>
		/// <param name="innerException">
		/// An inner exception to include in the exception.
		/// </param>
		public ConfigurationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
