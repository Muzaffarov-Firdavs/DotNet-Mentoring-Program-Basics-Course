INSERT INTO Person (Id, FirstName, LastName) VALUES (1, 'John', 'Doe'), (2, 'Jane', 'Smith');
INSERT INTO Address (Id, Street, City, State, ZipCode) VALUES (1, '123 Main St', 'Anytown', 'CA', '12345'), (2, '456 Maple St', 'Othertown', 'TX', '67890');
INSERT INTO Company (Id, Name, AddressId) VALUES (1, 'TechCorp', 1), (2, 'HealthInc', 2);
INSERT INTO Employee (Id, AddressId, PersonId, CompanyName, Position, EmployeeName) VALUES (1, 1, 1, 'TechCorp', 'Developer', 'John Doe'), (2, 2, 2, 'HealthInc', 'Nurse', NULL);
