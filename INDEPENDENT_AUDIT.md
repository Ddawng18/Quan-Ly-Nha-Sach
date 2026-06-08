# Independent Desktop Application Audit — BookStore Management System

**Auditor:** Zoo (Independent Examiner)  
**Date:** 2026-06-07  
**Project:** Quan-Ly-Nha-Sach — BookStore Management System  
**Platform:** .NET 9 Windows Forms (C#), SQL Server (ADO.NET)  
**Team:** Nguyễn Huỳnh Đăng, Lê Công Bảo, Trần Đại Phát, Nguyễn Lâm Sỹ Phú, Bành Phát Thịnh  
**Course:** Developing Desktop Applications — University Group Project

---

## Audit Methodology

Every file in the workspace was inspected. The report (`Desktop.pdf`) was cross-referenced against the source code line-by-line for every major claim. The database script, all BLL/DAL/DTO layers, all Forms/UserControls, payment providers, utilities, configuration files, and documentation were read in full. This audit starts from zero — no prior review is assumed.

---

# Overall Judgment

**Project type:** Desktop bookstore management application using layered architecture (DTO/DAL/BLL/UI) with Windows Forms, ADO.NET, and QR payment integration.

**Verdict:** A substantially complete, well-structured project that demonstrates solid understanding of layered architecture, SQL Server integration, and desktop UI design. However, **one critical data-integrity bug** (stock never persisted after sale) and **plaintext password storage** are show-stopping issues that prevent production readiness. The report overstates several claims (10 tables vs 8 actual, password hashing, real PDF export) and lacks actual screenshots. The codebase is otherwise clean, maintainable, and demonstrates genuine effort across a broad feature set.

| Component | Score | Max |
|-----------|-------|-----|
| Program / Demo | **2.5** | 4 |
| Report | **2.5** | 4 |
| **Total** | **5.0** | 8 |

**Biggest strengths:**
1. Clean 4-layer architecture with proper interface segregation
2. Complete POS system with cart, discounts, tax, loyalty points
3. Full MoMo/VNPay payment provider implementations with proper HMAC signatures
4. Transaction support in order creation with rollback
5. Consistent coding style and naming conventions throughout

**Biggest weaknesses:**
1. **CRITICAL: Stock deduction is never persisted to database** (in-memory only)
2. **Passwords stored and compared in plaintext** — no hashing whatsoever
3. Report inflates table count (claims 10, has 8) and misrepresents PDF export
4. No screenshots in report — only placeholder references
5. No duplicate ISBN validation before DB insert

**Most urgent fixes:**
1. Call `_bookRepository.UpdateStock()` in `PosService.CompleteCheckout()` at [`BLL/PosService.cs:184`](BLL/PosService.cs:184)
2. Hash passwords with BCrypt or SHA256 at minimum
3. Add ISBN uniqueness check in `BookService.AddBook()` at [`BLL/BookService.cs:80`](BLL/BookService.cs:80)

---

# Three Lecturer Mode Grades

## 🔴 Strict Mode (10/10 scale — Production-minded)

| Component | Score | Max |
|-----------|-------|-----|
| Program / Demo | **2.0** | 4 |
| Report | **1.5** | 4 |
| **Total** | **3.5** | 8 |

**Reasoning:** Under strict production standards, the stock-persistence bug is catastrophic — it means the application cannot fulfill its core purpose of inventory management. Plaintext passwords violate basic security requirements. The report makes demonstrably false claims (10 tables, password hashing, PDF export). No screenshots in the report. The fake PDF export (box-drawing characters saved as `.pdf`) is misleading. Multiple empty catch blocks swallow errors silently. The architecture, while clean in principle, has a Service Locator anti-pattern instead of proper DI. Search loads entire tables into memory. No connection pooling configuration. These flaws collectively make the application unfit for production deployment without significant rework.

**Main deductions:**
- Stock persistence bug: −1.0 from Program
- Plaintext passwords: −0.5 from Program
- Report–code fabrication (table count, hashing, PDF): −1.5 from Report
- No screenshots: −0.5 from Report
- Search scalability, empty catch blocks, Service Locator: −0.5 from Program

---

## 🟡 Normal Mode (7/10 scale — Realistic university grading)

| Component | Score | Max |
|-----------|-------|-----|
| Program / Demo | **3.0** | 4 |
| Report | **2.5** | 4 |
| **Total** | **5.5** | 8 |

**Reasoning:** This is realistic university-level grading that acknowledges genuine effort and partial implementations. The application has a solid layered architecture, working CRUD across all entities, a functional POS with discount/tax/loyalty logic, real payment provider integrations, role-based access control, and reporting with chart visualization. The critical stock bug is penalized but the rest of the POS workflow is correct. The report is well-structured and covers requirements, design, and use cases adequately, but loses marks for unsupported claims and missing screenshots. The database schema is properly normalized with foreign keys and constraints. The code demonstrates understanding of interfaces, repositories, validation patterns, and async/await. For a university group project, this shows substantial technical competence.

**Main deductions:**
- Stock persistence bug: −0.5 from Program
- Plaintext passwords: −0.25 from Program
- Report fabrications (10 tables, hashing claims): −0.75 from Report
- No screenshots: −0.5 from Report
- Search loads all data, Service Locator pattern: −0.25 from Program
- Hard DELETE on Customer/Employee (inconsistent with Book soft-delete): −0.25 from Report (design inconsistency)

---

## 🟢 Easy Mode (5/10 scale — Forgiving)

| Component | Score | Max |
|-----------|-------|-----|
| Program / Demo | **3.5** | 4 |
| Report | **3.0** | 4 |
| **Total** | **6.5** | 8 |

**Reasoning:** Under forgiving grading, the breadth of implemented features earns significant credit. The team tackled: authentication, book/category/supplier CRUD, customer management with loyalty, full POS workflow, order management with status transitions, 7 report types with OxyPlot charts, CSV/Excel/text export, QR payment with MoMo/VNPay/Demo providers, role-based sidebar hiding, theming system, file logging, and a clean project structure. The report is comprehensive in scope (requirements analysis, use cases, system design, database design). The code compiles and the database script is executable. The critical stock bug is noted but heavily discounted given the overall feature completeness and code quality. For a 5-person university team, this represents strong effort.

**Main deductions:**
- Stock persistence bug: −0.25 from Program
- Plaintext passwords: −0.25 from Program
- Report inaccuracies: −0.5 from Report
- Missing screenshots: −0.5 from Report
- Minor architecture concerns: −0.25 from Program

---

# Program Review

## Architecture

**Identified pattern:** Layered Architecture (4-tier: DTO → DAL → BLL → UI) with Repository and Service patterns.

```
┌──────────────────────────────────────┐
│  UI Layer (WinForms)                 │  BookStoreApp/
│  Forms, UserControls, ServiceLocator │
├──────────────────────────────────────┤
│  BLL (Business Logic Layer)          │  BLL/
│  Services, Validators, Payments      │
├──────────────────────────────────────┤
│  DAL (Data Access Layer)             │  DAL/
│  Repositories, DbConnectionFactory   │
├──────────────────────────────────────┤
│  DTO (Data Transfer Objects)         │  DTO/
│  Models, Enums, ValidationResult     │
├──────────────────────────────────────┤
│  Utilities                          │  Utilities/
│  FileLogger, ReportExporter          │
└──────────────────────────────────────┘
```

**Fitness for purpose:** The layered architecture is entirely appropriate for a desktop bookstore application of this scope. The separation of concerns is genuine — repositories handle only data access, services contain business logic and validation, and the UI delegates to services. This is production-appropriate architecture.

**Dependency Injection:** Implemented via a manual `ServiceLocator` static class ([`BookStoreApp/ServiceLocator.cs`](BookStoreApp/ServiceLocator.cs)). All dependencies are wired in the static constructor as singletons. While functional, this is the **Service Locator anti-pattern** rather than true Dependency Injection. All UI components have hard references to `ServiceLocator.*` static properties. This makes unit testing difficult (no constructor injection for mocks) and couples every consumer to the static locator. For a project of this scale, it's acceptable but not ideal.

**Rating:** 7/10 (clean layering, good interface segregation, but Service Locator pattern and lack of testability hold it back)

---

## Desktop Application Design

**Startup flow:** `Program.cs` → reads `appsettings.json` → configures `DbConnectionFactory` → launches `LoginForm` → on success, opens `MainForm` with role parameter.

**Navigation:** `MainForm` uses a sidebar of buttons. Each button click creates a **new** instance of the corresponding `UserControl` and loads it into `panelContent`. This means:
- State is **lost on every navigation** (no caching of filter state, scroll position, etc.)
- New database queries run on every tab switch
- No `Dispose()` is called on the replaced control, creating potential memory leaks

**Form communication:** Uses `ShowDialog()` for modal workflows (BookEditForm, PosForm, PaymentQRForm). `DialogResult` is used for success/failure signaling. This is correct WinForms practice.

**State handling:** Limited. The `MainForm` constructor takes a `role` string. Role-based visibility is handled by hiding sidebar buttons. `BookControl` receives a `readOnly` boolean parameter. No session timeout, no user context object, no activity tracking.

**Multi-form management:** `LoginForm` hides itself and shows `MainForm`. On logout, `MainForm` hides and shows a new `LoginForm`. Both register `FormClosed` events on each other to ensure the application exits. This is functional but fragile — two hidden forms can accumulate if logout/login cycles repeat.

**Rating:** 6/10 (functional navigation, correct modal patterns, but state loss on navigation, potential memory issues, fragile form lifecycle)

---

## Database Design

**Schema (8 tables):** `Categories`, `Suppliers`, `Books`, `Customers`, `Employees`, `Accounts`, `Orders`, `OrderDetails`

**Normalization assessment:**
- 3NF compliant: no transitive dependencies, all non-key attributes depend on the primary key
- Foreign keys properly defined: `Books.CategoryID`, `Books.SupplierID`, `Orders.CustomerID`, `Orders.EmployeeID`, `OrderDetails.OrderID`, `OrderDetails.BookID`, `Accounts.EmployeeID`
- UNIQUE constraint on `Books.ISBN` — correct
- Soft delete via `Books.IsDeleted` bit flag — good design choice
- `DEFAULT` constraints used appropriately (e.g., `LoyaltyPoints DEFAULT 0`, `IsDeleted DEFAULT 0`)

**Design issues:**
1. **Report claims 10 tables; actual schema has 8.** The report mentions `ImportReceipt` and `ImportDetail` tables ([`Desktop.pdf:281-283`](Desktop.pdf:281-283)) but these do not exist in `database.sql`. This is a report–code mismatch.
2. `Accounts.Password` stores plaintext values ('1', '2' in seed data) — column is named `Password` not `PasswordHash` as the report claims.
3. `Accounts.EmployeeID` is nullable (INT REFERENCES) but the seed data inserts Accounts without EmployeeID (the first two accounts have no EmployeeID). The relationship is broken.
4. `Orders.PaymentTransactionId` has no UNIQUE constraint — duplicate transaction IDs could be inserted.
5. No indexes defined beyond primary keys — query performance on `Orders.OrderDate`, `Books.Title` searches will degrade at scale.
6. `Customers` and `Employees` have no soft-delete mechanism — `CustomerRepository.Delete()` uses hard `DELETE` which can orphan `Orders.CustomerID` references.

**Seed data quality:** 20 books across 4 categories and 3 suppliers, 3 customers, 3 employees, 2 accounts, 3 sample orders with 5 order details. Realistic and sufficient for demonstration.

**Rating:** 6.5/10 (properly normalized, good constraints, but missing indexes, inconsistent soft-delete strategy, report inflates table count)

---

## UI/UX

**Theme system:** Cohesive `AppTheme` class ([`BookStoreApp/Theme/AppTheme.cs`](BookStoreApp/Theme/AppTheme.cs)) with consistent color palette (blue sidebar, colored action buttons). `AppBranding` loads a logo from `Assets/app-logo.png`. Both are applied consistently across forms and controls. This is well-executed.

**Dashboard:** Rich metrics display with 8 KPIs, recent orders table, best-selling books table — good information density.

**Data grids:** Consistent styling via `AppTheme.ApplyGridStyle()`. Column formatting for currency (N2), dates, and alignment is applied. Grid columns are manually configured with width and display index — good attention to detail.

**Filtering:** `BookControl` has 3 dropdown filters (category, publisher, stock level) plus text search. `OrdersControl` has date range pickers, status filter, and text search. `ReportsControl` has a report type dropdown with dynamic parameter controls (top N, threshold). All filters trigger reload on change — responsive design.

**POS Form:** Comprehensive with customer/employee selection, book dropdown with quantity, line-level and order-level discount types (percentage/fixed), tax rate, loyalty points redemption, cash and QR payment buttons. Well-designed for a real POS workflow.

**Weaknesses:**
- No loading indicators during data fetch
- No keyboard shortcuts documented or obvious
- Font size is not configurable
- No confirmation dialog on logout (the current implementation hides the form without asking)
- `PaymentQRForm` auto-closes after 800ms delay on confirmation — user has no time to read the success message
- No data export from grids (only from Reports)

**Rating:** 7/10 (cohesive theming, functional layouts, good filtering UX; missing loading states, accessibility concerns)

---

## Maintainability

**Code structure:** Well-organized into 5 projects with clear boundaries. File naming is consistent (`*Service.cs`, `*Repository.cs`, `I*Service.cs`, `I*Repository.cs`). Folder organization follows feature-based grouping within layers.

**DRY compliance:** Mostly good. `BindBookParams` and `MapBook` are extracted as private helpers. Validation logic is centralized in service methods. However:
1. **Duplicated FileLogger:** `BLL/FileLogger.cs` exists alongside `Utilities/FileLogger.cs`. The BLL copy has a comment "Copy vào BLL để không cần reference BookStoreApp.Utilities nữa" — this is knowingly duplicated code.
2. **Repeated column configuration:** `ConfigureGridColumns()` in `BookControl` and `OrdersControl` follow similar patterns but are not extracted.
3. **Repeated grid data source pattern:** Every UserControl follows `dgv.DataSource = null; dgv.DataSource = ...` — could be a helper method.

**God Form assessment:** No single form exceeds 300 lines. `PosForm` at 228 lines is the largest form — acceptable. Logic is properly delegated to services.

**Event handler quality:** Event handlers are thin — they typically call service methods and update UI. No 100+ line event handlers found. Good discipline.

**Magic numbers:** Minimal. `LoyaltySettings` centralizes loyalty constants. Colors are in `AppTheme`. Timeout values come from `appsettings.json`.

**Rating:** 7.5/10 (clean structure, thin event handlers, centralized constants; minor duplication, Service Locator coupling)

---

## Validation & Error Handling

**Validation pattern:** Consistent use of `ValidationResult` DTO with `IsValid`/`ErrorMessage` properties. Services return `ValidationResult` from all mutating operations.

**Input validation coverage:**
- `BookService.Validate()`: null check, ID check, required fields (Title, Author, ISBN), non-negative prices/quantity — **GOOD**
- `OrderService.CreateOrder()`: customer/employee selection, payment status validity, empty cart check, per-detail quantity > 0, stock availability, book existence — **GOOD**
- `PosService.AddOrUpdateLine()`: book existence, quantity > 0, stock check — **GOOD**
- `PosService.PrepareCheckout()`: customer/employee selection, empty cart, book existence, stock re-check — **GOOD**
- `AuthService.Login()`: null/whitespace username/password — **BASIC**

**Missing validation:**
- **No duplicate ISBN check** in `BookService.AddBook()` — will throw raw `SqlException` when UNIQUE constraint is violated
- **No duplicate username check** for account creation (though no account creation UI exists)
- **No order total sanity checks** (e.g., negative grand total after excessive discounts)
- **Customer/Employee phone format** not validated

**Error handling:**
- `Program.cs` has **empty catch blocks** at lines 43 and 67 — exceptions reading `appsettings.json` are silently swallowed
- Repository methods use `using` for connections/commands (proper disposal)
- `OrderRepository.CreateOrder()` uses **transaction with rollback** — **EXCELLENT**
- `PaymentQRForm` has proper `try/catch` around async payment operations with logging
- `PaymentProviderFactory` silently falls back to Demo when credentials are empty — reasonable but should log a warning
- No global exception handler for unhandled UI thread exceptions

**Rating:** 6.5/10 (good validation in services, transaction support; missing ISBN duplicate check, empty catch blocks, no global exception handler)

---

## Data Management

**Connection handling:** `DbConnectionFactory` is a static factory using a single connection string. Each repository method creates its own connection via `using` — connections are properly disposed. However, there is **no connection pooling configuration** and connections are opened/closed for every operation.

**CRUD quality:**
- `BookRepository`: Full CRUD with soft delete (IsDeleted flag) — **GOOD**
- `CustomerRepository`: Full CRUD with hard DELETE — **INCONSISTENT** (should be soft delete like Books)
- `EmployeeRepository`: Full CRUD with hard DELETE — **INCONSISTENT**
- `OrderRepository`: Create with transaction, read with JOINs, status update — **GOOD**
- `CategoryRepository`: Basic CRUD
- `SupplierRepository`: Basic CRUD

**Transaction usage:** Only `OrderRepository.CreateOrder()` uses `SqlTransaction`. The stock deduction in `PosService.CompleteCheckout()` should be part of the same transaction but isn't — it happens after the order is committed.

**Search implementation:** All search methods load the full table into memory and filter with LINQ. Example: [`BookService.SearchBooks()`](BLL/BookService.cs:19) calls `_bookRepository.GetAll()` then `.Where()` in memory. This won't scale beyond a few thousand records.

**CRITICAL BUG — Stock deduction never persisted:**

In [`BLL/PosService.cs:184-197`](BLL/PosService.cs:184), after `CompleteCheckout()` calls `_orderService.CreateOrder()`:

```csharp
// Deduct stock — moved from OrderRepository to service orchestration layer
foreach (var detail in checkout.Details)
{
    var book = _bookRepository.GetById(detail.BookID);
    if (book is not null)
    {
        var oldStock = book.QuantityInStock;
        book.QuantityInStock = Math.Max(0, book.QuantityInStock - detail.Quantity);
        book.LastSoldDate = DateTime.Now;
        FileLogger.Info(
            $"Stock: Book #{book.BookID} \"{book.Title}\" {oldStock}→{book.QuantityInStock}");
    }
}
```

The stock is modified **only on the in-memory `Book` object** returned by `GetById()`. There is **no call to `_bookRepository.UpdateStock()` or `_bookRepository.Update()`**. The stock deduction is never written to the database. The comment says "moved from OrderRepository to service orchestration layer" but the repository call was apparently removed without adding a replacement. **This means after every sale, the database still shows the original stock quantity.** The application's core inventory management function is broken.

The `BookRepository.UpdateStock()` method exists at [`DAL/Repositories/BookRepository.cs:77`](DAL/Repositories/BookRepository.cs:77) and is fully implemented — it's just never called from the checkout flow.

**Rating:** 4/10 (critical stock persistence bug overshadows otherwise solid CRUD; inconsistent soft-delete; search scalability issues)

---

## Deployment Readiness

**Build:** `dotnet build BookStoreApp.sln` compiles the solution. The README provides clear build and run instructions.

**Database setup:** `database.sql` is a complete, executable script that creates the database, tables, constraints, and seed data. Clear instructions in README for SSMS execution.

**Configuration:** `appsettings.json` with connection string and payment config. Copied to output directory on build. Supports multiple SQL Server variants (localhost, SQLEXPRESS, LocalDB) with documented examples.

**Missing for deployment:**
- No installer/publisher project (no MSI, ClickOnce, or publish profile)
- No database migration strategy — the SQL script is CREATE-only
- No backup/restore functionality (claimed in report as non-functional requirement but not implemented)
- No application version number embedded in the binary or displayed in UI
- Payment credentials are empty by default — requires manual configuration
- No logging of application startup/shutdown
- No health-check or connectivity test on startup
- The application requires SQL Server to be pre-installed and configured

**Rating:** 5/10 (runnable with clear instructions; no installer, no migration strategy, fragile startup with empty catch blocks)

---

## Demo Completeness

**Implemented and functional features:**
- ✅ Login with role-based access (Admin/Staff)
- ✅ Dashboard with 8 KPIs, recent orders, best sellers
- ✅ Book CRUD with soft delete, multi-criteria filtering
- ✅ Category CRUD
- ✅ Supplier CRUD
- ✅ Customer CRUD with purchase history view
- ✅ Employee CRUD (Admin only)
- ✅ Full POS: cart, line/order discounts, tax, loyalty points
- ✅ Order management: list, filter, detail view, status updates
- ✅ 7 report types: revenue summary, by day/week/month, best sellers, low stock, slow moving
- ✅ OxyPlot chart visualization for reports
- ✅ CSV, HTML-table-as-Excel, text-file-as-PDF export
- ✅ MoMo payment provider (full HMAC-SHA256 implementation)
- ✅ VNPay payment provider (full HMAC-SHA512 implementation)
- ✅ Demo payment provider with generated QR bitmap
- ✅ QR code display form with countdown and polling
- ✅ Payment provider factory with automatic fallback
- ✅ File logging (rolling daily logs)
- ✅ Consistent theming system
- ✅ Application icon and branding

**Partially implemented:**
- ⚠️ Stock deduction (calculated correctly but never persisted)
- ⚠️ PDF export (text file with box-drawing characters, not real PDF)
- ⚠️ Excel export (HTML file saved with .xls extension, not real Excel format)

**Not implemented (but claimed or implied):**
- ❌ Password hashing (plaintext only)
- ❌ Import receipt workflow (mentioned in report but no code)
- ❌ Backup/restore functionality
- ❌ Account creation UI
- ❌ Print invoice functionality

---

# Report Review

## Structure

The report (`Desktop.pdf`) follows a standard academic structure:
- Chapter I: Survey & Requirements
- Chapter II: System Analysis
- Chapter III: System Design
- Chapter IV: UI Design

The companion document `docs/functional-description.md` provides additional detail on use cases and workflow. The report is written in Vietnamese, which is appropriate for the course context.

**Rating:** 7/10 (well-organized, follows academic convention)

---

## Requirements

**Functional requirements** are clearly stated: book management, inventory, POS, customer management, reporting, account management, payment integration. These map directly to implemented features.

**Non-functional requirements** listed: performance, security, UI friendliness, extensibility, stability, compatibility. Some are aspirational rather than verified (e.g., "hoạt động trên nhiều thiết bị và hệ điều hành khác nhau" — the app is Windows-only).

**User stories** (US-01 to US-15) are referenced but not individually enumerated in the PDF. The `functional-description.md` provides fuller use case tables.

**Rating:** 6/10 (comprehensive scope, but non-functional requirements are vague and untestable)

---

## System Design

**Database design:** ERD referenced but not embedded in the PDF (exists as `docs/ERD.png`). The textual description is adequate. However, the report claims **10 tables** while the actual schema has **8 tables**. The two "missing" tables are `ImportReceipt` and `ImportDetail` — described in the data analysis section ([`Desktop.pdf:281-283`](Desktop.pdf:281-283)) but never implemented.

**Class design:** Lists DTO, DAL, BLL, UI, and Utilities classes. Class diagram exists at `docs/class-diagram.png` — not embedded in the PDF.

**Architecture description:** Correctly describes the 4-layer model with `DbConnectionFactory` as the database intermediary. Mentions the payment subsystem with its provider pattern.

**Rating:** 5.5/10 (accurate in most respects, but the 10-table claim and ImportReceipt references are fabrications)

---

## Database Explanation

The report describes the normalization process from manual Excel records to relational tables. However:
- Claims "mã hóa mật khẩu" (password encryption) but the code uses plaintext comparison
- Claims `PasswordHash` column name but the actual column is `Password`
- The detailed description of `ImportReceipt` and `ImportDetail` tables suggests these were planned but never built

**Rating:** 4/10 (good normalization narrative, but contains false claims about password hashing and non-existent tables)

---

## Screenshots

The PDF references 14 figures (Hình 4.1 through Hình 4.8.2) but **no actual images are embedded** in the PDF. The figure captions describe what should appear:
- Dashboard, Books, Categories, Suppliers, Customers, Orders
- Reports (7 sub-figures for different report types)
- Employees (2 sub-figures)

The PDF appears to be a text-only export that lost all embedded images. This is a significant documentation quality failure.

**Rating:** 1/10 (references exist but no visible screenshots)

---

## Testing

The report mentions "kiểm thử hộp trắng và hộp đen" (white-box and black-box testing) in `functional-description.md` line 45. However:
- No test cases are documented anywhere
- No unit test project exists in the solution
- No test data or test scripts are provided
- No bug reports or test results are included

The `BookStoreApp.sln` contains only production projects — no test project.

**Rating:** 1/10 (testing methodology mentioned but zero evidence of actual testing)

---

## References

No references section exists in `Desktop.pdf`. No citations for:
- .NET Framework / C# documentation
- SQL Server documentation
- MoMo/VNPay API specifications
- OxyPlot library
- Microsoft.Data.SqlClient
- Any academic sources on software design or bookstore management

**Rating:** 0/10 (no references provided)

---

## Documentation Quality

**Strengths:**
- The README is excellent: clear structure, technology table, architecture diagram, build instructions, configuration examples, test accounts
- `database.sql` is well-commented and executable
- Code has Vietnamese comments explaining intent
- `functional-description.md` provides additional depth

**Weaknesses:**
- PDF report has no embedded images
- Report contains factual errors (table count, password hashing, PDF export)
- No references/citations
- No testing documentation
- No deployment guide beyond basic build instructions
- No troubleshooting section

**Rating:** 4.5/10 (README is good, PDF report is substandard)

---

# Report ↔ Code Audit

| # | Report Claim | Code Evidence | Match? | Severity | Fix |
|---|-------------|---------------|--------|----------|-----|
| 1 | "10 bảng được thiết kế logic" | `database.sql` has 8 tables | ❌ MISMATCH | **High** | Either add ImportReceipt/ImportDetail tables or correct report to say 8 |
| 2 | "PasswordHash" column | `Accounts.Password` is the actual column name | ❌ MISMATCH | **Medium** | Rename column or update report; implement actual hashing |
| 3 | "mã hóa mật khẩu" (password encryption) | `AuthService.Login()` uses `string.Equals(account.Password, password, StringComparison.Ordinal)` | ❌ MISMATCH | **Critical** | Implement BCrypt or SHA256 hashing |
| 4 | "ImportReceipt & ImportDetail" tables | Not present in `database.sql` or any repository | ❌ MISMATCH | **High** | Remove from report or implement |
| 5 | "Xuất báo cáo ra file Excel hoặc PDF" | CSV: ✅; Excel: HTML with .xls extension; PDF: text file with box characters | ⚠️ PARTIAL | **Medium** | Use ClosedXML for real Excel, iTextSharp/PdfSharp for real PDF |
| 6 | "Transaction được rollback" on DB error | `OrderRepository.CreateOrder()` does use try/catch with `tran.Rollback()` | ✅ MATCH | — | — |
| 7 | "Tự động kiểm tra tồn kho và cập nhật số lượng sách" | Stock is checked but **never persisted** after sale | ❌ MISMATCH | **Critical** | Add `_bookRepository.UpdateStock()` call in `PosService.CompleteCheckout()` |
| 8 | "Phân quyền tài khoản" (role-based access) | `MainForm` hides sidebar buttons for Staff; `BookControl` accepts `readOnly` | ✅ MATCH | — | — |
| 9 | "Tích hợp qr code của các ngân hàng hay ví điện tử như momo" | `MomoPaymentProvider`, `VNPayPaymentProvider`, `DemoPaymentProvider` all implement `IPaymentProvider` | ✅ MATCH | — | — |
| 10 | "Hỗ trợ tìm kiếm sách nhanh" | `BookService.SearchBooks()` loads all books and filters in memory | ⚠️ PARTIAL | **Low** | Add server-side search with SQL LIKE |
| 11 | "Sao lưu và phục hồi dữ liệu" (backup/restore) | No implementation found | ❌ MISMATCH | **Medium** | Remove from non-functional requirements or implement |
| 12 | "Hệ thống có thể hoạt động trên nhiều thiết bị và hệ điều hành" | .NET 9 Windows Forms is Windows-only | ❌ MISMATCH | **Medium** | Correct to "Windows only" |
| 13 | Use Case diagrams | `docs/functional-description.md` contains Mermaid flowchart | ✅ MATCH | — | — |
| 14 | Class diagram | `docs/class-diagram.png` and `docs/class-diagram.mmd` exist | ✅ MATCH | — | — |
| 15 | Seed data: "20 sách, 3 khách hàng, 3 nhân viên, 2 tài khoản" | Confirmed in `database.sql` | ✅ MATCH | — | — |

**Summary:** 6 matches, 2 partial matches, 7 mismatches. The mismatches are concentrated in security claims (password hashing), data integrity (stock persistence), and report exaggeration (table count, import workflow, PDF export, cross-platform).

---

# Major Issues

| # | Location | Problem | Evidence | Impact | Affected Score | Fix |
|---|----------|---------|----------|--------|----------------|-----|
| 1 | [`BLL/PosService.cs:184-197`](BLL/PosService.cs:184) | **Stock deduction never persisted to database** | In-memory only: `book.QuantityInStock = ...` without calling `_bookRepository.UpdateStock()` or `Update()` | **Critical** — Core inventory function broken | Program −1.0 | Add `_bookRepository.UpdateStock(book.BookID, book.QuantityInStock, book.LastSoldDate)` after the stock modification |
| 2 | [`BLL/AuthService.cs:29`](BLL/AuthService.cs:29) | **Passwords stored and compared in plaintext** | `string.Equals(account.Password, password, StringComparison.Ordinal)` | **Critical** — Security violation; seed data has passwords '1' and '2' | Program −0.5, Report −0.5 | Implement BCrypt.Net or `SHA256` + salt; add `PasswordHash` column; update seed data |
| 3 | [`Desktop.pdf:388`](Desktop.pdf:388) | **Report claims 10 tables, only 8 exist** | `database.sql` has 8 CREATE TABLE statements | **High** — Fabrication in academic report | Report −0.75 | Correct the report or implement the missing tables |
| 4 | [`Desktop.pdf:560`](Desktop.pdf:560) | **Report claims password encryption** | No hashing in code; column is `Password` not `PasswordHash` | **High** — False claim in report | Report −0.5 | Implement hashing or remove the claim |
| 5 | [`BookStoreApp/ServiceLocator.cs`](BookStoreApp/ServiceLocator.cs) | **Service Locator anti-pattern instead of DI** | All services are static singletons accessed via static properties | **Medium** — Testability and coupling | Program −0.25 | Use `Microsoft.Extensions.DependencyInjection` or constructor injection |
| 6 | [`Desktop.pdf`](Desktop.pdf) | **No screenshots in PDF report** | 14 figure references with no embedded images | **High** — Documentation failure | Report −1.0 | Re-export PDF with embedded images |

---

# Minor Issues

| # | Location | Problem | Impact | Fix |
|---|----------|---------|--------|-----|
| 1 | [`BLL/BookService.cs:80-87`](BLL/BookService.cs:80) | No duplicate ISBN check before insert | SQL exception instead of clean validation | Add `_bookRepository.GetByISBN()` or check in-memory before Add |
| 2 | [`DAL/Repositories/CustomerRepository.cs:64-71`](DAL/Repositories/CustomerRepository.cs:64) | Hard DELETE on Customers | Orphans Orders.CustomerID references | Use soft delete like BookRepository |
| 3 | [`DAL/Repositories/EmployeeRepository.cs:53-59`](DAL/Repositories/EmployeeRepository.cs:53) | Hard DELETE on Employees | Orphans Orders.EmployeeID and Accounts.EmployeeID references | Use soft delete |
| 4 | [`BookStoreApp/Program.cs:42-45`](BookStoreApp/Program.cs:42) | Empty catch blocks swallow config errors | Silent failures; hard to diagnose | Log the exception |
| 5 | [`BookStoreApp/Program.cs:66-69`](BookStoreApp/Program.cs:66) | Empty catch blocks for payment config | Silent failures | Log the exception |
| 6 | [`BookStoreApp/Forms/MainForm.cs:44-49`](BookStoreApp/Forms/MainForm.cs:44) | New UserControl created on every navigation | State loss, memory pressure, redundant DB queries | Cache UserControls in a dictionary keyed by page name |
| 7 | [`BLL/FileLogger.cs`](BLL/FileLogger.cs) + [`Utilities/FileLogger.cs`](Utilities/FileLogger.cs) | Duplicated FileLogger class in two projects | Maintenance burden | Consolidate into Utilities and reference from BLL |
| 8 | [`BookStoreApp/Forms/LoginForm.cs:36`](BookStoreApp/Forms/LoginForm.cs:36) | No login attempt limiting | Brute-force vulnerability | Add attempt counter with temporary lockout |
| 9 | [`BookStoreApp/Forms/MainForm.cs:89-95`](BookStoreApp/Forms/MainForm.cs:89) | No logout confirmation dialog | Accidental logout risk | Add `MessageBox.Show("Are you sure?", ...)` |
| 10 | [`database.sql`](database.sql) | No indexes beyond primary keys | Query performance at scale | Add indexes on `Orders.OrderDate`, `Books.Title`, `Books.ISBN` |
| 11 | [`database.sql:67`](database.sql:67) | `Accounts.EmployeeID` is nullable but conceptually should link to an employee | Inconsistent data model | Make NOT NULL or remove column if not used |
| 12 | [`Utilities/ReportExporter.cs:59-104`](Utilities/ReportExporter.cs:59) | "PDF" export writes a `.txt` file with box-drawing characters | Misleading file extension | Use a real PDF library or rename to "Text Report" |
| 13 | [`BLL/BookService.cs:19-31`](BLL/BookService.cs:19) | Search loads entire table into memory | Performance at scale | Add SQL WHERE clause with LIKE |
| 14 | [`BookStoreApp/Forms/PaymentQRForm.cs:187-195`](BookStoreApp/Forms/PaymentQRForm.cs:187) | Auto-closes 800ms after payment confirmation | User can't read success message | Add a "Close" button and let user dismiss manually |
| 15 | No `.editorconfig` or code style configuration | Inconsistent code formatting across team | Minor maintainability | Add `.editorconfig` to solution |

---

# Cleanup Review

| Action | Files | Reason |
|--------|-------|--------|
| **Keep** | All `BLL/*Service.cs`, all `DAL/Repositories/*Repository.cs`, all `DTO/**/*.cs`, all `BookStoreApp/Forms/*.cs`, all `BookStoreApp/UserControls/*.cs`, `BookStoreApp/Theme/*.cs` | Core application code |
| **Archive** | `Utilities/FileLogger.cs` | Duplicate of `BLL/FileLogger.cs` — consolidate into one |
| **Refactor** | `BookStoreApp/ServiceLocator.cs` | Replace Service Locator with proper DI container |
| **Refactor** | `BLL/PosService.cs:184-197` | Add stock persistence call |
| **Refactor** | `BLL/AuthService.cs` | Add password hashing |
| **Refactor** | `BookStoreApp/Forms/MainForm.cs` | Cache UserControls to prevent state loss |
| **Avoid Touching** | `BLL/Payments/*.cs` | Payment providers are well-implemented and externally specified |
| **Avoid Touching** | `DAL/DbConnectionFactory.cs` | Simple, correct, single-responsibility |

---

# Must-Fix List

| # | Issue | Risk | Exact Fix | Expected Score Recovery |
|---|-------|------|-----------|------------------------|
| 1 | Stock never persisted after sale | Application is non-functional for inventory management | In `PosService.CompleteCheckout()`, after line 196, add: `_bookRepository.UpdateStock(book.BookID, book.QuantityInStock, book.LastSoldDate);` | +0.75 Program |
| 2 | Plaintext passwords | Security vulnerability; report fabrication | Add `BCrypt.Net-Next` NuGet package; hash passwords with `BCrypt.HashPassword()`; update `AuthService.Login()` to use `BCrypt.Verify()`; update seed data | +0.4 Program, +0.5 Report |
| 3 | Report claims 10 tables, has 8 | Academic integrity concern | Correct `Desktop.pdf` line 388 to "8 bảng" | +0.5 Report |
| 4 | No screenshots in report | Documentation failure | Re-export PDF from Word/LaTeX with images embedded; ensure all 14 figures are visible | +0.75 Report |
| 5 | Fake PDF export | Misleading functionality | Either use `PdfSharp`/`iTextSharp` for real PDF or rename export option to "Text Report" | +0.25 Program |

---

# Should-Fix List

| # | Issue | Fix |
|---|-------|-----|
| 1 | Hard DELETE on Customer/Employee | Add `IsDeleted` column, update repositories to soft-delete |
| 2 | Service Locator pattern | Migrate to `Microsoft.Extensions.DependencyInjection` with constructor injection |
| 3 | Duplicate FileLogger | Delete `BLL/FileLogger.cs`, reference `Utilities/FileLogger.cs` from BLL |
| 4 | Empty catch blocks | Log exceptions to FileLogger instead of swallowing |
| 5 | Search scalability | Add optional `WHERE` clauses to SQL queries |
| 6 | No indexes | Add `CREATE INDEX` statements to `database.sql` |
| 7 | New UserControl per navigation | Cache instances in `Dictionary<string, UserControl>` in MainForm |
| 8 | No logout confirmation | Add confirmation dialog |
| 9 | No ISBN uniqueness validation | Add check in `BookService.AddBook()` before insert |
| 10 | Accounts.EmployeeID nullable | Either link seed accounts to employees or document why it's nullable |

---

# Optional Polish

| # | Suggestion | Benefit |
|---|-----------|---------|
| 1 | Add unit test project (xUnit/NUnit) | Testability, academic rigor |
| 2 | Add loading spinners during data fetch | UX improvement |
| 3 | Add keyboard shortcuts (Ctrl+N for new book, Ctrl+S for save, etc.) | Power-user efficiency |
| 4 | Add data export from data grids (right-click → Export to CSV) | User convenience |
| 5 | Add application version number to window title | Support/debugging |
| 6 | Add `ImportReceipt`/`ImportDetail` tables and workflow | Completeness (matches report claims) |
| 7 | Add real Excel export via ClosedXML | Professional output |
| 8 | Add print functionality for invoices | Real-world POS requirement |
| 9 | Add settings form for configurable parameters | Flexibility |
| 10 | Add Vietnamese (and English) UI localization | Accessibility |
| 11 | Create WiX installer or ClickOnce publish profile | Deployment polish |
| 12 | Add `Dockerfile` with SQL Server container for easy demo | Reproducibility |

---

# Suggested Final Target

| Component | Current (Normal) | Target (with fixes) | How to Reach |
|-----------|-----------------|---------------------|--------------|
| Program | 3.0 / 4 | **3.5 / 4** | Fix stock persistence, add password hashing, replace fake PDF with real PDF or rename |
| Report | 2.5 / 4 | **3.25 / 4** | Embed screenshots, correct table count, remove ImportReceipt references or add tables, remove false password-hashing claim |
| **Total** | **5.5 / 8** | **6.75 / 8** | |

The 1.25-point recovery is achievable with approximately 4–6 hours of work concentrated on the must-fix list.

---

# Final Examiner Verdict

## Pass under strict grading? ❌ NO
The stock persistence bug means the core inventory function does not work. Plaintext passwords are unacceptable for any system handling financial data. The report contains verifiable falsehoods (10 tables, password hashing, real PDF). **3.5/8 — Fail.**

## Pass under normal grading? ✅ BORDERLINE PASS
The project demonstrates genuine technical skill across a broad feature set. The architecture is fundamentally sound, and most features work correctly. The critical bug and report inaccuracies pull the score down, but the overall quality is sufficient for a university group project. **5.5/8 — Pass (above 5.0).**

## Pass under easy grading? ✅ CLEAR PASS
The scope and ambition of the project, the clean code structure, the payment integrations, and the POS workflow earn significant credit. The bugs are noted but substantially discounted. **6.5/8 — Strong Pass.**

## Largest score loss:
The stock-persistence bug in `PosService.CompleteCheckout()` — it invalidates the claim that the system "automatically updates stock after each transaction."

## First fixes (in order):
1. Add `_bookRepository.UpdateStock()` call in `PosService.CompleteCheckout()`
2. Implement password hashing
3. Re-export PDF report with embedded screenshots
4. Correct the 10-table claim to 8

## Is the architecture maintainable? ✅ YES
The layered architecture with interface segregation, repository pattern, and service orchestration is genuinely maintainable. A new developer could understand the structure within an hour. Adding new features (e.g., a new report type) follows a clear pattern: DTO → Repository interface → Repository implementation → Service → UI. The Service Locator is the main architectural weakness but is not fatal at this scale.

## Does the report reflect the implementation? ⚠️ PARTIALLY
The report correctly describes the system's features, architecture, and design decisions at a high level. However, specific factual claims (10 tables, password hashing, cross-platform compatibility, backup/restore, real PDF export) do not match the code. The report describes what the team intended to build rather than what was actually built. For an academic submission, this discrepancy between claimed and implemented features is the most serious documentation issue.

---

*Audit completed 2026-06-07. All findings are evidence-based with file:line references. No prior review was consulted.*
