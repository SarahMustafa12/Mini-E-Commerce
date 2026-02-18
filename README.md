# Mini E-Commerce Backend

A simple backend API for a mini e-commerce system built with ASP.NET Core Web API.  
This project demonstrates basic CRUD operations with a focus on creating and retrieving data.

## Features
- Add products
- Place orders with customer details and selected products
- View order details, including ordered items and quantities

## Notes
- No authentication or user accounts are implemented
- Customer information is provided at the time of placing an order

## Purpose
This project is designed as a technical task for an internship and aims to demonstrate:
- ASP.NET Core Web API fundamentals
- Entity Framework Core usage
- Database migrations
- Basic repository pattern for data management

## Run Instructions
After configuring the database connection, run the following commands in the Package Manager Console:

```powershell
Add-Migration InitialCreate
Update-Database
