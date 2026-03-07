# Development Setup Guide

## Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org)
- IDE: Visual Studio Code, Visual Studio, or JetBrains Rider

## Initial Setup

### Backend Setup
```bash
cd zero/backend/ToDoApi
dotnet restore
dotnet build
```

### Frontend Setup
```bash
cd zero/frontend
npm install
```

## Development Workflow

### Starting the Backend
```bash
cd zero/backend/ToDoApi
dotnet run
```
- API runs on `https://localhost:5001`
- OpenAPI docs at `https://localhost:5001/openapi/v1.json`

### Starting the Frontend
```bash
cd zero/frontend
npm run dev
```
- Dev server at `http://localhost:5173`
- HMR enabled for hot reloads

### Running Tests
```bash
cd zero/backend/ToDoApi.Tests
dotnet test --logger="console;verbosity=detailed"
```

## Code Quality Tools

### Backend Code Style
- Format: `dotnet format`
- Analyze: `dotnet build --no-restore` (warnings enabled)

### Frontend Code Style
- TypeScript check: `npm run build` (includes `vue-tsc`)
- Format: Install Prettier in VS Code

## Project Dependencies

### Backend
- **ASP.NET Core 10**: Web framework
- **xUnit**: Unit testing

### Frontend
- **Vue 3**: UI framework
- **TypeScript**: Type safety
- **Vite**: Build tool
- **Axios**: HTTP client

## Troubleshooting

### Backend won't start
```bash
# Check port availability (5001)
# Clear build cache
rm -r bin obj
dotnet clean
dotnet build
```

### Frontend dev server won't start
```bash
# Clear node_modules
rm -r node_modules package-lock.json
npm install
npm run dev
```

### CORS errors in console
Ensure backend CORS policy matches frontend URL:
```csharp
var allowOrigins = new[] { "http://localhost:5173" };
```

### TypeScript errors in frontend
```bash
# Rebuild type definitions
npm run build
# Check tsconfig.json for strict mode settings
```

## Deployment

### Build for Production
```bash
# Backend
cd backend/ToDoApi
dotnet publish -c Release

# Frontend
cd frontend
npm run build
# Output: dist/ folder
```

### Docker (Optional)
Create `Dockerfile` for backend containerization (future enhancement).
