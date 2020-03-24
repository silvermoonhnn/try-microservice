using MediatR;

namespace notification_services.Application.UseCases.Notification.Command.Delete
{
    public class DeleteCommand : IRequest<DeleteCommandDto>
    {
        public int Id { get; set; }

        public DeleteCommand(int id)
        {
            Id = id;
        }
    }
}