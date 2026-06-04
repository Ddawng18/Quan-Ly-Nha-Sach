# BookStoreApp

A **Point-of-Sale & Bookstore Management System** built with .NET 9 and Windows Forms.

---

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Architecture](#architecture)
- [Build & Run](#build--run)
- [Payment Processing](#payment-processing)
- [Configuration](#configuration)
- [Key Forms & Controls](#key-forms--controls)
- [Recent Changes](#recent-changes)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

BookStoreApp is a desktop application for managing a bookstore's daily operations. It supports inventory management, customer and employee records, point-of-sale transactions, invoice tracking, reporting & analytics, and **QR-based payment processing** (MoMo, VNPay, or Demo mode).

The application uses an **in-memory fake data store** (`FakeDatabase`) — no external database is required for development or demo purposes. All data access goes through repository interfaces, so swapping to SQL Server later requires only a new implementation class.

---

## Key Features

### Book Management
- Full CRUD with fields: ISBN, title, author(s), publisher, category, purchase/selling price, stock quantity
- Search by title, author, or ISBN
- Filter by category, publisher, stock level (In Stock / Low Stock / Out of Stock)

### Customer Management
- CRUD with purchase history linked to invoices
- Loyalty points: earn on purchase, redeem as discount at checkout
- Configurable earning rate (`0.01` points per VND) and redemption rate (`100` VND per point)

### Employee Management
- CRUD with roles: **Admin** (full access) and **Staff** (POS + inventory read-only)
- Role-based UI: unauthorized controls are hidden for Staff users

### Point of Sale (POS) — [`OrderCreateForm`](BookStoreApp/Forms/OrderCreateForm.cs)
- Shopping cart: add/remove books, adjust quantity
- Real-time stock validation (prevents overselling)
- Per-line and per-order discounts (percentage or fixed amount)
- Loyalty point redemption
- Tax calculation
- Two payment workflows:
  - **Cash** — saves order directly
  - **QR Payment** — dedicated button opens a QR code form for provider-based payment

### Invoice Management
- List view with date-range and status filters
- Detail view with line items, customer, employee, payment method, amounts
- Status transitions: Pending → Paid → Cancelled
- Export to CSV

### Reporting & Analytics
- Revenue by day / week / month (bar/line charts via OxyPlot)
- Top N best-selling books (configurable N, default 10)
- Low-stock alert with configurable threshold
- Slow-moving inventory report (90 days)

---

## Technology Stack

| Component | Technology |
|-----------|-----------|
| **Runtime** | .NET 9 |
| **UI Framework** | Windows Forms |
| **Language** | C# 13 |
| **Charting** | [OxyPlot.WindowsForms](https://www.nuget.org/packages/OxyPlot.WindowsForms) 2.2.0 |
| **QR Generation** | System.Drawing.Common 9.0.0 (demo provider only) |
| **Data Store** | In-memory (`FakeDatabase` — static collections) |
| **Testing** | xUnit + Moq (Phase 5 — planned) |
| **Payment APIs** | MoMo v2 sandbox, VNPay sandbox (HTTP + HMAC signatures) |

---

## Project Structure

```
BookStoreApp.sln
├── DTO/                          # Shared models & enums
│   ├── Books/                    # Book, BookViewDto, BookFilter, StockLevelFilter
│   ├── Customers/                # Customer, CustomerPurchaseDto, CustomerStatsDto
│   ├── Dashboard/                # DashboardMetricDto, BestSellingBookDto, RecentOrderDto
│   ├── Employees/                # Employee
│   ├── Orders/                   # OrderStatus, OrderViewDto, OrderDetailViewDto
│   ├── Order.cs, OrderDetail.cs  # Order & detail entities
│   ├── POS/                      # CartLine, CartTotals, CheckoutRequest, CheckoutResult, DiscountType
│   ├── Payments/                 # PaymentStatus, PaymentCreationResult, PaymentStatusResult, PaymentConfig
│   ├── Reports/                  # ReportRowDto, ReportSectionDto
│   ├── Common/                   # ValidationResult
│   ├── Category/, Supplier/      # Category & Supplier entities
│   └── BookStoreApp.DTO.csproj
│
├── DAL/                          # Data access layer
│   ├── Interfaces/               # IBookRepository, ICustomerRepository, IOrderRepository, ...
│   ├── Repositories/             # Fake in-memory implementations backed by FakeDatabase
│   └── BookStoreApp.DAL.csproj
│
├── BLL/                          # Business logic layer
│   ├── IBookService.cs, BookService.cs
│   ├── ICustomerService.cs, CustomerService.cs
│   ├── IEmployeeService.cs, EmployeeService.cs
│   ├── IOrderService.cs, OrderService.cs
│   ├── IPosService.cs, PosService.cs
│   ├── IReportService.cs, ReportService.cs
│   ├── ILoyaltyService.cs, LoyaltyService.cs, LoyaltySettings.cs
│   ├── IAuthService.cs, AuthService.cs
│   ├── Payments/                 # IPaymentProvider, DemoPaymentProvider, MomoPaymentProvider, VNPayPaymentProvider, PaymentProviderFactory
│   └── BookStoreApp.BLL.csproj
│
├── Utilities/                    # Cross-cutting utilities
│   ├── FakeDatabase.cs           # Active in-memory data store
│   ├── FileLogger.cs             # Rolling file logger (logs/bookstore-{date}.log)
│   └── BookStoreApp.Utilities.csproj
│
├── BookStoreApp/                 # WinForms UI
│   ├── Forms/                    # LoginForm, MainForm, BookEditForm, CustomerEditForm, EmployeeEditForm, OrderCreateForm, PaymentQRForm, ...
│   ├── UserControls/             # DashboardControl, BookControl, CustomersControl, EmployeesControl, OrdersControl, ReportsControl, ...
│   ├── Theme/                    # AppBranding, AppTheme
│   ├── ServiceLocator.cs         # Manual DI root — wires all services
│   ├── Program.cs                # Entry point + PaymentConfig loader
│   ├── appsettings.json          # Payment provider configuration
│   └── BookStoreApp.csproj
│
├── docs/                         # Documentation
│   ├── CODEBASE_ANALYSIS.md      # Full audit (Phase 1)
│   └── CHANGELOG.md              # Rolling change log
│
└── README.md                     # This file
```

---

## Architecture

```
┌─────────────────────────────────────────────────┐
│  WinForms UI (Forms + UserControls)              │
│  Gets services from ServiceLocator               │
├─────────────────────────────────────────────────┤
│  BLL (Service interfaces + implementations)      │
│  Business rules, validation, orchestration       │
├─────────────────────────────────────────────────┤
│  DAL (Repository interfaces + fake impls)        │
│  Data access — currently backed by FakeDatabase  │
├─────────────────────────────────────────────────┤
│  DTO (Shared models, enums, records)             │
├─────────────────────────────────────────────────┤
│  Utilities (FakeDatabase, FileLogger)            │
└─────────────────────────────────────────────────┘
```

**Dependency injection** is done via a manual `ServiceLocator` class in [`BookStoreApp/ServiceLocator.cs`](BookStoreApp/ServiceLocator.cs). All UI controls obtain their services from this single source. Services receive repository interfaces via constructor injection.

---

## Build & Run

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Windows 10/11 (WinForms requires Windows)

### Build

```bash
dotnet build BookStoreApp.sln
```

### Run

```bash
dotnet run --project BookStoreApp
```

Or launch `BookStoreApp.exe` from the output directory:

```bash
.\BookStoreApp\bin\Debug\net9.0-windows\BookStoreApp.exe
```

### Test Login Credentials

| Username | Password | Role |
|----------|----------|------|
| `admin` | `admin` | Admin (full access) |
| `staff` | `staff` | Staff (POS + read-only inventory) |

---

## Payment Processing

### Payment Flow

The POS form ([`OrderCreateForm`](BookStoreApp/Forms/OrderCreateForm.cs)) supports two payment workflows:

| Method | Button | Behavior |
|--------|--------|----------|
| **Cash** | `Create Order` | Validates cart, creates order, deducts stock immediately |
| **QR Payment** | `Pay with QR` | Validates cart → opens [`PaymentQRForm`](BookStoreApp/Forms/PaymentQRForm.cs) → displays QR code → polls provider → on confirmation, creates order |

### Payment Providers

| Provider | File | Notes |
|----------|------|-------|
| **Demo** | [`DemoPaymentProvider`](BLL/Payments/DemoPaymentProvider.cs) | Generates QR-like bitmap, auto-confirms after 10 seconds. Always available. |
| **MoMo** | [`MomoPaymentProvider`](BLL/Payments/MomoPaymentProvider.cs) | MoMo v2 sandbox API (HMAC-SHA256). Requires PartnerCode, AccessKey, SecretKey. |
| **VNPay** | [`VNPayPaymentProvider`](BLL/Payments/VNPayPaymentProvider.cs) | VNPay sandbox API (HMAC-SHA512). Requires TmnCode, HashSecret. |

Provider selection is handled by [`PaymentProviderFactory`](BLL/Payments/PaymentProviderFactory.cs):

```csharp
// Returns DemoPaymentProvider when credentials are empty
var provider = PaymentProviderFactory.Create(config);
```

### PaymentQRForm

The [`PaymentQRForm`](BookStoreApp/Forms/PaymentQRForm.cs) displays:
- QR code image (250×250 px, scaled to fit)
- Order ID and amount (formatted as `#,##0 ₫`)
- Provider name (Demo / MoMo / VNPay)
- 5-minute countdown timer (configurable)
- Status: Waiting → Confirmed / Expired / Cancelled
- **[Refresh QR]** — generates a new payment request
- **[Cancel]** — cancels polling

All UI updates are dispatched to the UI thread via `this.Invoke(...)`.

---

## Configuration

Payment settings are in [`BookStoreApp/appsettings.json`](BookStoreApp/appsettings.json) (copied to output directory on build):

```json
{
  "Payment": {
    "DefaultProvider": "Demo",
    "QrTimeoutSeconds": 300,
    "PollingIntervalSeconds": 5,
    "MoMo": {
      "PartnerCode": "",
      "AccessKey": "",
      "SecretKey": "",
      "BaseUrl": "https://test-payment.momo.vn/v2/gateway/api/"
    },
    "VNPay": {
      "TmnCode": "",
      "HashSecret": "",
      "BaseUrl": "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html"
    }
  }
}
```

- **`DefaultProvider`**: `"Demo"`, `"MoMo"`, or `"VNPay"`. When credentials are empty, the factory falls back to `DemoPaymentProvider`.
- **`QrTimeoutSeconds`**: How long the QR code remains valid (default 300 = 5 minutes).
- **`PollingIntervalSeconds`**: How often the app checks payment status (default 5).

The config is loaded at startup in [`Program.cs`](BookStoreApp/Program.cs):

```csharp
public static PaymentConfig PaymentConfig { get; } = LoadPaymentConfig();
```

---

## Key Forms & Controls

| Form / Control | File | Purpose |
|---------------|------|---------|
| `LoginForm` | [`Forms/LoginForm.cs`](BookStoreApp/Forms/LoginForm.cs) | Authentication with fake accounts |
| `MainForm` | [`Forms/MainForm.cs`](BookStoreApp/Forms/MainForm.cs) | Shell with sidebar navigation, role-based menu |
| `OrderCreateForm` | [`Forms/OrderCreateForm.cs`](BookStoreApp/Forms/OrderCreateForm.cs) | POS: cart, discounts, tax, loyalty, Cash & QR payment |
| `PaymentQRForm` | [`Forms/PaymentQRForm.cs`](BookStoreApp/Forms/PaymentQRForm.cs) | QR display, countdown, polling, cancel/refresh |
| `BookEditForm` | [`Forms/BookEditForm.cs`](BookStoreApp/Forms/BookEditForm.cs) | Add/edit book with inline ErrorProvider validation |
| `DashboardControl` | [`UserControls/DashboardControl.cs`](BookStoreApp/UserControls/DashboardControl.cs) | Metrics, recent orders, best sellers |
| `BookControl` | [`UserControls/BookControl.cs`](BookStoreApp/UserControls/BookControl.cs) | Book list with search, category/publisher/stock filters |
| `CustomersControl` | [`UserControls/CustomersControl.cs`](BookStoreApp/UserControls/CustomersControl.cs) | Customer list with loyalty points and purchase history |
| `OrdersControl` | [`UserControls/OrdersControl.cs`](BookStoreApp/UserControls/OrdersControl.cs) | Invoice list with status filter, detail view, status updates |
| `ReportsControl` | [`UserControls/ReportsControl.cs`](BookStoreApp/UserControls/ReportsControl.cs) | Charts (OxyPlot) + data tables, CSV export |

---

## Recent Changes

### 2026-06-04 — QR Payment UX Improvement

- **Added dedicated "Pay with QR" button** to the POS form ([`OrderCreateForm`](BookStoreApp/Forms/OrderCreateForm.cs:65)). QR payment is no longer hidden in a dropdown.
- **Simplified "Create Order" to cash-only** — the Method dropdown now contains only `"Cash"`.
- **Extracted `PrepareAndValidateCheckout()`** method shared by both payment buttons.
- **Removed async event handler warnings** and unused `IsCashPayment` helper.
- **Build**: 0 errors, 0 warnings.

See [`docs/CHANGELOG.md`](docs/CHANGELOG.md) for the full history (Phases 1–4).

---

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make changes; ensure `dotnet build BookStoreApp.sln` passes with 0 errors
4. Add tests in `tests/BookstoreApp.Tests/` (xUnit + Moq)
5. Submit a pull request

### Code Style

- C# services go through interfaces (`IBookService`, `ICustomerService`, etc.)
- UI controls obtain dependencies from `ServiceLocator`, never `new XxxService()`
- All I/O operations use `async/await`; only event handlers use `async void`
- Validation returns `ValidationResult` (never throws for business rule violations)
- Logging goes through `Utilities.FileLogger`

---

## License

This project is intended for academic/demo purposes. No license is currently specified.
