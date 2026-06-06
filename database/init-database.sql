-- ============================================================
-- BookStore Management System - Database Initialization Script
-- Run this script in SQL Server to create the database schema
-- ============================================================

USE master;
GO

-- Create database if not exists
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BookStoreDb')
BEGIN
    CREATE DATABASE BookStoreDb;
END
GO

USE BookStoreDb;
GO

-- ============================================================
-- Drop tables if exist (for clean setup)
-- ============================================================
IF OBJECT_ID('dbo.OrderDetails', 'U') IS NOT NULL DROP TABLE OrderDetails;
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID('dbo.Books', 'U') IS NOT NULL DROP TABLE Books;
IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL DROP TABLE Customers;
IF OBJECT_ID('dbo.Employees', 'U') IS NOT NULL DROP TABLE Employees;
IF OBJECT_ID('dbo.Accounts', 'U') IS NOT NULL DROP TABLE Accounts;
IF OBJECT_ID('dbo.Suppliers', 'U') IS NOT NULL DROP TABLE Suppliers;
IF OBJECT_ID('dbo.Categories', 'U') IS NOT NULL DROP TABLE Categories;
GO

-- ============================================================
-- Create tables
-- ============================================================

CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);

CREATE TABLE Suppliers (
    SupplierID INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    Email NVARCHAR(100),
    Phone NVARCHAR(20)
);

CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Salary DECIMAL(18,2) NOT NULL DEFAULT 0,
    Position NVARCHAR(100),
    Role NVARCHAR(20) NOT NULL DEFAULT 'Staff',
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Accounts (
    AccountID INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeID INT NULL,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'Staff',
    FullName NVARCHAR(100) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Address NVARCHAR(255),
    LoyaltyPoints INT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Books (
    BookID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryID INT NOT NULL,
    SupplierID INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    ISBN NVARCHAR(50) NOT NULL,
    Publisher NVARCHAR(100),
    PublishYear INT,
    ImportPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    SellPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    QuantityInStock INT NOT NULL DEFAULT 0,
    LastImportDate DATETIME,
    LastSoldDate DATETIME,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);

CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT,
    EmployeeID INT,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    SubtotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    PaymentStatus NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    PaymentMethod NVARCHAR(50),
    PaymentTransactionId NVARCHAR(100),
    LoyaltyPointsEarned INT NOT NULL DEFAULT 0,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    BookID INT NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    UnitPrice DECIMAL(18,2) NOT NULL DEFAULT 0,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Subtotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);
GO

-- ============================================================
-- Insert sample data
-- ============================================================

-- Categories
INSERT INTO Categories (CategoryName) VALUES
(N'Văn học'),
(N'Kinh tế'),
(N'Khoa học'),
(N'Lịch sử'),
(N'Công nghệ'),
(N'Tâm lý - Kỹ năng sống');

-- Suppliers
INSERT INTO Suppliers (SupplierName, Address, Email, Phone) VALUES
(N'Nhà xuất bản Kim Đồng', N'Hà Nội', N'contact@kimdong.com.vn', N'0241234567'),
(N'Nhà xuất bản Trẻ', N'Hồ Chí Minh', N'nxbtre@nxbtre.com.vn', N'0281234567'),
(N'Alpha Books', N'Hà Nội', N'support@alphabooks.vn', N'0249876543'),
(N'Fahasa', N'Hồ Chí Minh', N'cs@fahasa.com', N'0289876543');

-- Employees
INSERT INTO Employees (FullName, Phone, Salary, Position, Role, CreatedDate) VALUES
(N'Nguyễn Văn Admin', N'0901234567', 15000000, N'Quản lý', N'Admin', GETDATE()),
(N'Trần Thị Staff', N'0909876543', 8000000, N'Nhân viên bán hàng', N'Staff', GETDATE());

-- Accounts
INSERT INTO Accounts (EmployeeID, Username, Password, Role, FullName, IsActive) VALUES
(1, N'admin', N'admin', N'Admin', N'Nguyễn Văn Admin', 1),
(2, N'staff', N'staff', N'Staff', N'Trần Thị Staff', 1);

-- Customers
INSERT INTO Customers (FullName, Phone, Address, LoyaltyPoints, CreatedDate) VALUES
(N'Lê Văn C', N'0912345678', N'123 Đường ABC, Quận 1', 100, GETDATE()),
(N'Phạm Thị D', N'0923456789', N'456 Đường XYZ, Quận 2', 50, GETDATE()),
(N'Hoàng Văn E', N'0934567890', N'789 Đường DEF, Quận 3', 0, GETDATE());

-- Books
INSERT INTO Books (CategoryID, SupplierID, Title, Author, ISBN, Publisher, PublishYear, ImportPrice, SellPrice, QuantityInStock, LastImportDate, LastSoldDate, IsDeleted) VALUES
(1, 1, N'Truyện Kiều', N'Nguyễn Du', N'978-604-2-12345-1', N'NXB Văn Học', 2020, 50000, 75000, 100, GETDATE(), NULL, 0),
(5, 3, N'Clean Code', N'Robert C. Martin', N'978-013-2-35232-0', N'Prentice Hall', 2008, 200000, 350000, 50, GETDATE(), NULL, 0),
(5, 3, N'Design Patterns', N'Gang of Four', N'978-020-1-63361-0', N'Addison-Wesley', 1994, 250000, 400000, 30, GETDATE(), NULL, 0),
(2, 2, N'Nghĩ Giàu Làm Giàu', N'Napoleon Hill', N'978-604-2-56789-2', N'NXB Trẻ', 2019, 60000, 95000, 80, GETDATE(), NULL, 0),
(3, 4, N'Vũ Trụ Trong Vỏ Hạt Dẻ', N'Stephen Hawking', N'978-055-3-38106-2', N'Bantam', 2001, 120000, 180000, 40, GETDATE(), NULL, 0);

GO

PRINT 'Database BookStoreDb initialized successfully!';
