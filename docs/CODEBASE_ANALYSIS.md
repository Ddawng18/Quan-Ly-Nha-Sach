# Codebase Analysis

Phase 1 audit date: 2026-06-04

## 1. Architecture Overview

### Architectural Style

The solution is a simple layered WinForms application:

- `BookStoreApp` is the UI layer. Forms and user controls own UI events, data binding, navigation, and most user-facing validation messages.
- `BookStoreApp.BLL` is a service layer. Services validate basic input and call repository interfaces.
- `BookStoreApp.DAL` is a repository layer. Repository interfaces exist for the current fake/in-memory data source.
- `BookStoreApp.DTO` contains mutable DTO/entity classes used across all layers.
- `BookStoreApp.Utilities` contains the active in-memory fake data store and report export helpers.

The style is layered, but still partly monolithic in the UI: forms instantiate concrete services directly, and several workflows keep business rules in form event handlers.

### Project And Folder Structure

| Path | Purpose |
|------|---------|
| `BookStoreApp.sln` | Solution containing UI, BLL, DAL, DTO, and Utilities projects. |
| `BookStoreApp/` | .NET 9 WinForms executable project. |
| `BookStoreApp/Forms/` | Login, main shell, edit dialogs, and order creation form. |
| `BookStoreApp/UserControls/` | Feature screens for dashboard, books, customers, employees, orders, reports, categories, suppliers. |
| `BookStoreApp/Theme/` | Branding and reusable UI styling helpers. |
| `BookStoreApp/Assets/` | Application icon and logo. |
| `BLL/` | Service interfaces and service implementations. |
| `DAL/Interfaces/` | Repository contracts for account, book, category, customer, dashboard, employee, order, report, supplier. |
| `DAL/Repositories/` | Fake/in-memory repository implementations backed by `FakeDatabase`. |
| `DTO/` | Shared DTO/entity classes for account, books, customers, dashboard, employees, orders, reports, suppliers, validation. |
| `Utilities/FakeDatabase.cs` | Active in-memory data store. This must be preserved for now. |
| `Utilities/ReportExporter.cs` | CSV/text export helper. |

### Data Flow

Current intended flow:

```text
WinForms UI -> BLL service interface -> DAL repository interface -> FakeDatabase static collections
```

Examples:

- `BookControl` calls `IBookService`, implemented by `BookService`, which calls `IBookRepository`, implemented by `BookRepository`, which reads/writes `FakeDatabase.Books`.
- `OrdersControl` calls `IOrderService`, implemented by `OrderService`, which uses `IOrderRepository` and `IBookRepository`; `OrderRepository.CreateOrder` creates order rows and deducts stock.

Important deviations:

- UI controls construct concrete services directly instead of receiving them by constructor injection.
- `BookService.MapToView` reads `FakeDatabase.Categories` directly for category names.
- Dashboard/report repositories perform cross-collection joins directly against `FakeDatabase`. This is acceptable for the active fake implementation, but these queries should remain behind repository interfaces.

### Framework And NuGet Baseline

- UI target framework: `net9.0-windows`, WinForms enabled.
- BLL/DAL/DTO/Utilities target framework: `net9.0`.
- Existing NuGet packages: none. No `PackageReference` entries are present in any project.
- Baseline build: `dotnet build BookStoreApp.sln` succeeds with 0 warnings and 0 errors.

## 2. Feature Inventory

| Module | Implemented? | Complete? | Notes |
|--------|-------------|-----------|-------|
| Authentication | Yes | Partial | Login exists using fake accounts. Roles are string values `"Manager"` and `"Staff"`, not required `Admin`/`Staff`. Passwords are plain text in fake data. |
| Main navigation | Yes | Partial | Staff hides Employees only. Staff can still access Books, Categories, Suppliers, Customers, Orders, Reports. |
| Dashboard | Yes | Partial | Shows metrics, recent orders, best sellers from fake data. No charting. |
| Book Management | Yes | Partial | CRUD exists. Fields include ISBN, title, author, publisher, category ID, supplier ID, import/sell price, stock. Search only checks title. No category/publisher/stock filters. DataGridView column sort may work via grid behavior but no explicit sort service. |
| Category Management | Yes | Mostly | CRUD/search exists. Delete prevents category removal while books reference it. |
| Supplier Management | Yes | Mostly | CRUD/search exists. Delete prevents supplier removal while books reference it. |
| Customer Management | Yes | Partial | CRUD, stats, and purchase history exist. No loyalty points or earning/redemption rules. |
| Employee Management | Yes | Partial | CRUD exists for full name, phone, position, salary. No employee role field; role-based UI is account-role based and incomplete. |
| POS / Order Creation | Yes | Incomplete | `OrderCreateForm` can add/remove line items and save an order. It lacks quantity adjustment after add, immediate aggregate stock guard, discounts, tax, loyalty redemption, and payment flow. |
| Invoice Management | Partial | Incomplete | Orders are displayed as invoice-like records with date/status filters. No detail view, no line item view, no payment method, no `Cancelled` status, no status transition workflow, no PDF invoice print/export. |
| Reporting & Analytics | Yes | Partial | Revenue summary, best-selling books, low stock, slow moving report tables exist. No day/week/month chart, no configurable top N in UI, no charting library installed. |
| Payment Integration | No | No | No `Payments` namespace, provider abstraction, MoMo/VNPay/demo provider, config, QR UI, or polling. |
| Configuration | No | No | No `appsettings.json` or configuration loader exists. |
| Logging | No | No | No logging infrastructure. |
| Tests | No | No | No tests project, xUnit, or Moq. |

## 3. Problems Identified

### P1. Payment Flow Is Missing

- Location: `BookStoreApp/Forms/OrderCreateForm.cs` lines 93-126
- Category: missing integration / missing workflow
- Severity: High
- Recommended fix: Introduce `IPaymentProvider` and `PaymentQRForm`, then change checkout/order save so payment is confirmed before generating a paid invoice. Use `DemoPaymentProvider` when credentials are absent.

### P2. POS Business Rules Live In Form Code

- Location: `BookStoreApp/Forms/OrderCreateForm.cs` lines 39-90 and 93-118
- Category: UI+logic mix / missing service layer
- Severity: High
- Recommended fix: Extract cart operations, stock checks, discounts, tax, loyalty redemption, and totals into a `PosService` or checkout service. Keep the form responsible for input/display only.

### P3. Adding Existing Cart Lines Can Exceed Stock Until Save

- Location: `BookStoreApp/Forms/OrderCreateForm.cs` lines 54-59; `BLL/OrderService.cs` lines 68-85
- Category: missing validation / delayed validation
- Severity: High
- Recommended fix: Validate requested quantity plus existing cart quantity against available stock when adding/updating cart lines. Keep final validation in `OrderService`.

### P4. Stock Mutation Happens Inside Repository Create

- Location: `DAL/Repositories/OrderRepository.cs` lines 27-52
- Category: business logic in data layer / hidden side effect
- Severity: High
- Recommended fix: Move stock deduction orchestration into a service transaction boundary. Repository should persist order/detail changes and expose explicit stock update methods, or the service should call a book inventory service/repository method.

### P5. Invoice Domain Is Collapsed Into Order DTO

- Location: `DTO/Order/Order.cs` lines 3-10; `DTO/OrderDetail/OrderDetail.cs` lines 3-10; `DAL/Repositories/OrderRepository.cs` lines 55-69
- Category: incomplete domain model
- Severity: High
- Recommended fix: Add invoice/payment fields required by the specification: status enum or constrained status values, payment method/provider, discount, tax, subtotal, grand total, transaction ID, and detail retrieval.

### P6. Customer Loyalty Is Not Modeled

- Location: `DTO/Customers/Customer.cs` lines 3-10; `BLL/CustomerService.cs` lines 21-80
- Category: missing domain model / missing business rules
- Severity: High
- Recommended fix: Add loyalty point storage and a `LoyaltyService` with configurable earning/redemption constants. Link point accrual/redemption to invoice checkout.

### P7. Required Admin Role Does Not Exist

- Location: `Utilities/FakeDatabase.cs` lines 7-27; `BookStoreApp/Forms/MainForm.cs` lines 10-28
- Category: authorization mismatch
- Severity: High
- Recommended fix: Normalize roles to required `Admin` and `Staff`. Apply role checks consistently in navigation and feature controls.

### P8. Staff Role Restrictions Are Incomplete

- Location: `BookStoreApp/Forms/MainForm.cs` lines 22-25 and 59-81
- Category: authorization / role-based UI gap
- Severity: High
- Recommended fix: Hide or disable all non-staff-permitted controls. Staff should have POS plus inventory read-only access, not full CRUD access to books/categories/suppliers/customers/reports.

### P9. Services Instantiate Concrete Repositories By Default

- Location: `BLL/BookService.cs` lines 12-20; similar constructors in other services
- Category: dependency coupling
- Severity: Medium
- Recommended fix: Preserve constructor overloads if needed, but introduce a composition root in `Program.cs` or a simple service locator so UI code can request interfaces. Continue supporting fake repositories until SQL Server implementation is added later.

### P10. UI Instantiates Concrete Services Directly

- Location: `BookStoreApp/UserControls/BookControl.cs` line 10; `CustomersControl.cs` line 10; `EmployeesControl.cs` line 10; `OrdersControl.cs` line 10; `ReportsControl.cs` line 11; `BookStoreApp/Forms/LoginForm.cs` line 8; `OrderCreateForm.cs` lines 8-10 and 117
- Category: tight coupling / testability
- Severity: Medium
- Recommended fix: Add constructor injection or a manual DI/service locator root. Avoid creating a second `OrderService` inside `OrderCreateForm.btnSave_Click`.

### P11. Book Search Does Not Meet Specification

- Location: `BLL/BookService.cs` lines 24-35
- Category: incomplete feature
- Severity: Medium
- Recommended fix: Search title, author, and ISBN. Add filter methods or filter DTOs for category, publisher, and stock level.

### P12. Book View Mapping Reads FakeDatabase Outside Repository

- Location: `BLL/BookService.cs` lines 45-64
- Category: layer leakage / repository bypass
- Severity: Medium
- Recommended fix: Move book view projection into a repository method or inject category repository/service into `BookService`.

### P13. Report Export Buttons Are Misleading

- Location: `BookStoreApp/UserControls/ReportsControl.cs` lines 80-84; `Utilities/ReportExporter.cs` lines 21-31
- Category: misleading UI / incomplete export
- Severity: Medium
- Recommended fix: Rename PDF export to text export or implement real PDF export later. Excel button currently writes CSV and should be labeled/exported honestly.

### P14. Reporting Lacks Required Charts And Configurable Top N

- Location: `BookStoreApp/UserControls/ReportsControl.cs` lines 13-19 and 55-76; `DAL/Repositories/ReportRepository.cs` lines 40-73
- Category: incomplete analytics
- Severity: Medium
- Recommended fix: Add a charting package only after confirming none exists. Candidate packages: `LiveCharts2` or `OxyPlot.WindowsForms`. Add configurable top N with default 10.

### P15. Status Values Are Free-Form Strings

- Location: `DTO/Order/Order.cs` line 10; `BookStoreApp/Forms/OrderCreateForm.cs` lines 29-31 and 102-107; `BookStoreApp/UserControls/OrdersControl.cs` lines 17-18 and 49-52
- Category: validation / domain consistency
- Severity: Medium
- Recommended fix: Add a shared status enum or constants for `Pending`, `Paid`, `Cancelled`. Enforce allowed transitions.

### P16. Validation Uses MessageBox Per Field

- Location: `BookStoreApp/Forms/BookEditForm.cs` lines 31-40; many user controls show service validation with `MessageBox`
- Category: validation UX
- Severity: Low
- Recommended fix: For Phase 4, move field-level validation to inline labels or `ErrorProvider`. Use `MessageBox` only for confirmations and non-field failures.

### P17. Fake Data Contains Mojibake In Some Titles/Authors

- Location: `Utilities/FakeDatabase.cs` lines 142, 171, 325, 339
- Category: data quality
- Severity: Low
- Recommended fix: Keep fake store intact unless specifically approved. Fixing sample text is safe later but not required for payment/core behavior.

### P18. No Logging Around Exceptions, Payments, Or Stock Changes

- Location: `BookStoreApp/UserControls/ReportsControl.cs` lines 99-107; `DAL/Repositories/OrderRepository.cs` lines 46-50
- Category: missing observability
- Severity: Medium
- Recommended fix: Add lightweight logging in Phase 4. Log exceptions, payment transactions, and stock changes.

## 4. What Must Not Be Touched

The following working/stable features should be treated as frozen unless a later phase requires a targeted change:

- `FakeDatabase` remains the active data store and must not be replaced or removed.
- Login with fake accounts should continue to work.
- Main shell navigation and theme/branding should remain intact.
- Existing CRUD flows for books, customers, employees, categories, and suppliers should not be rewritten.
- Category and supplier deletion guards should be preserved.
- Customer purchase history view should remain available and later be extended, not removed.
- Existing order list filters by date/status/search should remain available.
- Existing report table views and CSV/text export should remain available until replaced by equivalent or better functionality.
- Existing project structure and public service/repository method signatures should be preserved unless a documented Phase 1 problem requires extension.

## 5. Prioritized Implementation Plan

### Phase 2 Dependency Order

1. Extend DTO/domain contracts for order/invoice totals, status, payment method/provider, discounts, tax, loyalty points, employee role, and payment transaction metadata.
2. Add or extend repository interfaces so all new persistence continues to go through DAL abstractions backed by `FakeDatabase`.
3. Implement `LoyaltyService` with configurable earning and redemption constants.
4. Implement `PosService` for cart, stock validation, discounts, tax, loyalty redemption, and checkout totals.
5. Update book management search/filter behavior with minimal UI changes.
6. Complete customer management loyalty display/history integration.
7. Complete employee roles and enforce role-based UI access.
8. Complete invoice/order detail, status filtering, status transitions, and optional print/export TODO.
9. Complete reporting charts/top N/low-stock threshold after confirming or adding one charting package.

### Phase 3 Risk Isolation

1. Add configuration model and `appsettings.json` with empty credentials.
2. Add `Payments` namespace and the stable `IPaymentProvider` contract.
3. Add `DemoPaymentProvider` first so the app remains demonstrable without credentials.
4. Add `PaymentQRForm` using the provider abstraction and UI-thread-safe polling updates.
5. Integrate payment into POS checkout before invoice creation.
6. Add MoMo and VNPay providers behind configuration selection. Keep provider-specific code isolated.
7. Add payment logs and transaction persistence through repository interfaces.

### Phase 4 Targeted Refactoring

1. Introduce manual DI/composition root or service locator in `Program.cs`.
2. Replace direct UI service construction with injected services where touched by Phase 2/3.
3. Move stock update and total calculation out of repositories/forms and into services.
4. Add inline validation for touched forms.
5. Add lightweight logging for payment, exceptions, and stock changes.

### Phase 5 Tests

1. Add `/tests/BookstoreApp.Tests/` using xUnit and Moq.
2. Unit-test service math and stock behavior against repository abstractions.
3. Unit-test payment signatures with mocked HTTP clients; no real API calls.
4. Document setup, config, sandbox credential placement, known risks, and remaining TODOs.
