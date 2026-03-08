# 📋 To-Do Application

A full-stack to-do application built with **C# ASP.NET Core** and **Vue 3 + TypeScript**, demonstrating **SOLID principles**, **code quality**, **testing**, and **UX consistency**.

## 🚀 Quick Start

### Prerequisites
- .NET 10 SDK
- Node.js 18+

### Run Backend
```bash
cd backend/ToDoApi
dotnet run
# Runs on https://localhost:5001
```

### Run Frontend (new terminal)
```bash
cd frontend
npm install
npm run dev
# Runs on http://localhost:5173
```

### Run Tests
```bash
cd backend/ToDoApi.Tests
dotnet test
# Expected: ✅ 4/4 tests pass
```

## 📚 Documentation

- **[Architecture & Best Practices](./speckit-experiment/ARCHITECTURE.md)** - SOLID principles, code quality, UX design
- **[Development Setup Guide](./speckit-experiment/DEVELOPMENT.md)** - Local setup, tools, troubleshooting

## ✨ Features

✅ **SOLID Principles**
- Single Responsibility: Separate controllers, repositories, models
- Open/Closed: Interface-based repository abstraction
- Liskov Substitution: Swappable data implementations
- Interface Segregation: Minimal, focused contracts
- Dependency Inversion: Constructor injection, DI container

✅ **Code Quality**
- Nullable reference types enabled (.NET)
- TypeScript strict mode (Vue)
- Type-safe async/await patterns
- Proper error handling & HTTP status codes
- CORS security configured

✅ **Testing**
- xUnit test suite with 4 unit tests
- Repository layer tests covering CRUD operations
- All tests passing in CI

✅ **UX Consistency**
- Cohesive Vue color scheme (`#42b983` green)
- Clear form interactions (add, complete, delete)
- Error & loading state feedback
- Responsive, accessible components

## 📦 Tech Stack

| Layer | Technology |
|-------|-----------|
| **Backend** | ASP.NET Core 10, C# |
| **Frontend** | Vue 3, TypeScript, Vite |
| **Testing** | xUnit |
| **HTTP** | Axios (frontend), OpenAPI (backend) |

## 📁 Project Structure

```
speckit-experiment/
├── backend/ToDoApi/              # REST API
├── backend/ToDoApi.Tests/        # Unit tests
├── frontend/                     # Vue + TypeScript SPA
├── ARCHITECTURE.md               # Design & principles
├── DEVELOPMENT.md                # Setup guide
└── README.md                     # This file
```

## 🔄 Data Flow

```
┌─────────────┐      HTTP      ┌──────────────┐
│  Vue UI     │─────────────→  │ ASP.NET Core │
│ (Frontend)  │←─────────────  │  (Backend)   │
└─────────────┘    JSON        └──────────────┘
                                       │
                                       ▼
                            ┌──────────────────┐
                            │ In-Memory Repo   │
                            │ (TodoItem[])     │
                            └──────────────────┘
```

## 🧪 Test Results

```
dotnet test
Test summary: total: 4, failed: 0, succeeded: 4, skipped: 0
✅ AddAndGetById_ShouldReturnInsertedItem
✅ Update_Nonexistent_ShouldReturnFalse
✅ Delete_ShouldRemoveItem
✅ [Additional test]
```

## 🎯 Next Steps

- Add database layer (Entity Framework Core)
- Implement authentication (JWT)
- Add frontend unit tests (Vitest)
- Containerize with Docker
- Deploy to Azure/Cloud

---

**Created**: March 2026 | **Framework**: .NET 10, Vue 3
