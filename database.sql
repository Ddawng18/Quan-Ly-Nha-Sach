-- ============================================================
-- BookStore Management System - Database Setup Script
-- Copy nội dung file này, paste vào SQL Server Management Studio
-- (SSMS) hoặc Azure Data Studio, rồi nhấn F5 (Execute)
-- ============================================================

CREATE DATABASE BookStoreDb;
GO

USE BookStoreDb;
GO

-- ============================================================
-- TABLES
-- ============================================================

CREATE TABLE Categories (
    CategoryID   INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

CREATE TABLE Suppliers (
    SupplierID   INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(150) NOT NULL,
    Address      NVARCHAR(250),
    Email        NVARCHAR(100),
    Phone        NVARCHAR(20)
);

CREATE TABLE Books (
    BookID          INT IDENTITY(1,1) PRIMARY KEY,
    CategoryID      INT NOT NULL REFERENCES Categories(CategoryID),
    SupplierID      INT NOT NULL REFERENCES Suppliers(SupplierID),
    Title           NVARCHAR(200) NOT NULL,
    Author          NVARCHAR(150) NOT NULL,
    ISBN            NVARCHAR(20)  NOT NULL UNIQUE,
    Publisher       NVARCHAR(150),
    PublishYear     INT,
    ImportPrice     DECIMAL(18,2) NOT NULL DEFAULT 0,
    SellPrice       DECIMAL(18,2) NOT NULL DEFAULT 0,
    QuantityInStock INT           NOT NULL DEFAULT 0,
    LastImportDate  DATETIME,
    LastSoldDate    DATETIME,
    IsDeleted       BIT           NOT NULL DEFAULT 0
);

CREATE TABLE Customers (
    CustomerID     INT IDENTITY(1,1) PRIMARY KEY,
    FullName       NVARCHAR(150) NOT NULL,
    Phone          NVARCHAR(20),
    Address        NVARCHAR(250),
    LoyaltyPoints  INT           NOT NULL DEFAULT 0,
    CreatedDate    DATETIME      NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Employees (
    EmployeeID  INT IDENTITY(1,1) PRIMARY KEY,
    FullName    NVARCHAR(150) NOT NULL,
    Phone       NVARCHAR(20),
    Salary      DECIMAL(18,2) NOT NULL DEFAULT 0,
    Position    NVARCHAR(100),
    Role        NVARCHAR(50)  NOT NULL DEFAULT 'Staff',
    CreatedDate DATETIME      NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Accounts (
    AccountID  INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT REFERENCES Employees(EmployeeID),
    Username   NVARCHAR(50)  NOT NULL UNIQUE,
    Password   NVARCHAR(256) NOT NULL,
    Role       NVARCHAR(50)  NOT NULL DEFAULT 'Staff',
    FullName   NVARCHAR(150) NOT NULL,
    IsActive   BIT           NOT NULL DEFAULT 1
);

CREATE TABLE Orders (
    OrderID              INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID           INT REFERENCES Customers(CustomerID),
    EmployeeID           INT REFERENCES Employees(EmployeeID),
    OrderDate            DATETIME      NOT NULL DEFAULT GETDATE(),
    SubtotalAmount       DECIMAL(18,2) NOT NULL DEFAULT 0,
    DiscountAmount       DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount            DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount          DECIMAL(18,2) NOT NULL DEFAULT 0,
    PaymentStatus        NVARCHAR(20)  NOT NULL DEFAULT 'Pending',
    PaymentMethod        NVARCHAR(50),
    PaymentTransactionId NVARCHAR(100),
    LoyaltyPointsEarned  INT           NOT NULL DEFAULT 0
);

CREATE TABLE OrderDetails (
    OrderDetailID  INT IDENTITY(1,1) PRIMARY KEY,
    OrderID        INT           NOT NULL REFERENCES Orders(OrderID),
    BookID         INT           NOT NULL REFERENCES Books(BookID),
    Quantity       INT           NOT NULL,
    UnitPrice      DECIMAL(18,2) NOT NULL,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Subtotal       DECIMAL(18,2) NOT NULL
);
CREATE TABLE ImportReceipts (
    ImportID    INT           IDENTITY(1,1) PRIMARY KEY,
    SupplierID  INT           NOT NULL,
    EmployeeID  INT           NOT NULL,
    ImportDate  DATETIME      NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Note        NVARCHAR(500) NULL,

    CONSTRAINT FK_Import_Supplier FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    CONSTRAINT FK_Import_Employee FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE ImportDetails (
    ImportDetailID INT           IDENTITY(1,1) PRIMARY KEY,
    ImportID       INT           NOT NULL,
    BookID         INT           NOT NULL,
    Quantity       INT           NOT NULL,
    ImportPrice    DECIMAL(18,2) NOT NULL,
    Subtotal       DECIMAL(18,2) NOT NULL,

    CONSTRAINT FK_ImportDetail_Receipt FOREIGN KEY (ImportID) REFERENCES ImportReceipts(ImportID),
    CONSTRAINT FK_ImportDetail_Book    FOREIGN KEY (BookID)   REFERENCES Books(BookID),
    CONSTRAINT CK_ImportDetail_Qty     CHECK (Quantity > 0),
    CONSTRAINT CK_ImportDetail_Price   CHECK (ImportPrice >= 0)
);

GO

-- ============================================================
-- SEED DATA
-- ============================================================

SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (CategoryID, CategoryName) VALUES
(1, N'Fiction'),
(2, N'Science'),
(3, N'Classic'),
(4, N'Philosophy');
SET IDENTITY_INSERT Categories OFF;

SET IDENTITY_INSERT Suppliers ON;
INSERT INTO Suppliers (SupplierID, SupplierName, Address, Email, Phone) VALUES
(1, N'Alpha Books Co.',      N'123 Nguyen Hue, Ho Chi Minh City', 'contact@alphabooks.vn', '0281234567'),
(2, N'Northern Publishing',  N'45 Ba Trieu, Ha Noi',               'sales@northpub.vn',    '0247654321'),
(3, N'Central Media Supply', N'78 Bach Dang, Da Nang',             'info@centralmedia.vn', '0236123789');
SET IDENTITY_INSERT Suppliers OFF;

SET IDENTITY_INSERT Books ON;
INSERT INTO Books (BookID,CategoryID,SupplierID,Title,Author,ISBN,Publisher,PublishYear,ImportPrice,SellPrice,QuantityInStock) VALUES
(1,  2,1,N'Clean Code',                          N'Robert C. Martin',          '978-0132350884', N'Prentice Hall',           2008,  12,   20,  10),
(2,  1,2,N'No Longer Human',                     N'Osamu Dazai',               '978-0811204815', N'New Directions',          1958,  80,  120,  21),
(3,  3,1,N'The Great Gatsby',                    N'F. Scott Fitzgerald',        '978-0743273565', N'Scribner',                1925,  10,   20,  10),
(4,  1,3,N'Oregairu',                            N'Wataru Watari',              '978-4048668524', N'Shogakukan',              2011,  24,   36,  67),
(5,  3,1,N'To Kill a Mockingbird',               N'Harper Lee',                 '978-0061120084', N'HarperCollins',           1960,   5,    7,  10),
(6,  4,2,N'L''Étranger',                         N'Albert Camus',               '978-2070360024', N'Gallimard',               1942,  30,   45,  12),
(7,  2,3,N'Do Androids Dream of Electric Sheep?',N'Philip K. Dick',             '978-0345404473', N'Doubleday',               1968,  20,   30,   5),
(8,  1,1,N'The Little Prince',                   N'Antoine de Saint-Exupéry',   '978-0156012195', N'Reynal & Hitchcock',      1943,   6,   10,  10),
(9,  3,2,N'Don Quixote',                         N'Miguel de Cervantes',        '978-0060934347', N'Francisco de Robles',     1605,  85,  120,  12),
(10, 3,1,N'Moby Dick',                           N'Herman Melville',            '978-0142437247', N'Harper & Brothers',       1851,  35,   50,   3),
(11, 1,3,N'The Lord of the Rings',               N'J.R.R. Tolkien',             '978-0544003415', N'Allen & Unwin',           1954, 140,  200,  10),
(12, 3,2,N'The Odyssey',                         N'Homer',                      '978-0140268867', N'Penguin Classics',        1996, 900, 1200,   2),
(13, 3,1,N'Faust',                               N'Johann Wolfgang von Goethe', '978-0140449134', N'Cotta''sche Buchhandlung',1808,  70,  100,   2),
(14, 3,2,N'Crime and Punishment',                N'Fyodor Dostoevsky',          '978-0143058144', N'The Russian Messenger',   1866, 105,  150,  10),
(15, 1,3,N'Metamorphosis',                       N'Franz Kafka',                '978-0553213690', N'Kurt Wolff Verlag',       1915,  55,   80,  10),
(16, 3,1,N'War and Peace',                       N'Leo Tolstoy',                '978-0192833983', N'The Russian Messenger',   1869, 140,  200,  10),
(17, 1,2,N'The Adventures of Sherlock Holmes',   N'Arthur Conan Doyle',         '978-0140437728', N'George Newnes Ltd',       1892,  70,  100,  10),
(18, 1,3,N'Kumo Desu ga, Nanika.',               N'Okina Baba',                 '978-4040734540', N'Kadokawa Shoten',         2015,  24,   36,  10),
(19, 3,1,N'Wuthering Heights',                   N'Emily Brontë',               '978-0141439556', N'Thomas Cautley Newby',    1847,  14,   20,  10),
(20, 1,2,N'Jigokuhen',                           N'Ryūnosuke Akutagawa',         '978-4101001011', N'Iwanami Shoten',          1918,  70,  100,   9);
SET IDENTITY_INSERT Books OFF;

SET IDENTITY_INSERT Customers ON;
INSERT INTO Customers (CustomerID, FullName, Phone, Address, LoyaltyPoints, CreatedDate) VALUES
(1, N'Nguyen Van A', '0901111111', N'Ho Chi Minh City', 120, DATEADD(DAY, -10, GETDATE())),
(2, N'Tran Thi B',   '0902222222', N'Ha Noi',            80, DATEADD(DAY, -7,  GETDATE())),
(3, N'Le Van C',     '0903333333', N'Da Nang',             0, DATEADD(DAY, -2,  GETDATE()));
SET IDENTITY_INSERT Customers OFF;

SET IDENTITY_INSERT Employees ON;
INSERT INTO Employees (EmployeeID, FullName, Phone, Salary, Position, Role, CreatedDate) VALUES
(1, N'Tran Van Staff',    '0911000001',  8000000, N'Sales Staff',   'Staff', DATEADD(MONTH, -6,  GETDATE())),
(2, N'Pham Thi Manager',  '0911000002', 15000000, N'Store Manager', 'Admin', DATEADD(MONTH, -12, GETDATE())),
(3, N'Hoang Van Support', '0911000003',  7000000, N'Support',       'Staff', DATEADD(MONTH, -3,  GETDATE()));
SET IDENTITY_INSERT Employees OFF;

SET IDENTITY_INSERT Accounts ON;
INSERT INTO Accounts (AccountID, Username, Password, Role, FullName, IsActive) VALUES
(1, 'admin', '1', 'Admin', N'Administrator', 1),
(2, 'E',     '2', 'Staff', N'Employee',       1);
SET IDENTITY_INSERT Accounts OFF;

SET IDENTITY_INSERT Orders ON;
INSERT INTO Orders (OrderID,CustomerID,EmployeeID,OrderDate,SubtotalAmount,DiscountAmount,TaxAmount,TotalAmount,PaymentStatus,PaymentMethod,LoyaltyPointsEarned) VALUES
(1, 1, 1, DATEADD(DAY,-5,GETDATE()), 140, 0, 0, 140, 'Paid',    'Cash', 1),
(2, 2, 1, DATEADD(DAY,-3,GETDATE()), 156, 0, 0, 156, 'Paid',    'Cash', 1),
(3, 3, 2, DATEADD(DAY,-1,GETDATE()),  50, 0, 0,  50, 'Pending', 'Cash', 0);
SET IDENTITY_INSERT Orders OFF;

SET IDENTITY_INSERT OrderDetails ON;
INSERT INTO OrderDetails (OrderDetailID,OrderID,BookID,Quantity,UnitPrice,DiscountAmount,Subtotal) VALUES
(1, 1, 1, 2,  20, 0,  40),
(2, 1, 5, 1, 100, 0, 100),
(3, 2, 4, 2,  36, 0,  72),
(4, 2, 8, 2,  42, 0,  84),
(5, 3, 7, 1,  50, 0,  50);
SET IDENTITY_INSERT OrderDetails OFF;

PRINT 'Database BookStoreDb created and seeded successfully!';
GO
