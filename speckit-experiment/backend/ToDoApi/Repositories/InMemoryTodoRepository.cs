using ToDoApi.Interfaces;
using ToDoApi.Models;

namespace ToDoApi.Repositories
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _items = new();

        public Task AddAsync(TodoItem item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var existing = _items.FirstOrDefault(x => x.Id == id);
            if (existing is null)
                return Task.FromResult(false);

            _items.Remove(existing);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TodoItem>>(_items);
        }

        public Task<TodoItem?> GetByIdAsync(Guid id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        public Task<bool> UpdateAsync(TodoItem item)
        {
            var index = _items.FindIndex(x => x.Id == item.Id);
            if (index == -1)
                return Task.FromResult(false);

            _items[index] = item;
            return Task.FromResult(true);
        }
    }
}