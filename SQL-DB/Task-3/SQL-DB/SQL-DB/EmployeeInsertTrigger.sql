CREATE TRIGGER EmployeeInsertTrigger
ON Employee
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewEmployeeId INT, @AddressId INT;

    SELECT @NewEmployeeId = Id, @AddressId = AddressId FROM inserted;

    INSERT INTO Company (Name, AddressId)
    SELECT CompanyName, @AddressId FROM Employee WHERE Id = @NewEmployeeId;
END
