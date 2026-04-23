# Simplified Banking System

This repository contains the final exam project for **PROG8555 Microsoft Web Technology**. It is a simplified banking system built entirely on **ASP.NET Core (MVC + Web API)** within a single solution architecture. 

The application serves two main purposes:
1. **Customer Web Portal (MVC):** A front-end interface where customers can view their accounts, check balances, perform deposits, and process withdrawals.
2. **ATM Backend Service (Web API):** A RESTful API that simulates an ATM, allowing third-party integration to check balances and process transactions via JSON payloads.

---

## Technologies Used
* **Framework:** .NET 10 / ASP.NET Core
* **Architecture:** MVC (Model-View-Controller) & REST API
* **Database:** SQLite
* **ORM:** Entity Framework Core (Code First)
* **Frontend:** HTML, C#, Razor Pages, Bootstrap 5
* **Documentation/Testing:** Swagger UI / OpenApi

---

## Setup & Installation

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (Version 10.0 or later)
* A code editor like [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio 2022.

### Getting Started

1. **Clone the repository** to your local machine.
2. **Navigate** to the project directory:
   ```bash
   cd BankingSystem
   ```
3. **Run Database Migrations** (if not already applied):
   ```bash
   dotnet ef database update
   ```
   *(Note: The repository might already include a seeded `banking.db` file for testing).*
4. **Run the Application:**
   ```bash
   dotnet run
   ```
5. Open your browser and navigate to the provided `localhost` URL (typically `http://localhost:5000` or `https://localhost:5001`).

---

## Web Portal Features (MVC)

The MVC portal is accessible via the root URL and includes the following features:
* **Home Page:** A welcome landing page.
* **Accounts Dashboard:** Lists all available accounts seeded in the database.
* **Account Details:** Displays specific account balances and provides options to deposit, withdraw, or view history.
* **Transaction Processing:** Forms to handle deposits and withdrawals with robust server-side validation.
* **Transaction History:** A detailed table logging all previous deposits/withdrawals and accurately displaying the running "Balance After" for each entry.

---

## ATM API Endpoints (Web API)

You can view and test all endpoints visually by navigating to `http://localhost:<PORT>/swagger` when the application is running.

Alternatively, you can test the following RESTful endpoints via Postman or `curl`:

| Method | Route | Description | Expected Body/Params |
|---|---|---|---|
| **GET** | `/api/atm/balance/{accountId}` | Retrieves the current account balance. | `accountId` in URL |
| **POST** | `/api/atm/deposit` | Processes an ATM deposit. | `{ "accountId": int, "amount": decimal }` |
| **POST** | `/api/atm/withdraw` | Processes an ATM withdrawal. | `{ "accountId": int, "amount": decimal }` |
| **GET** | `/api/atm/transactions/{accountId}` | Retrieves transaction history. | `accountId` in URL |

### API Response Examples:

**Balance Response DTO:**
```json
{
  "accountId": 1,
  "accountNumber": "ACC-12345",
  "balance": 1500.00
}
```

**Transaction Response DTO:**
```json
[
  {
    "transactionId": 1,
    "amount": 200.50,
    "transactionType": "Deposit",
    "transactionDate": "2023-10-25T14:30:00",
    "description": "ATM Deposit"
  }
]
```

---

## Project Structure & Patterns
* **Dependency Injection:** The `IBankRepository` is registered and injected into both the MVC Controllers and the API Controllers to ensure the `DbContext` is never used directly within a controller.
* **Data Annotations:** Both Domain Models and Request DTOs utilize strict data annotations (`[Required]`, `[Range]`, `[RegularExpression]`) to validate data before processing.
* **Shared Context:** Both the Web Portal and the ATM API share the exact same SQLite Database context, meaning a deposit via the API will instantly reflect in the Web Portal. 

---

**Author:** Peter Toan Do
**Student Number:** 9086580
