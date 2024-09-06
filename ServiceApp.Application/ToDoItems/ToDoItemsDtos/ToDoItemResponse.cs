namespace ServiceApp.Application.ToDoItems.ToDoItemsDtos;
public record struct ToDoItemResponse(int Id, int TaskId, bool IsComplete, bool IsApproved, DateTime DueDate, TaskToDo Task, string UserId, string FamilyId, string UserName);
