# Agri-Energy Connect Platform

Agri-Energy Connect is a role-based ASP.NET Core MVC web application that connects agricultural producers (Farmers) with energy service providers (Employees). Employees can register farmers, while farmers can log in to manage and track their product listings.
This project is a prototype web application built with **ASP.NET Core MVC** and **Entity Framework Core**. It supports a role-based access system for two user roles: **Employees** and **Farmers**.

## Table of Contents

* [Features](#features)
* [Requirements](#requirements)
* [Setup Instructions](#setup-instructions)
* [Database Seeding](#database-seeding)
* [How It Works](#how-it-works)

  * [Role-Based Access](#role-based-access)
  * [Employee Functionality](#employee-functionality)
  * [Farmer Functionality](#farmer-functionality)
* [Project Structure](#project-structure)
* [Notes](#notes)

---

## Features

* Role-based access control (Employees and Farmers)
* Secure authentication using ASP.NET Identity
* Employees can register Farmer accounts
* Farmers can log in and manage their own products
* Product filtering and dashboard functionality

---

## Requirements

* [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* Visual Studio 2022+ or Visual Studio Code
* SQLite (used as the database engine)

---

## Setup Instructions

1. **Clone the repository:**

```bash
[https://github.com/your-username/AgriEnergyConnect.git](https://github.com/BulelaCool/AgriEnergyConnect.git)
```

2. **Navigate into the project folder:**

```bash
cd AgriEnergyConnect
```

3. **Restore dependencies:**

```bash
dotnet restore
```

4. **Apply migrations and create the database:**

```bash
dotnet ef database update
```

5. **Run the project:**

```bash
dotnet run
```

The app will launch at `https://localhost:5001` or `http://localhost:5000`

---

## Database Seeding

Upon first run, the app seeds two roles (`Employee`, `Farmer`) into the database using `SeedData.cs`. If they do not exist, they will be created automatically.

To ensure this works:

* Check the `SeedData.Initialize(IServiceProvider serviceProvider)` method in `SeedData.cs`.
* You can manually create an initial Employee user for testing by extending this seed method.

---

## How It Works

### Role-Based Access

* **Employee**: Has elevated permissions to create new **Farmer** accounts.
* **Farmer**: Can log in and manage their product listings only.

Roles are assigned using ASP.NET Identity and `UserManager` during registration or seeding.

### Employee Functionality

* Only **Employees** can register new **Farmer** accounts.
* Accessible via `/Employee/RegisterFarmer`
* Can view a list of all registered Farmers and their associated products.
* Can filter products by category or date using `/Employee/FilterProducts`

### Farmer Functionality

* Farmers can only view and manage their own products.
* Upon login, a Farmer is redirected to `/Farmer/Dashboard`.

### Login Behavior

* On login, the system checks the user's role:

  * If **Employee**, redirected to `/Employee/Index`
  * If **Farmer**, redirected to `/Farmer/Dashboard`

This redirection is handled in `Login.cshtml.cs`.

---

## Project Structure

```
AgriEnergyConnect/
|-- Controllers/
|   |-- EmployeeController.cs
|   |-- FarmerController.cs
|-- Models/
|   |-- Farmer.cs
|   |-- Product.cs
|   |-- ViewModels/
|       |-- RegisterFarmerViewModel.cs
|-- Data/
|   |-- ApplicationDbContext.cs
|   |-- SeedData.cs
|-- Areas/Identity/
|   |-- Pages/Account/
|       |-- Login.cshtml
|       |-- Login.cshtml.cs
|-- Views/
|   |-- Employee/
|   |-- Farmer/
|-- Program.cs
|-- appsettings.json
```

---

## Notes

* This is a prototype project and should not be used in production without further security hardening.
* All views are secured with `[Authorize]` attributes based on roles.
* Additional features like email confirmation or multi-factor authentication are not enabled by default.

---

## Contact

For further information or assistance, reach out to the project maintainer or submit an issue in the repository.
