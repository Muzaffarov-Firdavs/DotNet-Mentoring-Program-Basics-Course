CREATE PROCEDURE InsertEmployee
    @EmployeeName NVARCHAR(100) = NULL,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @CompanyName NVARCHAR(20),
    @Position NVARCHAR(30) = NULL,
    @Street NVARCHAR(50),
    @City NVARCHAR(20) = NULL,
    @State NVARCHAR(50) = NULL,
    @ZipCode NVARCHAR(50) = NULL
AS
BEGIN
    IF @EmployeeName IS NULL AND (@FirstName IS NULL OR LTRIM(RTRIM(@FirstName)) = '') AND (@LastName IS NULL OR LTRIM(RTRIM(@LastName)) = '')
    BEGIN
        THROW 50000, 'At least one of EmployeeName, FirstName, or LastName must be provided and not be an empty string.', 1;
    END

    SET @CompanyName = LEFT(@CompanyName, 20);

    DECLARE @AddressId INT, @PersonId INT;

    INSERT INTO Address (Street, City, State, ZipCode)
    VALUES (@Street, @City, @State, @ZipCode);

    SET @AddressId = SCOPE_IDENTITY();

    INSERT INTO Person (FirstName, LastName)
    VALUES (@FirstName, @LastName);

    SET @PersonId = SCOPE_IDENTITY();

    INSERT INTO Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
    VALUES (@AddressId, @PersonId, @CompanyName, @Position, @EmployeeName);
END
