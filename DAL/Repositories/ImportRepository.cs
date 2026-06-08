using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class ImportRepository : IImportRepository
{
    public IReadOnlyList<ImportReceiptViewDto> GetAll()
        => FetchReceipts(null);

    public IReadOnlyList<ImportReceiptViewDto> GetBySupplier(int supplierId)
        => FetchReceipts(supplierId);

    public IReadOnlyList<ImportDetailViewDto> GetDetails(int importId)
    {
        var list = new List<ImportDetailViewDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT id.*, b.Title AS BookTitle, b.ISBN
            FROM ImportDetails id
            JOIN Books b ON b.BookID = id.BookID
            WHERE id.ImportID = @id", conn)
        {
            CommandTimeout = DbConnectionFactory.CommandTimeout
        };
        cmd.Parameters.AddWithValue("@id", importId);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new ImportDetailViewDto
            {
                ImportDetailID = (int)reader["ImportDetailID"],
                ImportID       = (int)reader["ImportID"],
                BookID         = (int)reader["BookID"],
                BookTitle      = reader["BookTitle"].ToString()!,
                ISBN           = reader["ISBN"].ToString()!,
                Quantity       = (int)reader["Quantity"],
                ImportPrice    = (decimal)reader["ImportPrice"],
                Subtotal       = (decimal)reader["Subtotal"]
            });
        }
        return list;
    }

    public void CreateImport(ImportReceipt receipt, IReadOnlyList<ImportDetail> details)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var tran = conn.BeginTransaction();
        try
        {
            using var cmdReceipt = new SqlCommand(@"
                INSERT INTO ImportReceipts
                    (SupplierID, EmployeeID, ImportDate, TotalAmount, Note)
                OUTPUT INSERTED.ImportID
                VALUES
                    (@sup, @emp, GETDATE(), @total, @note)", conn, tran)
            {
                CommandTimeout = DbConnectionFactory.CommandTimeout
            };

            cmdReceipt.Parameters.AddWithValue("@sup",   receipt.SupplierID);
            cmdReceipt.Parameters.AddWithValue("@emp",   receipt.EmployeeID);
            cmdReceipt.Parameters.AddWithValue("@total", receipt.TotalAmount);
            cmdReceipt.Parameters.AddWithValue("@note",  (object?)receipt.Note ?? DBNull.Value);

            receipt.ImportID = (int)cmdReceipt.ExecuteScalar();

            foreach (var d in details)
            {
                using var cmdDetail = new SqlCommand(@"
                    INSERT INTO ImportDetails
                        (ImportID, BookID, Quantity, ImportPrice, Subtotal)
                    VALUES
                        (@iid, @bid, @qty, @price, @sub)", conn, tran)
                {
                    CommandTimeout = DbConnectionFactory.CommandTimeout
                };

                cmdDetail.Parameters.AddWithValue("@iid",   receipt.ImportID);
                cmdDetail.Parameters.AddWithValue("@bid",   d.BookID);
                cmdDetail.Parameters.AddWithValue("@qty",   d.Quantity);
                cmdDetail.Parameters.AddWithValue("@price", d.ImportPrice);
                cmdDetail.Parameters.AddWithValue("@sub",   d.Subtotal);
                cmdDetail.ExecuteNonQuery();

                using var cmdStock = new SqlCommand(@"
                    UPDATE Books SET
                        QuantityInStock = QuantityInStock + @qty,
                        ImportPrice     = @price,
                        LastImportDate  = GETDATE()
                    WHERE BookID = @bid", conn, tran)
                {
                    CommandTimeout = DbConnectionFactory.CommandTimeout
                };

                cmdStock.Parameters.AddWithValue("@qty",   d.Quantity);
                cmdStock.Parameters.AddWithValue("@price", d.ImportPrice);
                cmdStock.Parameters.AddWithValue("@bid",   d.BookID);
                cmdStock.ExecuteNonQuery();
            }

            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    private static IReadOnlyList<ImportReceiptViewDto> FetchReceipts(int? supplierId)
    {
        var list = new List<ImportReceiptViewDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();

        var sql = @"
            SELECT ir.*,
                   s.SupplierName,
                   e.FullName AS EmployeeName
            FROM ImportReceipts ir
            LEFT JOIN Suppliers s ON s.SupplierID = ir.SupplierID
            LEFT JOIN Employees e ON e.EmployeeID = ir.EmployeeID
            WHERE 1=1";

        if (supplierId.HasValue) sql += " AND ir.SupplierID = @sup";
        sql += " ORDER BY ir.ImportDate DESC";

        using var cmd = new SqlCommand(sql, conn)
        {
            CommandTimeout = DbConnectionFactory.CommandTimeout
        };
        if (supplierId.HasValue) cmd.Parameters.AddWithValue("@sup", supplierId.Value);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new ImportReceiptViewDto
            {
                ImportID     = (int)reader["ImportID"],
                SupplierID   = (int)reader["SupplierID"],
                SupplierName = reader["SupplierName"]?.ToString() ?? "-",
                EmployeeID   = (int)reader["EmployeeID"],
                EmployeeName = reader["EmployeeName"]?.ToString() ?? "-",
                ImportDate   = (DateTime)reader["ImportDate"],
                TotalAmount  = (decimal)reader["TotalAmount"],
                Note         = reader["Note"]?.ToString()
            });
        }
        return list;
    }
}
