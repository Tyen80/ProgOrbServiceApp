using ServiceApp.Application.Abstractions.RequestHandling;

namespace ServiceApp.Application.Tasks.GetAllTasks;
public class GetAllTasksQuery : IQuery<List<TaskResponse>>
{
}
