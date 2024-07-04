INSERT INTO Person (FirstName, LastName) 
VALUES 
('John', 'Doe'), 
('Jane', 'Smith');

INSERT INTO Address (Street, City, State, ZipCode) 
VALUES 
('123 Main St', 'Anytown', 'CA', '12345'), 
('456 Maple St', 'Othertown', 'TX', '67890');

INSERT INTO Company (Name, AddressId) 
VALUES 
('TechCorp', 1), 
('HealthInc', 2);

INSERT INTO Employee (AddressId, PersonId, CompanyName, Position, EmployeeName) 
VALUES 
(1, 1, 'TechCorp', 'Developer', 'John Doe'), 
(2, 2, 'HealthInc', 'Nurse', NULL);
