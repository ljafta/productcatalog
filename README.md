# ProductCatalog



This repository contains a full-stack Product Catalog Management System built with:

- **Backend:** ASP.NET Core Web API
- **Frontend:** Angular 16+ (Standalone Components)
- **Architecture:** Clean separation of concerns, DTO-based contracts

## Features

### Backend (ASP.NET Core)
- CRUD operations for Products and Categories
- Category hierarchy (tree support)
- Pagination, search, and category filtering
- In-memory repositories (for simplicity)
- Swagger UI for API exploration


### Frontend (Angular)
- Product listing with search and category filter
- Add/Edit product form with validation
- Delete product with confirmation
- Category management
- Loading indicators and error handling
- RxJS-based API calls
- Standalone components
- Unit tests (Angular + API)

---

## Prerequisites

- .NET 8 SDK
- Node.js 18+
- Angular CLI 16+


---

## Running the Backend

```bash
cd backend/ProductCatalog.Api

Visual Studio (Play ▶) → IIS Express
Backend  is coming from IIS Express, not Kestrel.

Backend will be available at:
https://localhost:44329

Swagger:
https://localhost:44329/swagger

Running the Frontend

cd frontend/product-catalog-ui
npm install
ng serve

Frontend will be available at:

http://localhost:4200

Frontend
ng test
