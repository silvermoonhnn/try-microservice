using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using notification_services.Application.UseCases.Notification.Command.Create;
using notification_services.Application.UseCases.Notification.Command.Delete;
using notification_services.Application.UseCases.Notification.Command.Update;
using notification_services.Application.UseCases.Notification.Queries.GetNotification;
using notification_services.Application.UseCases.Notification.Queries.GetNotificationLog;
using notification_services.Application.UseCases.Notification.Queries.GetNotifications;

namespace notification_services.Controllers
{
    [ApiController]
    [Route("notification")]

    public class NotificationController : ControllerBase
    {
        public IMediator _mediatr;

        public NotificationController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<ActionResult<GetDto>> Get(string include)
        {
            if(include == "logs")
            {
                return Ok(await _mediatr.Send(new GetLogQuery()));
            }
            else
            {
                return Ok(await _mediatr.Send(new GetsQuery()));
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateCommandDto>> Post(CreateCommand po)
        {
            return Ok(await _mediatr.Send(po));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetDto>> GetById(int id, string include)
        {
            return Ok(await _mediatr.Send(new GetQuery(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCommand up)
        {
            up.Data.Attributes.Notification_Id = id;

            return Ok(await _mediatr.Send(up));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotif(int id)
        {
            var command = new DeleteCommand(id);
            var result = await _mediatr.Send(command);
            return result != null ? (IActionResult)Ok(new { Message = "success" }) : NotFound(new { Message = "not found" });
        }
    }
}