using BlazorWebRtc_Api.Controllers.Base;
using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Features.Queries.Users.GetAll;
using BlazorWebRtc_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers;

public class UserInfoController : BaseController
{
    public UserInfoController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetUserList()
    {
        GetAllUsersQuery getAllUsersQuery = new();
        ResponseModel response = await _mediator.Send(getAllUsersQuery);
        return Ok(response);
    }
}