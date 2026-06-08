-- ============================================================
-- Thêm bảng ImportReceipts và ImportDetails vào database
-- Chạy script này trên SQL Server trước khi dùng chức năng nhập hàng
-- ============================================================

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
