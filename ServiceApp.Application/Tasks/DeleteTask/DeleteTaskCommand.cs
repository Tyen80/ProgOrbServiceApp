using ServiceApp.Application.Abstractions.RequestHandling;

namespace ServiceApp.Application.Tasks.DeleteTask;
public class DeleteTaskCommand : ICommand
{
    public int Id { get; set; }
}
