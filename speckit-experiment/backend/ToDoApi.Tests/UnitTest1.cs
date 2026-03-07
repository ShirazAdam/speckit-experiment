using System;
using System.Threading.Tasks;
using ToDoApi.Models;
using ToDoApi.Repositories;
using Xunit;

namespace ToDoApi.Tests;

public class InMemoryTodoRepositoryTests
{
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

    [Fact]
    public async Task Update_Nonexistent_ShouldReturnFalse()
    {
        var repo = new InMemoryTodoRepository();
        var item = new TodoItem { Id = Guid.NewGuid(), Title = "X" };
        var result = await repo.UpdateAsync(item);
        Assert.False(result);
    }

    [Fact]
    public async Task Delete_ShouldRemoveItem()
    {
        var repo = new InMemoryTodoRepository();
        var item = new TodoItem { Title = "ToRemove" };
        await repo.AddAsync(item);
        var deleted = await repo.DeleteAsync(item.Id);
        Assert.True(deleted);
        var fetched = await repo.GetByIdAsync(item.Id);
        Assert.Null(fetched);
    }
}
