namespace ServiceApp.Application.ToDoItems.GetToDoItemById;
public class GetToDoItemByIdQuery : IQuery<ToDoItemResponse>
{
    public int Id { get; set; }

}

