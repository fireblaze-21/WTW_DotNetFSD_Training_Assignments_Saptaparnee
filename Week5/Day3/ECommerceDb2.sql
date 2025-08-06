CREATE TABLE StockAudit (
    AuditID INT IDENTITY PRIMARY KEY,
    ProductID INT,
    OldStock INT,
    NewStock INT,
    ChangedAt DATETIME
);

CREATE TABLE WalletAudit (
    AuditID INT IDENTITY PRIMARY KEY,
    UserID INT,
    OldBalance DECIMAL(10,2),
    NewBalance DECIMAL(10,2),
    TransactionType NVARCHAR(10),
    ChangedAt DATETIME
);

CREATE TRIGGER trg_StockAudit
ON Products
AFTER UPDATE
AS
BEGIN
    INSERT INTO StockAudit (ProductID, OldStock, NewStock, ChangedAt)
    SELECT d.ProductID, d.Stock, i.Stock, GETDATE()
    FROM DELETED d
    JOIN INSERTED i ON d.ProductID = i.ProductID
    WHERE d.Stock <> i.Stock;  -- Only if stock changed
	Print 'AFTER UPDATE TRIGGER IS EXECUTED';
END;


SELECT * FROM Products;
UPDATE Products
SET Stock=Stock+4
WHERE ProductID=1;
SELECT * FROM Products;

SELECT * FROM StockAudit



------------------------------------------------------------------------------------------------------


CREATE TRIGGER trg_WalletAudit
ON UserWallet
AFTER UPDATE
AS
BEGIN
    INSERT INTO WalletAudit (UserID, OldBalance, NewBalance, TransactionType, ChangedAt)
    SELECT 
        d.UserID, 
        d.Balance, 
        i.Balance,
        CASE 
            WHEN i.Balance < d.Balance THEN 'Debit' 
            WHEN i.Balance > d.Balance THEN 'Credit'
            ELSE 'None'
        END,
        GETDATE()
    FROM DELETED d
    JOIN INSERTED i ON d.UserID = i.UserID
    WHERE d.Balance <> i.Balance;
	PRINT ' TRIGGER AFTER BALANCE UPDATE IS EXECUTED'
END;

-----------------------------------------------------------------------------------------------------


CREATE TRIGGER trg_PreventZeroStockOrders
ON Orders
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM INSERTED i
        JOIN Products p ON i.ProductID = p.ProductID
        WHERE p.Stock = 0
    )
    BEGIN
	    PRINT ' INSTEAD OF INSERT TRIGGER EXECUTED'
        RAISERROR('Cannot place order: Product is out of stock.', 16, 1);
        RETURN;
    END

    -- Insert only if stock is available
    INSERT INTO Orders (UserID, ProductID, Quantity, TotalAmount, OrderDate)
    SELECT UserID, ProductID, Quantity, TotalAmount, OrderDate
    FROM INSERTED;
	PRINT 'INSERT TRIGGER EXECUTED'
END;

INSERT INTO Products VALUES (4, 'MOUSE' , 500.00, 0);
-- This will fail due to stock = 0
INSERT INTO Orders (UserID, ProductID, Quantity, TotalAmount)
VALUES (102, 4, 1, 500.00);

-------------------------------------------------------------------------------------------------------------


CREATE TRIGGER trg_LowStockAlert
ON Orders
AFTER INSERT
AS
BEGIN
    DECLARE @Stock INT;
    DECLARE @ProductID INT;

    SELECT TOP 1 @ProductID = ProductID
    FROM INSERTED;

    SELECT @Stock = Stock
    FROM Products
    WHERE ProductID = @ProductID;

    IF @Stock < 5
    BEGIN
        PRINT 'ALERT: Stock for Product ID ' + CAST(@ProductID AS NVARCHAR) + ' is below 5!';
    END
END;


SELECT * FROM WalletAudit;
SELECT * FROM StockAudit;