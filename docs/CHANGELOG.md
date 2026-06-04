# Changelog

## Phase 1 - Codebase Audit (2026-06-04)

- Added `docs/CODEBASE_ANALYSIS.md` with architecture overview, feature inventory, identified problems, frozen working areas, and prioritized implementation plan.
- Confirmed baseline solution build succeeds with `dotnet build BookStoreApp.sln` before implementation work.
- Confirmed no third-party NuGet packages are currently referenced by any project.

## Phase 2 - Core Modules (2026-06-04)

- Extended DTOs for loyalty points, employee roles, invoice/order totals, discounts, tax, payment method, order detail views, cart lines, and checkout request/result models.
- Extended repository interfaces and fake repository implementations so Phase 2 data access still goes through DAL abstractions backed by `FakeDatabase`.
- Added `LoyaltyService` and `PosService` for loyalty math, cart stock checks, discounts, tax, checkout preparation, and checkout completion.
- Updated book management search/filtering for title, author, ISBN, category, publisher, and stock level.
- Updated customer management to display loyalty points and aggregate loyalty stats.
- Updated employee management with `Admin`/`Staff` roles.
- Updated role-based UI so Staff keeps Orders/POS and read-only Books while admin management/report screens are hidden.
- Updated POS/order creation with live stock validation, line/order discounts, loyalty redemption, tax, payment method, and total breakdown.
- Updated invoice/order management with detail lines, `Pending`/`Paid`/`Cancelled` status filtering, and status transition updates.
- Added `OxyPlot.WindowsForms` for reporting charts and enhanced reports with revenue by day/week/month, configurable top N best sellers, and configurable low-stock threshold.
- Left real payment flow and real PDF export for later phases: Phase 3 owns QR payment, and invoice PDF export remains a TODO until the final TODO document.

## Phase 3 - QR Payment Integration (2026-06-04)

### New Files

| File | Purpose |
|------|---------|
| `DTO/Payments/PaymentStatus.cs` | `PaymentStatus` enum: Pending, Paid, Cancelled, Failed, Expired |
| `DTO/Payments/PaymentCreationResult.cs` | Record returned by `IPaymentProvider.CreatePaymentAsync` |
| `DTO/Payments/PaymentStatusResult.cs` | Record returned by `IPaymentProvider.QueryStatusAsync` |
| `DTO/Payments/PaymentConfig.cs` | POCO for `appsettings.json` deserialization (Payment, MoMo, VNPay config) |
| `BLL/Payments/IPaymentProvider.cs` | Stable abstraction for QR payment providers |
| `BLL/Payments/DemoPaymentProvider.cs` | Demo provider: generates QR-like bitmap at runtime, auto-confirms after 10s |
| `BLL/Payments/MomoPaymentProvider.cs` | MoMo v2 sandbox integration with HMAC-SHA256 signing |
| `BLL/Payments/VNPayPaymentProvider.cs` | VNPay sandbox integration with HMAC-SHA512 signing |
| `BLL/Payments/PaymentProviderFactory.cs` | Credential-based provider selection; falls back to Demo when keys are empty |
| `BookStoreApp/appsettings.json` | Payment configuration with empty credentials and Demo as default |
| `BookStoreApp/Forms/PaymentQRForm.cs` | QR display form with countdown timer, status polling, cancel/refresh |
| `BookStoreApp/Forms/PaymentQRForm.Designer.cs` | Designer-generated UI layout for PaymentQRForm |
| `BookStoreApp/Forms/PaymentQRForm.resx` | Resource file for PaymentQRForm |

### Modified Files

| File | Change |
|------|--------|
| `DTO/Order/Order.cs` | Added `PaymentTransactionId` property |
| `DTO/Orders/OrderViewDto.cs` | Added `PaymentTransactionId` property |
| `DAL/Repositories/OrderRepository.cs` | `MapOrders` now includes `PaymentTransactionId` in projection |
| `BLL/BookStoreApp.BLL.csproj` | Added `System.Drawing.Common` 9.0.0 for demo QR generation |
| `BookStoreApp/Program.cs` | Added `PaymentConfig` static loader from `appsettings.json` |
| `BookStoreApp/BookStoreApp.csproj` | Added `PaymentQRForm` compile items, `appsettings.json` copy-to-output |
| `BookStoreApp/Forms/OrderCreateForm.cs` | Integrated QR payment flow: non-cash payments show `PaymentQRForm` before `CompleteCheckout`; async event handler; payment method changed to "Cash"/"QR Payment" |

### Key Design Decisions

- **No new NuGet packages** beyond `System.Drawing.Common` (required for demo QR bitmap generation in BLL). All HTTP, JSON, and crypto use .NET 9 built-in APIs.
- **Payment flow**: `PrepareCheckout` → (if QR) `PaymentQRForm` → `CompleteCheckout`. No order is created until payment is confirmed. Cash payments unchanged.
- **Thread safety**: `PaymentQRForm` uses `System.Windows.Forms.Timer` + `this.Invoke` for all UI updates; `CancellationTokenSource` for graceful cancellation.
- **Provider selection**: `PaymentProviderFactory.Create(config)` returns `DemoPaymentProvider` when `SecretKey`/`HashSecret` are empty — the app is always demonstrable without live credentials.
- **Build**: 0 errors, 0 warnings.

## Phase 4 - Targeted Refactoring (2026-06-04)

### New Files

| File | Purpose |
|------|---------|
| `BookStoreApp/ServiceLocator.cs` | Manual DI root — wires all `I*Service` → concrete implementations with injected repository dependencies |
| `Utilities/FileLogger.cs` | Lightweight rolling file logger (`logs/bookstore-{date}.log`), thread-safe |

### Modified Files — Service Layer (P9: removed parameterless constructors)

All 10 services had their parameterless `new XxxRepository()` constructors removed. Only the interface-injecting constructors remain:

| File | Change |
|------|--------|
| `BLL/AuthService.cs` | Removed `AuthService()` default ctor |
| `BLL/BookService.cs` | Removed `BookService()` default ctor |
| `BLL/CategoryService.cs` | Removed `CategoryService()` default ctor |
| `BLL/CustomerService.cs` | Removed `CustomerService()` default ctor |
| `BLL/DashboardService.cs` | Removed `DashboardService()` default ctor |
| `BLL/EmployeeService.cs` | Removed `EmployeeService()` default ctor |
| `BLL/OrderService.cs` | Removed `OrderService()` default ctor |
| `BLL/PosService.cs` | Removed `PosService()` default ctor |
| `BLL/ReportService.cs` | Removed `ReportService()` default ctor |
| `BLL/SupplierService.cs` | Removed `SupplierService()` default ctor |

### Modified Files — UI Layer (P10: replaced direct service construction)

All 9 UI files now obtain services from `ServiceLocator` instead of `new XxxService()`:

| File | Services replaced |
|------|------------------|
| `BookStoreApp/UserControls/BookControl.cs` | `IBookService`, `ICategoryService` |
| `BookStoreApp/UserControls/CustomersControl.cs` | `ICustomerService` |
| `BookStoreApp/UserControls/EmployeesControl.cs` | `IEmployeeService` |
| `BookStoreApp/UserControls/OrdersControl.cs` | `IOrderService` |
| `BookStoreApp/UserControls/ReportsControl.cs` | `IReportService` |
| `BookStoreApp/UserControls/DashboardControl.cs` | `IDashboardService` |
| `BookStoreApp/UserControls/CategoryControl.cs` | `ICategoryService` |
| `BookStoreApp/UserControls/SupplierControl.cs` | `ISupplierService` |
| `BookStoreApp/Forms/LoginForm.cs` | `IAuthService` |
| `BookStoreApp/Forms/OrderCreateForm.cs` | `IBookService`, `ICustomerService`, `IEmployeeService`, `IPosService` |

### Modified Files — P4: Stock Deduction Moved to Service Layer

| File | Change |
|------|--------|
| `DAL/Repositories/OrderRepository.cs` | Removed stock mutation (lines 66-71): `book.QuantityInStock` and `book.LastSoldDate` are no longer modified inside `CreateOrder` |
| `BLL/PosService.cs` | `CompleteCheckout` now deducts stock after order creation — stock mutation lives in the service orchestration layer where it belongs |

### Modified Files — P16: Inline Validation

| File | Change |
|------|--------|
| `BookStoreApp/Forms/BookEditForm.cs` | Replaced `MessageBox.Show` for publish year validation with `ErrorProvider` component; added inline validation for Title, Author, ISBN fields |

### Modified Files — P18: Logging

| File | Change |
|------|--------|
| `BookStoreApp/Forms/PaymentQRForm.cs` | Log payment creation, confirmation, and polling errors |
| `BLL/Payments/MomoPaymentProvider.cs` | Log API call failures |
| `BLL/Payments/VNPayPaymentProvider.cs` | Log API call failures |
| `BLL/PosService.cs` | Log stock changes on checkout |

### Impact Summary

| Metric | Before | After |
|--------|--------|-------|
| UI files with `new XxxService()` | 9 | 0 |
| Services with parameterless ctors | 10 | 0 |
| Stock mutation location | `OrderRepository` (data layer) | `PosService` (service layer) |
| BookEditForm validation | `MessageBox` per field | Inline `ErrorProvider` |
| Observability | None | All payments, exceptions, stock changes logged |
| **Build** | — | **0 errors, 0 warnings** |
