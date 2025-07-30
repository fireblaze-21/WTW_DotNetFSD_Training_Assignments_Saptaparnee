-- Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName VARCHAR(100) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) CHECK (Price > 0) NOT NULL,
    StockQuantity INT DEFAULT 0 CHECK (StockQuantity >= 0)
);

-- Customers table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20) NULL
);

-- Orders table
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    OrderDate DATE DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) CHECK (TotalAmount >= 0) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- OrderDetails table
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT CHECK (Quantity > 0) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Insert sample data
INSERT INTO Products (ProductName, Category, Price, StockQuantity)
VALUES 
('Laptop', 'Computers', 999.99, 50),
('Headphones', 'Accessories', 49.99, 15);

INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber)
VALUES 
('Alice', 'Smith', 'alice@example.com', '123-456-7890'),
('Bob', 'Johnson', 'bob@example.com', '234-567-8901');

INSERT INTO Orders (CustomerID, TotalAmount)
VALUES 
(1, 1049.98),
(2, 49.99);

INSERT INTO OrderDetails (OrderID, ProductID, Quantity)
VALUES 
(1, 1, 1),
(1, 2, 1),
(2, 2, 1);

-- Query: Low stock (< 20)
SELECT 'Low Stock' AS Report, ProductID, ProductName, StockQuantity
FROM Products
WHERE StockQuantity < 20;

-- Query: Customer spending
SELECT 
    C.CustomerID,
    C.FirstName + ' ' + C.LastName AS FullName,
    SUM(O.TotalAmount) AS TotalSpent
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.CustomerID, C.FirstName, C.LastName;

-- Update stock after order
UPDATE P
SET P.StockQuantity = P.StockQuantity - OD.Quantity
FROM Products P
JOIN OrderDetails OD ON P.ProductID = OD.ProductID;

-- Verify stock after update
SELECT 'Updated Stock' AS Report, ProductID, ProductName, StockQuantity
FROM Products;
