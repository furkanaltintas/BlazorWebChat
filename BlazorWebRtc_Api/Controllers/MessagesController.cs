using BlazorWebRtc_Api.Controllers.Base;
using BlazorWebRtc_Application.Features.Commands.Messages.Create;
using BlazorWebRtc_Application.Features.Queries.Messages.GetAll;
using BlazorWebRtc_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers
{
    public class MessagesController : BaseController
    {
        public MessagesController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages(Guid messageUserId)
        {
            GetAllMessagesQuery getAllMessagesQuery =  new() { MessageUserId = messageUserId };
            ResponseModel response = await _mediator.Send(getAllMessagesQuery);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessage(SendMessageCommand sendMessageCommand)
        {
            ResponseModel response = await _mediator.Send(sendMessageCommand);
            return Ok(response);
        }
    }
}
