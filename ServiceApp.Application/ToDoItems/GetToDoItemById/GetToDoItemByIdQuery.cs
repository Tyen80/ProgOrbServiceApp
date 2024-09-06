using ServiceApp.Application.ToDoItems.ToDoItemsDtos;

namespace ServiceApp.Application.ToDoItems.GetToDoItemById;
public class GetToDoItemByIdQuery : IQuery<ToDoItemResponse>
{
    public int Id { get; set; }

}

