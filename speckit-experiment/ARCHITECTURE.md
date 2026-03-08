# To-Do Application Architecture & Best Practices

## Overview
A full-stack to-do application demonstrating **SOLID principles**, **code quality**, **testing**, and **UX consistency**.

- **Backend**: C# ASP.NET Core Web API (.NET 10.0)
- **Frontend**: Vue 3 + TypeScript with Vite
- **Testing**: xUnit for backend

---

## SOLID Principles Implementation

### 1. **Single Responsibility Principle (SRP)**
Each class has one reason to change:

- **`TodoItem.cs`**: Only represents a to-do model
- **`ITodoRepository.cs`**: Defines data access contract
- **`InMemoryTodoRepository.cs`**: Implements in-memory storage only
- **`TodosController.cs`**: Handles HTTP routing and responses
- **`todoService.ts`**: Manages API HTTP calls
- **`TodoList.vue`**: Manages todo list state & UI
- **`TodoItem.vue`**: Renders individual todo item

### 2. **Open/Closed Principle (OCP)**
Classes are open for extension, closed for modification:

```csharp
// Interface allows alternative implementations without changing controller
public interface ITodoRepository { ... }

// Can implement SqlRepository, MongoRepository, etc.
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
```

### 3. **Liskov Substitution Principle (LSP)**
Any `ITodoRepository` implementation can be substituted:

```csharp
// Current implementation
services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();

// Future implementations will work without controller changes
// services.AddSingleton<ITodoRepository, SqlRepository>();
```

### 4. **Interface Segregation Principle (ISP)**
`ITodoRepository` exposes only essential methods:

```csharp
public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task AddAsync(TodoItem item);
    Task<bool> UpdateAsync(TodoItem item);
    Task<bool> DeleteAsync(Guid id);
}
```

### 5. **Dependency Injection Principle (DIP)**
`TodosController` depends on abstraction, not concrete implementation:

```csharp
public class TodosController : ControllerBase
{
    private readonly ITodoRepository _repository; // Injected interface
    
    public TodosController(ITodoRepository repository)
    {
        _repository = repository; // DI via constructor
    }
}
```

---

## Code Quality Standards

### Backend (.NET)

#### Null Handling
- Nullable reference types enabled (`<Nullable>enable</Nullable>`)
- Explicit null-coalescing:
  ```csharp
  return item is null ? NotFound() : Ok(item);
  ```

#### Type Safety
- Strong typing throughout
- Async/await for all I/O operations
- Generics for reusable types

#### Error Handling
- HTTP status codes properly returned: `NotFound()`, `BadRequest()`, `NoContent()`
- CORS configured for frontend security
- Validation at controller level

### Frontend (Vue + TypeScript)

#### Type Safety
- `strictNullChecks` and `verbatimModuleSyntax` enabled
- Type-only imports: `import type { TodoItem }`
- Proper component prop/emit typing:
  ```typescript
  interface Props { item: TodoItem; }
  interface Emits { (e: 'update', item: TodoItem): void; }
  const props = defineProps<Props>();
  ```

#### Component Structure
- Composition API with `<script setup>`
- Reactive state management with `ref()`, `reactive()`
- Separation of concerns: list vs. item components
- Proper error & loading state handling

#### Styling Consistency
- Scoped styles prevent CSS conflicts
- Consistent color scheme (Vue green `#42b983`)
- Accessible button styling and interactions

---

## Testing Strategy

### Backend Unit Tests (xUnit)

Tests validate **repository layer** (SRP boundary):

```csharp
[Fact]
public async Task AddAndGetById_ShouldReturnInsertedItem()
{
    var repo = new InMemoryTodoRepository();
    var item = new TodoItem { Title = "Test" };
    await repo.AddAsync(item);
    var fetched = await repo.GetByIdAsync(item.Id);
    Assert.NotNull(fetched);
    Assert.Equal("Test", fetched!.Title);
}
```

**Current Coverage**:
- ✅ Add and retrieval
- ✅ Update nonexistent items (failure case)
- ✅ Deletion

**Run tests**:
```bash
cd backend/ToDoApi.Tests
dotnet test
```

### Frontend Testing (Future)
Recommended: **Vitest** + **Vue Test Utils**
- Component rendering tests
- User interaction simulation
- API call mocking

---

## UX Consistency

### Design System

| Element | Style | Purpose |
|---------|-------|---------|
| Input field | `#ccc` border, `0.5rem` padding | Clear form focus |
| Add button | `#42b983` (Vue green) | Consistent branding |
| Delete button | `#c00` (red) | Clear danger action |
| Completed task | Strike-through, `#999` | Visual feedback |

### User Workflows

1. **Add To-Do**
   - Enter text → Press Enter or click Add
   - Button hover effect provides feedback
   - Input clears on success

2. **Complete To-Do**
   - Checkbox toggles completion
   - Strike-through applied immediately
   - API call reflects change

3. **Delete To-Do**
   - Red ✕ button clearly indicates danger
   - Item removed from list on success
   - Error message shows if deletion fails

### Error Handling
- Loading state displays during async operations
- Error messages show API failures
- Empty state message when no todos exist

---

## Project Structure

```
speckit-experiment/
├── backend/
│   ├── ToDoApi/
│   │   ├── Program.cs              # DI & middleware config
│   │   ├── Controllers/
│   │   │   └── TodosController.cs  # HTTP endpoints
│   │   ├── Interfaces/
│   │   │   └── ITodoRepository.cs  # Data access abstraction
│   │   ├── Models/
│   │   │   └── TodoItem.cs         # Domain model
│   │   └── Repositories/
│   │       └── InMemoryTodoRepository.cs  # Implementation
│   └── ToDoApi.Tests/
│       └── UnitTest1.cs            # Repository tests
│
└── frontend/
    ├── src/
    │   ├── App.vue                 # Root component
    │   ├── main.ts                 # Entry point
    │   ├── types.ts                # TypeScript interfaces
    │   ├── services/
    │   │   └── todoService.ts      # API client
    │   └── components/
    │       ├── TodoList.vue        # List container
    │       └── TodoItem.vue        # Item renderer
    ├── package.json
    └── vite.config.ts
```

---

## Running the Application

### Backend
```bash
cd backend/ToDoApi
dotnet run
# Server starts at https://localhost:5001
```

### Frontend (separate terminal)
```bash
cd frontend
npm run dev
# Dev server at http://localhost:5173
```

### Testing
```bash
cd backend/ToDoApi.Tests
dotnet test
# Expected: 4/4 tests pass
```

---

## Future Enhancements

### Backend
- [ ] Database persistence (EF Core)
- [ ] User authentication (JWT)
- [ ] Input validation (FluentValidation)
- [ ] Logging (Serilog)
- [ ] Integration tests

### Frontend
- [ ] Unit tests (Vitest + Vue Test Utils)
- [ ] E2E tests (Playwright)
- [ ] Dark mode toggle
- [ ] Pagination for large lists
- [ ] Edit inline functionality

### DevOps
- [ ] Docker containerization
- [ ] CI/CD pipeline (GitHub Actions)
- [ ] Azure deployment
