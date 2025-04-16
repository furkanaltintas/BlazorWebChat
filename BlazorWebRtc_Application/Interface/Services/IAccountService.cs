using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Models;

namespace BlazorWebRtc_Application.Interface.Services;

public interface IAccountService 
{
    Task<ResponseModel> SignIn(LoginCommand loginCommand);
    Task<ResponseModel> SignUp(RegisterCommand registerCommand);
}