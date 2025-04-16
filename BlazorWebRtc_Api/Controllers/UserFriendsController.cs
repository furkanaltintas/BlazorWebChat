using BlazorWebRtc_Api.Controllers.Base;
using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Features.Commands.UserFriends.Create;
using BlazorWebRtc_Application.Features.Commands.UserFriends.Delete;
using BlazorWebRtc_Application.Features.Queries.UserFriends.GetAll;
using BlazorWebRtc_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers;

public class UserFriendsController : BaseController
{
    public UserFriendsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetFriendship()
    {
        GetAllUserFriendsQuery getAllUserFriendsQuery = new();
        ResponseModel response = await _mediator.Send(getAllUserFriendsQuery);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddUserFriend(CreateUserFriendCommand userFriendCommand)
    {
        Boolean result = await _mediator.Send(userFriendCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserFriend(DeleteUserFriendCommand deleteUserFriendCommand)
    {
        Boolean result = await _mediator.Send(deleteUserFriendCommand);
        return Ok(result);
    }
}