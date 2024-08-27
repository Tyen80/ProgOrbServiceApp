namespace ServiceApp.Application.ToDoItems;
public record struct ToDoItemResponse(int Id, int TaskId, bool IsComplete, DateTime DueDate, TaskToDo Task);
