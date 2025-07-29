using System;
using TodoList.Application.DTOs.Request;
using TodoList.Application.DTOs.Response;

public interface ITodoService
{
	Task<CreateTodoItemResponse> CreateTodoItemAsync(CreateTodoItemRequest createTodoItemRequest);

    Task<IEnumerable<CreateTodoItemResponse>> GetTodoListItemAsync();

    Task<UpdateTodoItemResponse> UpdateTodoItemAsync(UpdateTodoItemRequest request);
    Task<bool> DeleteTodoItemAsync(Guid Id);
    Task<bool> MarkTodoItemAsCompleteAsync(Guid id);
    Task<GetTodoIetmResponse> GetTodoItemAsync(Guid id);

}
