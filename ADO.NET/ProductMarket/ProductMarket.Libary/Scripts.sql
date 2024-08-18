-- Create Product Table
CREATE TABLE Products
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Weight DECIMAL(18, 2),
    Height DECIMAL(18, 2),
    Width DECIMAL(18, 2),
    Length DECIMAL(18, 2)
);

-- Create Order Table
CREATE TABLE [Orders]
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Status NVARCHAR(50) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    UpdatedDate DATETIME NOT NULL,
    ProductId INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Product(Id)
);

-- Create stored proc for orders
CREATE PROCEDURE GetFilteredOrders
    @Year INT = NULL,
    @Month INT = NULL,
    @Status NVARCHAR(50) = NULL,
    @ProductId INT = NULL
AS
BEGIN
    SELECT * FROM Orders
    WHERE
        (@Year IS NULL OR YEAR(CreatedDate) = @Year) AND
        (@Month IS NULL OR MONTH(CreatedDate) = @Month) AND
        (@Status IS NULL OR Status = @Status) AND
        (@ProductId IS NULL OR ProductId = @ProductId)
END

