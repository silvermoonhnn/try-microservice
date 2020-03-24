using MediatR;

namespace notification_services.Application.UseCases.Notification.Queries.GetNotification
{
    public class GetQuery : IRequest<GetDto>
    {
        public int Id { get; set; }

        public GetQuery(int id)
        {
            Id = id;
        }
    }
}