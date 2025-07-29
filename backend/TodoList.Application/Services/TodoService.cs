using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Xml.Linq;
using TodoList.Application.DTOs.Request;
using TodoList.Application.DTOs.Response;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
	public TodoService(ITodoRepository todoRepository)
	{
        _todoRepository = todoRepository;

    }

    public async Task<CreateTodoItemResponse> CreateTodoItemAsync(CreateTodoItemRequest createTodoItemRequest)
	{
        if(string.IsNullOrEmpty(createTodoItemRequest.Name))
        {
            throw new ArgumentNullException("Name cannot be empty");
        }

        
        TodoItem item = TodoItem.Create(createTodoItemRequest.Name, createTodoItemRequest.DueDate);
        await _todoRepository.CreateTodoItemAsync(item);
        CreateTodoItemResponse response = new CreateTodoItemResponse()
        {
            Id = item.ID,
            Name = item.Name,
            DueDate = item.DueDate,
        };

        return response;
    }

    public async Task<IEnumerable<CreateTodoItemResponse>> GetTodoListItemAsync()
    {
        
        var todoItems = await _todoRepository.GetAllAsync();

        var response = todoItems
            .Select(item => new CreateTodoItemResponse
            {
                Id = item.ID,
                Name = item.Name,
                DueDate = item.DueDate
            });

        return response;
    }

    public async Task<UpdateTodoItemResponse> UpdateTodoItemAsync(UpdateTodoItemRequest request)
    {
        var todoItem = await _todoRepository.GetTodoItem(request.Id);
        if(todoItem == null)
        {
            throw new InvalidOperationException("Invalid Todo Id");
        }

        todoItem.Update(request.Name, request.DueDate);
        await _todoRepository.UpdateAsync(todoItem);
        return new UpdateTodoItemResponse()
        {
            Id = todoItem.ID,
            Name = todoItem.Name,
            DueDate = todoItem.DueDate,
        };
    }

    public async Task<bool> DeleteTodoItemAsync(Guid Id)
    {
        var todoItem = await _todoRepository.GetTodoItem(Id);
        if (todoItem == null)
        {
            throw new InvalidOperationException("Invalid Todo Id");
        }
        todoItem.Delete();
        await _todoRepository.UpdateAsync(todoItem);
        return true;

    }

    public async Task<bool> MarkTodoItemAsCompleteAsync(Guid id)
    {
        var todoItem = await _todoRepository.GetTodoItem(id);
        if (todoItem == null)
        {
            throw new InvalidOperationException("Invalid Todo Id");
        }
        todoItem.MarkComplete();
        await _todoRepository.UpdateAsync(todoItem);
        return true;
    }

    public async Task<GetTodoIetmResponse> GetTodoItemAsync(Guid id)
    {
        var todoItem = await _todoRepository.GetTodoItem(id);
        if (todoItem == null)
        {
            throw new InvalidOperationException("Invalid Todo Id");
        }
        return new GetTodoIetmResponse()
        {
            ID = todoItem.ID,
            Name = todoItem.Name,
            DueDate = todoItem.DueDate,
            CompletedDate = todoItem.CompletedDate,
            IsCompleted = todoItem.IsCompleted
        };
    }
}
