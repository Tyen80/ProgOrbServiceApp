namespace ServiceApp.Application.Tasks.GetAllTaskByUserId;
public class GetAllTaskByUserIdQuery : IQuery<List<TaskResponse>>
{
    public int UserId { get; set; }
}
