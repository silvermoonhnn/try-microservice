using MediatR;

namespace payment_services.Application.UseCases.Payment.Queries.GetPayment
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