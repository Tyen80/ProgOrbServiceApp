using ServiceApp.Application.Abstractions.RequestHandling;

namespace ServiceApp.Application.Tasks.GetTaskById;
public class GetTaskByIdQuery : IQuery<TaskResponse>
{
    public int Id { get; set; }
}
