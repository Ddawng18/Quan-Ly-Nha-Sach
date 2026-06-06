using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    public IReadOnlyList<OrderViewDto> GetAll() => FetchOrders(null, null);

    public IReadOnlyList<OrderViewDto> GetByDateRange(DateTime? fromDate, DateTime? toDate)
        => FetchOrders(fromDate, toDate);

    public Order? GetOrder(int orderId)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT * FROM Orders WHERE OrderID = @id", conn);
        cmd.Parameters.AddWithValue("@id", orderId);
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapOrder(reader) : null;
    }

    public IReadOnlyList<OrderDetailViewDto> GetDetails(int orderId)
    {
        var list = new List<OrderDetailViewDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT od.*, b.Title AS BookTitle
            FROM OrderDetails od
            JOIN Books b ON b.BookID = od.BookID
            WHERE od.OrderID = @id", conn);
        cmd.Parameters.AddWithValue("@id", orderId);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new OrderDetailViewDto
            {
                OrderDetailID  = (int)reader["OrderDetailID"],
                OrderID        = (int)reader["OrderID"],
                BookID         = (int)reader["BookID"],
                BookTitle      = reader["BookTitle"].ToString()!,
                Quantity       = (int)reader["Quantity"],
                UnitPrice      = (decimal)reader["UnitPrice"],
                DiscountAmount = (decimal)reader["DiscountAmount"],
                Subtotal       = (decimal)reader["Subtotal"]
            });
        }
        return list;
    }

    public void CreateOrder(Order order, IReadOnlyList<OrderDetail> details)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var tran = conn.BeginTransaction();
        try
        {
            // 1. Insert Order
            using var cmdOrder = new SqlCommand(@"
                INSERT INTO Orders
                    (CustomerID, EmployeeID, OrderDate, SubtotalAmount, DiscountAmount,
                     TaxAmount, TotalAmount, PaymentStatus, PaymentMethod,
                     PaymentTransactionId, LoyaltyPointsEarned)
                OUTPUT INSERTED.OrderID
                VALUES
                    (@cust, @emp, GETDATE(), @sub, @disc,
                     @tax, @total, @status, @method,
                     @txn, @points)", conn, tran);

            cmdOrder.Parameters.AddWithValue("@cust",   (object?)order.CustomerID    ?? DBNull.Value);
            cmdOrder.Parameters.AddWithValue("@emp",    (object?)order.EmployeeID    ?? DBNull.Value);
            cmdOrder.Parameters.AddWithValue("@sub",    order.SubtotalAmount);
            cmdOrder.Parameters.AddWithValue("@disc",   order.DiscountAmount);
            cmdOrder.Parameters.AddWithValue("@tax",    order.TaxAmount);
            cmdOrder.Parameters.AddWithValue("@total",  order.TotalAmount);
            cmdOrder.Parameters.AddWithValue("@status", order.PaymentStatus ?? "Pending");
            cmdOrder.Parameters.AddWithValue("@method", (object?)order.PaymentMethod         ?? DBNull.Value);
            cmdOrder.Parameters.AddWithValue("@txn",    (object?)order.PaymentTransactionId  ?? DBNull.Value);
            cmdOrder.Parameters.AddWithValue("@points", order.LoyaltyPointsEarned);

            order.OrderID = (int)cmdOrder.ExecuteScalar();

            // 2. Insert OrderDetails
            foreach (var d in details)
            {
                using var cmdDetail = new SqlCommand(@"
                    INSERT INTO OrderDetails
                        (OrderID, BookID, Quantity, UnitPrice, DiscountAmount, Subtotal)
                    VALUES
                        (@oid, @bid, @qty, @price, @disc, @sub)", conn, tran);

                cmdDetail.Parameters.AddWithValue("@oid",   order.OrderID);
                cmdDetail.Parameters.AddWithValue("@bid",   d.BookID);
                cmdDetail.Parameters.AddWithValue("@qty",   d.Quantity);
                cmdDetail.Parameters.AddWithValue("@price", d.UnitPrice);
                cmdDetail.Parameters.AddWithValue("@disc",  d.DiscountAmount);
                cmdDetail.Parameters.AddWithValue("@sub",   d.Subtotal);
                cmdDetail.ExecuteNonQuery();
            }

            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            throw;
        }
    }

    public void UpdateStatus(int orderId, string status)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "UPDATE Orders SET PaymentStatus = @status WHERE OrderID = @id", conn);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.Parameters.AddWithValue("@id",     orderId);
        cmd.ExecuteNonQuery();
    }

    // ── helpers ──────────────────────────────────────────────
    private static IReadOnlyList<OrderViewDto> FetchOrders(DateTime? from, DateTime? to)
    {
        var list = new List<OrderViewDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();

        var sql = @"
            SELECT o.*,
                   c.FullName AS CustomerName,
                   e.FullName AS EmployeeName
            FROM Orders o
            LEFT JOIN Customers c ON c.CustomerID = o.CustomerID
            LEFT JOIN Employees e ON e.EmployeeID = o.EmployeeID
            WHERE 1=1";

        if (from.HasValue) sql += " AND CAST(o.OrderDate AS DATE) >= @from";
        if (to.HasValue)   sql += " AND CAST(o.OrderDate AS DATE) <= @to";
        sql += " ORDER BY o.OrderDate DESC";

        using var cmd = new SqlCommand(sql, conn);
        if (from.HasValue) cmd.Parameters.AddWithValue("@from", from.Value.Date);
        if (to.HasValue)   cmd.Parameters.AddWithValue("@to",   to.Value.Date);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new OrderViewDto
            {
                OrderID              = (int)reader["OrderID"],
                CustomerID           = reader["CustomerID"] == DBNull.Value ? 0 : (int)reader["CustomerID"],
                CustomerName         = reader["CustomerName"]?.ToString() ?? "-",
                EmployeeID           = reader["EmployeeID"] == DBNull.Value ? 0 : (int)reader["EmployeeID"],
                EmployeeName         = reader["EmployeeName"]?.ToString() ?? "-",
                OrderDate            = (DateTime)reader["OrderDate"],
                SubtotalAmount       = (decimal)reader["SubtotalAmount"],
                DiscountAmount       = (decimal)reader["DiscountAmount"],
                TaxAmount            = (decimal)reader["TaxAmount"],
                TotalAmount          = (decimal)reader["TotalAmount"],
                PaymentStatus        = reader["PaymentStatus"].ToString()!,
                PaymentMethod        = reader["PaymentMethod"]?.ToString(),
                PaymentTransactionId = reader["PaymentTransactionId"]?.ToString()
            });
        }
        return list;
    }

    private static Order MapOrder(SqlDataReader r) => new()
    {
        OrderID              = (int)r["OrderID"],
        CustomerID           = r["CustomerID"] == DBNull.Value ? 0 : (int)r["CustomerID"],
        EmployeeID           = r["EmployeeID"] == DBNull.Value ? 0 : (int)r["EmployeeID"],
        OrderDate            = (DateTime)r["OrderDate"],
        SubtotalAmount       = (decimal)r["SubtotalAmount"],
        DiscountAmount       = (decimal)r["DiscountAmount"],
        TaxAmount            = (decimal)r["TaxAmount"],
        TotalAmount          = (decimal)r["TotalAmount"],
        PaymentStatus        = r["PaymentStatus"].ToString()!,
        PaymentMethod        = r["PaymentMethod"]?.ToString(),
        PaymentTransactionId = r["PaymentTransactionId"]?.ToString(),
        LoyaltyPointsEarned  = (int)r["LoyaltyPointsEarned"]
    };
}
