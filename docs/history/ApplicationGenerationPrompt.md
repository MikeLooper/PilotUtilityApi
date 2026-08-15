You are a Senior .NET/C# Software Engineer. Generating a production-ready REST API.

What is provided:
1. A basic structure for a C# API; including the following:
	A. Code projects:
		- PilotUtilityApi.Domain:
			-- Data Transfer Object (DTO) models and related domain code.
		- PilotUtilityApi.Repositories:
			-- Database Entity models and database communication repositories.
			-- This project should never be directly consumed by the PilotUtilityApi.Web project
		- PilotUtilityApi.Services:
			-- Service classes (which are called by a controller) that call the repository classes and exchange entity models for DTOs, which would then be returned to the calling controller.
		- PilotUtilityApi.Shared:
			-- Common code that could be used in any code project.  Content examples: configuration classes, utility classes, logging logic.
			-- May also contain interfaces used across the various code projects.
		- PilotUtilityApi.Web:
			-- The controllers and related web-related code.
			-- This project should be as minimal as possible - no more code than necessary to transfer data to the Services classes and then return a value from the Services classes to the endpoint response.
			-- This project should never contain any business logic.
	B. Unit test projects:
		- PilotUtilityApi.Domain.Tests:
			-- Unit testing of classes in the PilotUtilityApi.Domain project.
		- PilotUtilityApi.Repositories.Tests:
			-- Unit testing of classes in the PilotUtilityApi.Repositories project.
		- PilotUtilityApi.Services.Tests:
			-- Unit testing of classes in the PilotUtilityApi.Services project.
		- PilotUtilityApi.Shared.Tests:
			-- Unit testing of classes in the PilotUtilityApi.Shared project.
		- PilotUtilityApi.TestingShared:
			-- Code common to the other unit test projects, such as:
				- Shared testing doubles.
				- Shared testing utility classes.
		- PilotUtilityApi.Web.Tests:
			-- Unit testing of classes in the PilotUtilityApi.Web project.
	C. Configuration settings in the `appsettings.json` file.
		- Database connection attributes for both SQL Server and PostgreSQL are in the Application.DataSources array.
		- These attributes should be used for connecting-to and using the databases.
		- Which database should be used is defined by the 'Active' flag - there can only be one active in this array at one time.
	E. Enough code to successfully start the application, but with no controllers or endpoints.

Steps:
1. Using the `appsettings.json` file as a model, generate configuration class(es) as needed to ingest the file values.  The following details will apply to this step:
	- The configuration interface will be called: IApplicationConfiguration
	- The configuration class will be called: ApplicationConfiguration
	- The commented code in the PilotUtilityApi.Services.Extensions.ServicesInjectionExtensions.ServicesConfiguration method would be uncommented and used to inject the configuration object.
2. Create a database script to Reset Testing values.  This script will do the following:
	- Check the "Categories" table for a row where the "categoryName" column = "Test Cat" and the "description" column = "Test Category".  If found, remove that row.
	- Check the "Customers" table for a row where the "city" column = "Test City" and the "companyName" column = "Test Company".  If found, remove that row.
	- Check the "Employees" table for a row where the "firstName" column = "Test First" and the "lastName" column = "Test Last".  If found, remove that row.
	- Check the "OrderDetails" table for a row where the "orderID" column = 10248 and the "productID" column = 12 and the "unitPrice" column = 99.0000.  If found, remove that row.
	- Check the "Orders" table for a row where the "shipCity" column = "Test City" and the "shipName" column = "Test Name".  If found, remove that row.
	- Check the "Products" table for a row where the "productName" column = "Test Product" and the "quantityPerUnit" column = "lots".  If found, remove that row.
	- Check the "Shippers" table for a row where the "companyname" column = "Test Shipper" and the "phone" column = "(503) 555-9831".  If found, remove that row.
	- Check the "Suppliers" table for a row where the "companyName" column = "Test Company" and the "contactTitle" column = "Test Title".  If found, remove that row.
	- After completing all of the prior database commands, return a count of how many of those commands deleted a row.
	- Implement provider-specific reset SQL for SQL Server and PostgreSQL.  This will require two copies of the database script - one supporting each database:
		- Whichever database is currently active (based on the Application.DataSources item with an Active setting of true) would determine which script would be used.
3. Place the Reset Testing database script, created in the last step, into a constants class: `PilotUtilityApi.Repositories/Constants/SqlConstants.cs`.
4. Create a Testing Management repository class (and related interface) which will contain a Reset Testing method.  This method will call the database, using Dapper, and execute the Reset Testing script.  All methods will be `Async`. The response of this method will use the RetrieveResponse class for the following:
	- If the database call succeeds: Return the number of rows deleted (from the database script).
	- If the database call has an error: Return a zero and the error message extracted from the error occurrance.
	- If the database call throws an exception, Do the following:
		- Catch the database exception.
		- Create a correlationid, from the PilotUtilityApi.Shared.Logging.LoggingUtilities.GetLoggingCorrelation method.
		- Log details about the current location, the correlationid, and the exception details.
		- Throw a new UserException, with the following in the exception message:
			- A message that an error occurred.
			- The correlationid (which was logged, for troubleshooting).
5. Create a Testing Management service class (and related interface) which will contain a Reset Testing method.  All methods will be `Async`. This method will do the following:
	- Call the Testing Management repository (Reset Testing method).
	- When a response is ready, pass the result of the repository call back to the controller that called the service class.
6. The Testing Management repository and service class must be registered for injection in the PilotUtilityApi.Services.Extensions.ServicesInjectionExtensions.ServicesRegistration method.  The commented code in this method demonstrates how this can be done.
7. Create a Testing controller, which will contain a Reset Testing endpoint.  All methods will be `Async`. This endpoint will do the following:
	- Route example: `POST /testing/reset`.
	- Call the Testing Management service class (Reset Testing method).
	- If the result of that call does NOT include an error (i.e. RetrieveResponse.IsError = false), and has a valid value: return an OK (status code 200) containing the value from the service class.
	- If the result of that call does NOT include an error (i.e. RetrieveResponse.IsError = false), but has a null value: return a NotFound (status code 204) .
	- If the result of that call DOES include an error (i.e. RetrieveResponse.IsError = true), do the following:
		- Add a Warning header to the response, containing the error message.
		- Return a BadRequest (status code 400) .
8. Create or update NUnit tests for all newly added or modified classes and all changed behavior paths, following `.github/instructions/unit-tests.instructions.md`.  Verify that the unit tests all execute successfully.
9. Update the README to include the following:
	- A description of this application.
	- Instructions on how to build and execute this application.
	- A source tree display of the structure of this application.
	- A description of the settings in the configuration, along with default values.
	- Instructions on how to switch between the two databases.

Specifications:
- Target databases: SQL Server and PostgreSQL
- Architectural guidance is located in the architecture instructions file.
- Unit testing guidance is located in the unit test instructions file.

Constraints:
- Direct requirement to follow:
  - `.github/instructions/architecture.instructions.md`
  - `.github/instructions/unit-tests.instructions.md`

