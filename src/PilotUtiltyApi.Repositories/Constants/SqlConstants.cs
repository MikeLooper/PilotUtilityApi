namespace PilotUtilityApi.Repositories.Constants
{
	/// <summary>
	/// SQL constants for database operations.
	/// </summary>
	public static class SqlConstants
	{
		/// <summary>
		/// SQL Server script to reset testing data.
		/// Returns the count of rows deleted.
		/// </summary>
		public const string SqlServerResetTestingScript = @"
DECLARE @DeletedCount INT = 0;

-- Categories: Remove test row
IF EXISTS (SELECT 1 FROM Categories WHERE categoryName = 'Test Cat' AND description = 'Test Category')
BEGIN
	DELETE FROM Categories WHERE categoryName = 'Test Cat' AND description = 'Test Category';
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- Customers: Remove test row
IF EXISTS (SELECT 1 FROM Customers WHERE city = 'Test City' AND companyname = 'Test Company')
BEGIN
	DELETE FROM Customers WHERE city = 'Test City' AND companyname = 'Test Company';
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- Employees: Remove test row
IF EXISTS (SELECT 1 FROM Employees WHERE firstName = 'Test First' AND lastName = 'Test Last')
BEGIN
	DELETE FROM Employees WHERE firstName = 'Test First' AND lastName = 'Test Last';
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- OrderDetails: Remove test row
IF EXISTS (SELECT 1 FROM [Order Details] WHERE orderid = 10248 AND productid = 12 AND unitprice = 99.0000)
BEGIN
	DELETE FROM [Order Details] WHERE orderid = 10248 AND productid = 12 AND unitprice = 99.0000;
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- Orders: Remove test row
IF EXISTS (SELECT 1 FROM Orders WHERE shipcity = 'Test City' AND shipName = 'Test Name')
BEGIN
	DELETE FROM Orders WHERE shipcity = 'Test City' AND shipName = 'Test Name';
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- Products: Remove test row
IF EXISTS (SELECT 1 FROM Products WHERE productname = 'Test Product' AND quantityperunit = 'lots')
BEGIN
	DELETE FROM Products WHERE productname = 'Test Product' AND quantityperunit = 'lots';
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- Shippers: Remove test row
IF EXISTS (SELECT 1 FROM Shippers WHERE companyname = 'Test Shipper' AND phone = '(503) 555-9831')
BEGIN
	DELETE FROM Shippers WHERE companyname = 'Test Shipper' AND phone = '(503) 555-9831';
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- Suppliers: Remove test row
IF EXISTS (SELECT 1 FROM Suppliers WHERE companyname = 'Test Company' AND contacttitle = 'Test Title')
BEGIN
	DELETE FROM Suppliers WHERE companyname = 'Test Company' AND contacttitle = 'Test Title';
	SET @DeletedCount = @DeletedCount + @@ROWCOUNT;
END

-- Return the count of deleted rows
SELECT @DeletedCount AS DeletedCount;
";

		/// <summary>
		/// PostgreSQL script to reset testing data.
		/// Returns the count of rows deleted.
		/// </summary>
		public const string PostgreSqlResetTestingScript = @"
DO $$
DECLARE
	deleted_count INT := 0;
	temp_count INT;
BEGIN
	-- Categories: Remove test row
	DELETE FROM categories WHERE ""categoryname"" = 'Test Cat' AND description = 'Test Category';
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- Customers: Remove test row
	DELETE FROM customers WHERE city = 'Test City' AND ""companyname"" = 'Test Company';
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- Employees: Remove test row
	DELETE FROM employees WHERE ""firstname"" = 'Test First' AND ""lastname"" = 'Test Last';
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- OrderDetails: Remove test row
	DELETE FROM ""orderdetails"" WHERE ""orderid"" = 10248 AND ""productid"" = 12 AND ""unitprice"" = 99.0000::money;
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- Orders: Remove test row
	DELETE FROM orders WHERE ""shipcity"" = 'Test City' AND ""shipname"" = 'Test Name';
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- Products: Remove test row
	DELETE FROM products WHERE ""productname"" = 'Test Product' AND ""quantityperunit"" = 'lots';
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- Shippers: Remove test row
	DELETE FROM shippers WHERE companyname = 'Test Shipper' AND phone = '(503) 555-9831';
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- Suppliers: Remove test row
	DELETE FROM suppliers WHERE ""companyname"" = 'Test Company' AND ""contacttitle"" = 'Test Title';
	GET DIAGNOSTICS temp_count = ROW_COUNT;
	deleted_count := deleted_count + temp_count;

	-- Create a temporary table to return the result
	CREATE TEMP TABLE IF NOT EXISTS temp_result (DeletedCount INT);
	TRUNCATE temp_result;
	INSERT INTO temp_result (DeletedCount) VALUES (deleted_count);
END $$;

-- Return the count of deleted rows
SELECT DeletedCount FROM temp_result;
";
	}
}
