using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using payment_services.Application.UseCases.Notification.Command.Delete;
using payment_services.Application.UseCases.Payment.Command.Create;
using payment_services.Application.UseCases.Payment.Command.Update;
using payment_services.Application.UseCases.Payment.Queries.GetPayment;
using payment_services.Application.UseCases.Payment.Queries.GetPayments;

namespace payment_services.Controllers
{
    [ApiController]
    [Route("payment")]

    public class PaymentController : ControllerBase
    {
        public IMediator _mediatr;
        
        public PaymentController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCommandDto>> Post([FromBody] CreateCommand po)
        {
            return Ok(await _mediatr.Send(po));
        }

        [HttpGet]
        public async Task<ActionResult<GetsDto>> Gets()
        {
            return Ok(await _mediatr.Send(new GetsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetDto>> GetById(int id)
        {
            return Ok(await _mediatr.Send(new GetQuery(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCommand up)
        {
            up.Data.Attributes.Id = id;

            return Ok(await _mediatr.Send(up));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCommand(id);
            var result = await _mediatr.Send(command);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }
    }
}