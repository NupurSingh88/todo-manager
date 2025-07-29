using Microsoft.Extensions.Caching.Memory;
using System;

public class TodoRepository : ITodoRepository
{
    private readonly IMemoryCache _cache;
    private const string CacheKey = "todo_items";
    private readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    public TodoRepository(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }

    private Dictionary<Guid, TodoItem> GetInternalDictionary()
    {
        return _cache.GetOrCreate(CacheKey, entry =>
        {
            entry.SlidingExpiration = CacheDuration;
            return new Dictionary<Guid, TodoItem>();
        })!;
    }

    public async Task CreateTodoItemAsync(TodoItem todoItem)
    {
        var dict = GetInternalDictionary();
        dict[todoItem.ID] = todoItem;
        _cache.Set(CacheKey, dict, CacheDuration);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        var dict = GetInternalDictionary();
        return dict.Values.ToList();
    }

    public async Task<TodoItem?> GetTodoItem(Guid id)
    {
        var dict = GetInternalDictionary();
        dict.TryGetValue(id, out var item);
        return await Task.FromResult(item);
    }

    public async Task UpdateAsync(TodoItem updatedItem)
    {
        var dict = GetInternalDictionary();
        if (dict.ContainsKey(updatedItem.ID))
        {
            dict[updatedItem.ID] = updatedItem;
            _cache.Set(CacheKey, dict, CacheDuration);
        }
        await Task.CompletedTask;
    }

    public async Task MarkCompleteAsync(Guid id)
    {
        var dict = GetInternalDictionary();
        if (dict.TryGetValue(id, out var item))
        {
            item.IsCompleted = true;
            _cache.Set(CacheKey, dict, CacheDuration);
        }
        await Task.CompletedTask;
    }
}


