using BlazorWebRtc_Api.Controllers.Base;
using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Features.Commands.Requests.Create;
using BlazorWebRtc_Application.Features.Commands.Requests.Update;
using BlazorWebRtc_Application.Features.Queries.Requests.GetAll;
using BlazorWebRtc_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers;

public class RequestsController : BaseController
{
    public RequestsController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> SendRequest(CreateRequestCommand requestCommand)
    {
        ResponseModel result = await _mediator.Send(requestCommand);
        return Ok(result);
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFriendRequest([FromRoute] string userId)
    {
        GetAllRequestsQuery requestQuery = new(Guid.Parse(userId));
        ResponseModel response = await _mediator.Send(requestQuery);
        return Ok(response);
    }


    [HttpPut]
    public async Task<IActionResult> UpdateRequest(UpdateRequestCommand updateRequestCommand)
    {
        ResponseModel response = await _mediator.Send(updateRequestCommand);
        return Ok(response);
    }
}