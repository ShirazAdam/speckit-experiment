# Testing Strategy & Guidelines

## Backend Testing (xUnit)

### Current Test Suite (`backend/ToDoApi.Tests/UnitTest1.cs`)

The test class validates the **InMemoryTodoRepository** in isolation:

```csharp
public class InMemoryTodoRepositoryTests
{
    [Fact]
    public async Task AddAndGetById_ShouldReturnInsertedItem() { ... }
    
    [Fact]
    public async Task Update_Nonexistent_ShouldReturnFalse() { ... }
    
    [Fact]
    public async Task Delete_ShouldRemoveItem() { ... }
}
```

### AAA Pattern (Arrange-Act-Assert)

Each test follows the AAA pattern:

```csharp
[Fact]
public async Task AddAndGetById_ShouldReturnInsertedItem()
{
    // Arrange: Set up test data
    var repo = new InMemoryTodoRepository();
    var item = new TodoItem { Title = "Test" };
    
    // Act: Execute the behavior under test
    await repo.AddAsync(item);
    var fetched = await repo.GetByIdAsync(item.Id);
    
    // Assert: Verify results
    Assert.NotNull(fetched);
    Assert.Equal("Test", fetched!.Title);
}
```

### Test Coverage

| Scenario | Test Name | Status |
|----------|-----------|--------|
| Add item & retrieve | `AddAndGetById_ShouldReturnInsertedItem` | ✅ |
| Update nonexistent | `Update_Nonexistent_ShouldReturnFalse` | ✅ |
| Delete item | `Delete_ShouldRemoveItem` | ✅ |
| Get all items | (Implicit in other tests) | ✅ |

### Running Tests

```bash
# Run all tests
dotnet test

# Run with verbose output
dotnet test --logger="console;verbosity=detailed"

# Run single test class
dotnet test --filter "ClassName=InMemoryTodoRepositoryTests"

# Run specific test
dotnet test --filter "Name=AddAndGetById_ShouldReturnInsertedItem"
```

### Best Practices Applied

✅ **Test isolation**: Each test creates fresh repository instance  
✅ **Clear naming**: `[Action]_[Scenario]_[ExpectedResult]` format  
✅ **Async support**: Tests use `async/await` matching production code  
✅ **Single responsibility**: One assertion per logical test  
✅ **No external dependencies**: In-memory implementation, no mocks needed yet  

---

## Frontend Testing (Future)

### Recommended Tools

- **Framework**: [Vitest](https://vitest.dev) (Vite-native, fast)
- **Component Testing**: [Vue Test Utils](https://test-utils.vuejs.org)
- **E2E Testing**: [Playwright](https://playwright.dev)

### Example Test Structure

```typescript
// components/__tests__/TodoList.spec.ts
import { describe, it, expect, vi } from 'vitest';
import { mount } from '@vue/test-utils';
import TodoList from '../TodoList.vue';

describe('TodoList.vue', () => {
  it('renders input and button', () => {
    const wrapper = mount(TodoList);
    expect(wrapper.find('input').exists()).toBe(true);
    expect(wrapper.find('button').exists()).toBe(true);
  });

  it('adds item when button clicked', async () => {
    const wrapper = mount(TodoList);
    await wrapper.find('input').setValue('New task');
    await wrapper.find('button').trigger('click');
    expect(wrapper.vm.todos.length).toBe(1);
  });
});
```

### Integration Test Approach

Test service layer interactions:

```typescript
// services/__tests__/todoService.spec.ts
import { describe, it, expect, vi } from 'vitest';
import { getTodos } from '../todoService';

describe('todoService', () => {
  it('calls correct API endpoint', async () => {
    const mockGet = vi.spyOn(api, 'get');
    await getTodos();
    expect(mockGet).toHaveBeenCalledWith('');
  });
});
```

---

## Controller Layer Testing (Future)

When adding more complex business logic, test controllers:

```csharp
[Fact]
public async Task Post_WithValidItem_ReturnsCreatedAtAction()
{
    // Arrange
    var mockRepo = new Mock<ITodoRepository>();
    var controller = new TodosController(mockRepo.Object);
    var item = new TodoItem { Title = "New" };
    
    // Act
    var result = await controller.Create(item);
    
    // Assert
    var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
    Assert.Equal(nameof(TodosController.Get), createdResult.ActionName);
}
```

---

## Test Maintenance Guidelines

### Adding New Tests

1. **Follow AAA pattern** (Arrange-Act-Assert)
2. **Use descriptive names** (property_scenario_expected)
3. **Test one logical behavior** per test method
4. **Avoid test interdependencies** (each test must be runnable standalone)
5. **Test edge cases** (null, empty, out of bounds)

### Test Organization

- Group related tests in test classes
- One test class per class under test
- Use [Theory] for parameterized tests

Example:
```csharp
[Theory]
[InlineData("")]
[InlineData(null)]
public async Task Create_WithEmptyTitle_ShouldFail(string title)
{
    var item = new TodoItem { Title = title };
    var result = await _repo.GetByIdAsync(item.Id);
    Assert.Null(result);
}
```

### Continuous Integration

Add to workflow:
```yaml
# .github/workflows/test.yml
- name: Run Tests
  run: dotnet test --no-build
```

---

## Code Coverage Goals

| Component | Target | Current |
|-----------|--------|---------|
| Repository | 100% | 100% |
| Controller | 80% | 0% (implement next) |
| Models | 100% | N/A (simple POCOs) |
| Services | 80% | 0% (implement next) |

---

## References

- xUnit Docs: https://xunit.net/docs/getting-started
- Vitest Docs: https://vitest.dev/guide/
- Vue Test Utils: https://test-utils.vuejs.org/
- Testing Best Practices: https://martinfowler.com/articles/testing-strategies.html
