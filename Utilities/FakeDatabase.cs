using BookStoreApp.DTO;

namespace BookStoreApp.Utilities;

public static class FakeDatabase
{
    public static List<Account> Accounts { get; } =
    [
        new Account
        {
            AccountID = 1,
            Username = "admin",
            Password = "1",
            Role = "Admin",
            FullName = "Administrator",
            IsActive = true
        },
        new Account
        {
            AccountID = 2,
            Username = "E",
            Password = "2",
            Role = "Staff",
            FullName = "Employee",
            IsActive = true
        }
    ];

    public static List<Category> Categories { get; } =
    [
        new Category { CategoryID = 1, CategoryName = "Fiction" },
        new Category { CategoryID = 2, CategoryName = "Science" },
        new Category { CategoryID = 3, CategoryName = "Classic" },
        new Category { CategoryID = 4, CategoryName = "Philosophy" }
    ];

    public static List<Supplier> Suppliers { get; } =
    [
        new Supplier
        {
            SupplierID = 1,
            SupplierName = "Alpha Books Co.",
            Address = "123 Nguyen Hue, Ho Chi Minh City",
            Email = "contact@alphabooks.vn",
            Phone = "0281234567"
        },
        new Supplier
        {
            SupplierID = 2,
            SupplierName = "Northern Publishing",
            Address = "45 Ba Trieu, Ha Noi",
            Email = "sales@northpub.vn",
            Phone = "0247654321"
        },
        new Supplier
        {
            SupplierID = 3,
            SupplierName = "Central Media Supply",
            Address = "78 Bach Dang, Da Nang",
            Email = "info@centralmedia.vn",
            Phone = "0236123789"
        }
    ];

    public static List<Book> Books { get; } =
    [
        new Book
        {
            BookID = 1,
            CategoryID = 2,
            SupplierID = 1,
            Title = "Clean Code",
            Author = "Robert C. Martin",
            ISBN = "978-0132350884",
            Publisher = "Prentice Hall",
            PublishYear = 2008,
            ImportPrice = 12,
            SellPrice = 20,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 2,
            CategoryID = 1,
            SupplierID = 2,
            Title = "No Longer Human",
            Author = "Osamu Dazai",
            ISBN = "978-0811204815",
            Publisher = "New Directions",
            PublishYear = 1958,
            ImportPrice = 80,
            SellPrice = 120,
            QuantityInStock = 21
        },
        new Book
        {
            BookID = 3,
            CategoryID = 3,
            SupplierID = 1,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            ISBN = "978-0743273565",
            Publisher = "Scribner",
            PublishYear = 1925,
            ImportPrice = 10,
            SellPrice = 20,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 4,
            CategoryID = 1,
            SupplierID = 3,
            Title = "Oregairu",
            Author = "Wataru Watari",
            ISBN = "978-4048668524",
            Publisher = "Shogakukan",
            PublishYear = 2011,
            ImportPrice = 24,
            SellPrice = 36,
            QuantityInStock = 67
        },
        new Book
        {
            BookID = 5,
            CategoryID = 3,
            SupplierID = 1,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            ISBN = "978-0061120084",
            Publisher = "HarperCollins",
            PublishYear = 1960,
            ImportPrice = 5,
            SellPrice = 7,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 6,
            CategoryID = 4,
            SupplierID = 2,
            Title = "L'Étranger",
            Author = "Albert Camus",
            ISBN = "978-2070360024",
            Publisher = "Gallimard",
            PublishYear = 1942,
            ImportPrice = 30,
            SellPrice = 45,
            QuantityInStock = 12
        },
        new Book
        {
            BookID = 7,
            CategoryID = 2,
            SupplierID = 3,
            Title = "Do Androids Dream of Electric Sheep?",
            Author = "Philip K. Dick",
            ISBN = "978-0345404473",
            Publisher = "Doubleday",
            PublishYear = 1968,
            ImportPrice = 20,
            SellPrice = 30,
            QuantityInStock = 5
        },
        new Book
        {
            BookID = 8,
            CategoryID = 1,
            SupplierID = 1,
            Title = "The Little Prince",
            Author = "Antoine de Saint-Exupéry",
            ISBN = "978-0156012195",
            Publisher = "Reynal & Hitchcock",
            PublishYear = 1943,
            ImportPrice = 6,
            SellPrice = 10,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 9,
            CategoryID = 3,
            SupplierID = 2,
            Title = "Don Quixote",
            Author = "Miguel de Cervantes",
            ISBN = "978-0060934347",
            Publisher = "Francisco de Robles",
            PublishYear = 1605,
            ImportPrice = 85,
            SellPrice = 120,
            QuantityInStock = 12
        },
        new Book
        {
            BookID = 10,
            CategoryID = 3,
            SupplierID = 1,
            Title = "Moby Dick",
            Author = "Herman Melville",
            ISBN = "978-0142437247",
            Publisher = "Harper & Brothers",
            PublishYear = 1851,
            ImportPrice = 35,
            SellPrice = 50,
            QuantityInStock = 3
        },
        new Book
        {
            BookID = 11,
            CategoryID = 1,
            SupplierID = 3,
            Title = "The Lord of the Rings",
            Author = "J.R.R. Tolkien",
            ISBN = "978-0544003415",
            Publisher = "Allen & Unwin",
            PublishYear = 1954,
            ImportPrice = 140,
            SellPrice = 200,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 12,
            CategoryID = 3,
            SupplierID = 2,
            Title = "The Odyssey",
            Author = "Homer",
            ISBN = "978-0140268867",
            Publisher = "Penguin Classics",
            PublishYear = 1996,
            ImportPrice = 900,
            SellPrice = 1200,
            QuantityInStock = 2
        },
        new Book
        {
            BookID = 13,
            CategoryID = 3,
            SupplierID = 1,
            Title = "Faust",
            Author = "Johann Wolfgang von Goethe",
            ISBN = "978-0140449134",
            Publisher = "Cotta'sche Buchhandlung",
            PublishYear = 1808,
            ImportPrice = 70,
            SellPrice = 100,
            QuantityInStock = 2
        },
        new Book
        {
            BookID = 14,
            CategoryID = 3,
            SupplierID = 2,
            Title = "Crime and Punishment",
            Author = "Fyodor Dostoevsky",
            ISBN = "978-0143058144",
            Publisher = "The Russian Messenger",
            PublishYear = 1866,
            ImportPrice = 105,
            SellPrice = 150,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 15,
            CategoryID = 1,
            SupplierID = 3,
            Title = "Metamorphosis",
            Author = "Franz Kafka",
            ISBN = "978-0553213690",
            Publisher = "Kurt Wolff Verlag",
            PublishYear = 1915,
            ImportPrice = 55,
            SellPrice = 80,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 16,
            CategoryID = 3,
            SupplierID = 1,
            Title = "War and Peace",
            Author = "Leo Tolstoy",
            ISBN = "978-0192833983",
            Publisher = "The Russian Messenger",
            PublishYear = 1869,
            ImportPrice = 140,
            SellPrice = 200,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 17,
            CategoryID = 1,
            SupplierID = 2,
            Title = "The Adventures of Sherlock Holmes",
            Author = "Arthur Conan Doyle",
            ISBN = "978-0140437728",
            Publisher = "George Newnes Ltd",
            PublishYear = 1892,
            ImportPrice = 70,
            SellPrice = 100,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 18,
            CategoryID = 1,
            SupplierID = 3,
            Title = "Kumo Desu ga, Nanika.",
            Author = "Okina Baba",
            ISBN = "978-4040734540",
            Publisher = "Kadokawa Shoten",
            PublishYear = 2015,
            ImportPrice = 24,
            SellPrice = 36,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 19,
            CategoryID = 3,
            SupplierID = 1,
            Title = "Wuthering Heights",
            Author = "Emily Brontë",
            ISBN = "978-0141439556",
            Publisher = "Thomas Cautley Newby",
            PublishYear = 1847,
            ImportPrice = 14,
            SellPrice = 20,
            QuantityInStock = 10
        },
        new Book
        {
            BookID = 20,
            CategoryID = 1,
            SupplierID = 2,
            Title = "Jigokuhen",
            Author = "Ryūnosuke Akutagawa",
            ISBN = "978-4101001011",
            Publisher = "Iwanami Shoten",
            PublishYear = 1918,
            ImportPrice = 70,
            SellPrice = 100,
            QuantityInStock = 9
        }
    ];

    public static List<Customer> Customers { get; } =
    [
    new Customer
    {
        CustomerID = 1,
        FullName = "Nguyen Van A",
        Phone = "0901111111",
        Address = "Ho Chi Minh City",
        LoyaltyPoints = 120,
        CreatedDate = DateTime.Now.AddDays(-10)
    },

    new Customer
    {
        CustomerID = 2,
        FullName = "Tran Thi B",
        Phone = "0902222222",
        Address = "Ha Noi",
        LoyaltyPoints = 80,
        CreatedDate = DateTime.Now.AddDays(-7)
    },

    new Customer
    {
        CustomerID = 3,
        FullName = "Le Van C",
        Phone = "0903333333",
        Address = "Da Nang",
        LoyaltyPoints = 0,
        CreatedDate = DateTime.Now.AddDays(-2)
    }
    ];

    public static List<Employee> Employees { get; } =
    [
        new Employee
        {
            EmployeeID = 1,
            FullName = "Tran Van Staff",
            Phone = "0911000001",
            Salary = 8000000,
            Position = "Sales Staff",
            Role = "Staff",
            CreatedDate = DateTime.Now.AddMonths(-6)
        },
        new Employee
        {
            EmployeeID = 2,
            FullName = "Pham Thi Manager",
            Phone = "0911000002",
            Salary = 15000000,
            Position = "Store Manager",
            Role = "Admin",
            CreatedDate = DateTime.Now.AddMonths(-12)
        },
        new Employee
        {
            EmployeeID = 3,
            FullName = "Hoang Van Support",
            Phone = "0911000003",
            Salary = 7000000,
            Position = "Support",
            Role = "Staff",
            CreatedDate = DateTime.Now.AddMonths(-3)
        }
    ];

    public static List<Order> Orders { get; } =
    [
        new Order
        {
            OrderID = 1,
            CustomerID = 1,
            EmployeeID = 1,
            OrderDate = DateTime.Now.AddDays(-5),
            SubtotalAmount = 140,
            DiscountAmount = 0,
            TaxAmount = 0,
            TotalAmount = 140,
            PaymentStatus = OrderStatus.Paid,
            PaymentMethod = "Cash",
            LoyaltyPointsEarned = 1
        },
        new Order
        {
            OrderID = 2,
            CustomerID = 2,
            EmployeeID = 1,
            OrderDate = DateTime.Now.AddDays(-3),
            SubtotalAmount = 156,
            DiscountAmount = 0,
            TaxAmount = 0,
            TotalAmount = 156,
            PaymentStatus = OrderStatus.Paid,
            PaymentMethod = "Cash",
            LoyaltyPointsEarned = 1
        },
        new Order
        {
            OrderID = 3,
            CustomerID = 3,
            EmployeeID = 2,
            OrderDate = DateTime.Now.AddDays(-1),
            SubtotalAmount = 50,
            DiscountAmount = 0,
            TaxAmount = 0,
            TotalAmount = 50,
            PaymentStatus = OrderStatus.Pending,
            PaymentMethod = "Cash"
        }
    ];

    public static List<OrderDetail> OrderDetails { get; } =
    [
        new OrderDetail { OrderDetailID = 1, OrderID = 1, BookID = 1, Quantity = 2, UnitPrice = 20, Subtotal = 40 },
        new OrderDetail { OrderDetailID = 2, OrderID = 1, BookID = 5, Quantity = 1, UnitPrice = 100, Subtotal = 100 },
        new OrderDetail { OrderDetailID = 3, OrderID = 2, BookID = 4, Quantity = 2, UnitPrice = 36, Subtotal = 72 },
        new OrderDetail { OrderDetailID = 4, OrderID = 2, BookID = 8, Quantity = 2, UnitPrice = 42, Subtotal = 84 },
        new OrderDetail { OrderDetailID = 5, OrderID = 3, BookID = 7, Quantity = 1, UnitPrice = 50, Subtotal = 50 }
    ];
}
