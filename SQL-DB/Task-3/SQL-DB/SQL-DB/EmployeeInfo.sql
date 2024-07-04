CREATE VIEW EmployeeInfo AS
SELECT 
    e.Id AS EmployeeId,
    COALESCE(e.EmployeeName, p.FirstName + ' ' + p.LastName) AS EmployeeFullName,
    a.ZipCode + '_' + a.State + ', ' + a.City + '-' + a.Street AS EmployeeFullAddress,
    e.CompanyName + '(' + e.Position + ')' AS EmployeeCompanyInfo
FROM 
    Employee e
JOIN 
    Person p ON e.PersonId = p.Id
JOIN 
    Address a ON e.AddressId = a.Id;
