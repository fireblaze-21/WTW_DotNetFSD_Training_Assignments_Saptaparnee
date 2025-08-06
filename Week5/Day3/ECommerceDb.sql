CREATE DATABASE ECommerceDb;

USE ECommerceDb;

/*1. Products(ProductID, ProductName, Price, Stock)
2. UserWallet(UserID, Balance)
3. Orders(OrderID, UserID, ProductID, Quantity, TotalAmount, OrderDate*/
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(100),
    Price DECIMAL(10,2),
    Stock INT
);

CREATE TABLE UserWallet (
    UserId INT PRIMARY KEY,
	Balance DECIMAL(10,2)
);

CREATE TABLE Orders (
    OrderID INT IDENTITY PRIMARY KEY,
    UserID INT,
    ProductID INT,
    Quantity INT,
    TotalAmount DECIMAL(10,2),
    OrderDate DATETIME
);

/*INSERT SAMPLE VALUES*/

-- Products
INSERT INTO Products VALUES (1, 'Laptop', 800.00, 10);
INSERT INTO Products VALUES (2, 'Smartphone', 400.00, 20);
INSERT INTO Products VALUES (3, 'Headphones', 100.00, 15);

-- Users
INSERT INTO UserWallet VALUES (101, 2000.00);
INSERT INTO UserWallet VALUES (102, 500.00);
INSERT INTO UserWallet VALUES (103, 1500.00);

SELECT * FROM Products;
SELECT * FROM UserWallet;


--CREATE LOG OF ORDERS
CREATE TABLE OrderLog (
    LogID INT IDENTITY PRIMARY KEY,
    UserID INT,
    ProductID INT,
    Quantity INT,
    TotalAmount DECIMAL(10,2),
    OrderDate DATETIME,
    Status NVARCHAR(50)
);

DECLARE @UserID INT = 101;
DECLARE @ProductID INT = 1;
DECLARE @Quantity INT = 2;

DECLARE @ProductPrice DECIMAL(10,2);
DECLARE @AvailableStock INT;
DECLARE @UserBalance DECIMAL(10,2);
DECLARE @TotalAmount DECIMAL(10,2);

BEGIN TRANSACTION;

BEGIN TRY
    -- Step 1: Check product stock
    SELECT @ProductPrice = Price, @AvailableStock = Stock
    FROM Products
    WHERE ProductID = @ProductID;

    IF @AvailableStock < @Quantity
    BEGIN
        RAISERROR('Insufficient product stock', 16, 1);
    END
	 -- Step 2: Calculate total and check user balance
    SET @TotalAmount = @ProductPrice * @Quantity;

    SELECT @UserBalance = Balance
    FROM UserWallet
    WHERE UserID = @UserID;

    IF @UserBalance < @TotalAmount
    BEGIN
        RAISERROR('Insufficient user balance', 16, 1);
    END

    -- Step 3: Deduct stock from product
    UPDATE Products
    SET Stock = Stock - @Quantity
    WHERE ProductID = @ProductID;

    -- Step 4: Deduct balance from user wallet
    UPDATE UserWallet
    SET Balance = Balance - @TotalAmount
    WHERE UserID = @UserID;

    -- Step 5: Insert order record
    INSERT INTO Orders (UserID, ProductID, Quantity, TotalAmount, OrderDate)
    VALUES (@UserID, @ProductID, @Quantity, @TotalAmount, GETDATE());

	-- Step 6 : Insert into Order Log
    INSERT INTO OrderLog(UserID, ProductID, Quantity, TotalAmount, OrderDate, Status)
    VALUES (@UserID, @ProductID, @Quantity, @TotalAmount, GETDATE(), 'SUCCESS');

    -- Commit the transaction
    COMMIT;
    PRINT 'Order placed successfully.';
END TRY

BEGIN CATCH
    -- Rollback if any step fails
    ROLLBACK;
	--LOG FAILURE
	-- Step 6 : Insert into Order Log
    INSERT INTO OrderLog(UserID, ProductID, Quantity, TotalAmount, OrderDate, Status)
    VALUES (@UserID, @ProductID, @Quantity, @TotalAmount, GETDATE(), 'FAILED : '+  ERROR_MESSAGE());
    PRINT 'Order failed: ' + ERROR_MESSAGE();
END CATCH;

SELECT * FROM Products;
SELECT * FROM UserWallet;
SELECT * FROM Orders;
SELECT * FROM OrderLog ORDER BY TotalAmount DESC;

--QUERIES
UPDATE UserWallet
SET Balance=Balance+2000
WHERE UserId=101;