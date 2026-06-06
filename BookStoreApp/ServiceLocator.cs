using BookStoreApp.BLL;
using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;

namespace BookStoreApp;

/// <summary>
/// Manual dependency injection root.
/// All UI controls and forms obtain their service dependencies from here
/// instead of constructing concrete implementations directly.
/// </summary>
public static class ServiceLocator
{
    // Repositories — SQL Server
    private static readonly IBookRepository      BookRepo      = new BookRepository();
    private static readonly ICustomerRepository  CustomerRepo  = new CustomerRepository();
    private static readonly IEmployeeRepository  EmployeeRepo  = new EmployeeRepository();
    private static readonly IOrderRepository     OrderRepo     = new OrderRepository();
    private static readonly ICategoryRepository  CategoryRepo  = new CategoryRepository();
    private static readonly ISupplierRepository  SupplierRepo  = new SupplierRepository();
    private static readonly IAccountRepository   AccountRepo   = new AccountRepository();
    private static readonly IDashboardRepository DashboardRepo = new DashboardRepository();
    private static readonly IReportRepository    ReportRepo    = new ReportRepository();

    // Shared service instances
    private static readonly ILoyaltyService LoyaltySvc = new LoyaltyService();
    private static readonly IOrderService   OrderSvc;

    // Public service accessors
    public static IBookService      BookService      { get; }
    public static ICategoryService  CategoryService  { get; }
    public static ICustomerService  CustomerService  { get; }
    public static IEmployeeService  EmployeeService  { get; }
    public static IOrderService     OrderService     => OrderSvc;
    public static IPosService       PosService       { get; }
    public static IReportService    ReportService    { get; }
    public static ISupplierService  SupplierService  { get; }
    public static IAuthService      AuthService      { get; }
    public static IDashboardService DashboardService { get; }

    static ServiceLocator()
    {
        OrderSvc = new OrderService(OrderRepo, BookRepo);

        // BookService nhận thêm CategoryRepo để không cần FakeDatabase
        BookService      = new BookService(BookRepo, CategoryRepo);
        CategoryService  = new CategoryService(CategoryRepo);
        CustomerService  = new CustomerService(CustomerRepo);
        EmployeeService  = new EmployeeService(EmployeeRepo);
        PosService       = new PosService(BookRepo, CustomerRepo, OrderSvc, LoyaltySvc);
        ReportService    = new ReportService(ReportRepo);
        SupplierService  = new SupplierService(SupplierRepo);
        AuthService      = new AuthService(AccountRepo);
        DashboardService = new DashboardService(DashboardRepo);
    }
}
