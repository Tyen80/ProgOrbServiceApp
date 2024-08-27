﻿
using ServiceApp.Domain.ToDoItems;

namespace ServiceApp.Application.ToDoItems.CreateToDoItem;
public class CreateToDoItemCommandHandler : ICommandHandler<CreateToDoItemCommand, ToDoItemResponse>
{
    private readonly IToDoItemRepository _toDoItemRepository;

    public CreateToDoItemCommandHandler(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    public async Task<Result<ToDoItemResponse>> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newToDoItem = request.Adapt<ToDoItem>();
            var createdToDoItem = await _toDoItemRepository.CreateAsync(newToDoItem);
            return createdToDoItem.Adapt<ToDoItemResponse>();
        }
        catch (Exception)
        {
            return Result<ToDoItemResponse>.Fail<ToDoItemResponse>("ToDoItem not created");
        }
    }
}
