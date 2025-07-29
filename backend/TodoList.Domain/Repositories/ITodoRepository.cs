using System;

public interface ITodoRepository
{
	Task CreateTodoItemAsync(TodoItem item);

    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetTodoItem(Guid id);
    Task UpdateAsync(TodoItem updatedItem);
    Task MarkCompleteAsync(Guid id);
}
