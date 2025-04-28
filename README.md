# Train Component Management API
This is the backend API for the Train Component Management Application.

## API Endpoints
- `GET /api/traincomponents` — Get all train components (with optional search, pagination)
- `POST /api/traincomponents` — Create a new train component
- `GET /api/traincomponents/{id}` — Get a specific train component by ID
- `PUT /api/traincomponents/{id}` — Update a train component
- `DELETE /api/traincomponents/{id}` — Delete a train component
- `PATCH /api/traincomponents/{id}/quantity` — Update quantity for a train component
  
## Technologies
- ASP.NET Core 6.0
- Entity Framework Core
- CORS configured for Angular frontend app
  
## Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- PostgreSQL server running locally
  
## How to Run
cd TrainComponentManagementAPI\TrainComponentManagement
dotnet build
dotnet run

### Note
- Database `train_component_db` will be automatically created on application startup if it does not exist (using EF Core Migrations).
- Make sure your PostgreSQL server is running locally.
- Adjust `ConnectionStrings:DefaultConnection` in `appsettings.json` if needed.
