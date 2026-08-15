using NUnit.Framework;

/// <summary>
/// A test base class.
/// </summary>
/// <remarks>
/// Execution Order Lifecycle:
/// - [OneTimeSetUp]: Base class runs first, then the derived class.
/// - [SetUp]: Base class runs first, then the derived class.
/// - [Test]: The actual test method executes.
/// - [TearDown]: Derived class runs first, then the base class.
/// - [OneTimeTearDown]: Derived class runs first, then the base class
///	</remarks>
public abstract class TestBase
{
	//protected string SharedTestResource;

	//// Runs ONCE before any tests in the derived class start
	//[OneTimeSetUp]
	//public void InitialSuiteSetup()
	//{
	//	// Allocate heavy resources like database connections or global configuration
	//}

	//// Runs BEFORE every individual test case
	//[SetUp]
	//public void Setup()
	//{
	//	SharedTestResource = "Initialized Data";
	//	// Initialize mock objects or clear test environments here
	//}

	//// Runs AFTER every individual test case
	//[TearDown]
	//public void Teardown()
	//{
	//	// Reset state or clean up test files
	//}

	//// Runs ONCE after all tests in the derived class finish
	//[OneTimeTearDown]
	//public void FinalSuiteCleanup()
	//{
	//	// Dispose heavy resources
	//}

	//// Standard IDisposable pattern for managed/unmanaged resources
	//public void Dispose()
	//{
	//	// Optional fallback cleanup
	//}
}
