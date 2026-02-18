
## 3SOLUTION.md` (Design & Trade-offs – REQUIRED)

```md
# Solution Design & Trade-offs

## Architecture Overview

This solution follows a clear separation between frontend and backend, with a contract-first mindset using DTOs.

### Backend Architecture
- ASP.NET Core Web API
- Controller → Service/Repository pattern
- DTOs used to isolate API contracts from domain entities
- In-memory repositories for simplicity and faster setup
- Pagination and filtering handled server-side

### Frontend Architecture
- Angular 16+ with standalone components
- Feature-based folder structure
- Services for API communication
- Strongly typed TypeScript interfaces matching backend DTOs 1:1
- RxJS for async data handling

---

## Data Contracts

Angular models were intentionally designed to match backend DTOs exactly.

Example:

**Backend**
```csharp
public record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    int Quantity,
    Guid CategoryId
);

