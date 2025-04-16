using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Account.Login;

// Model yapımız
public class LoginCommand : IRequest<ResponseModel>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}