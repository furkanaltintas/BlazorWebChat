using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;

namespace BlazorWebRtc_Client.Services.Abstract;

public interface IAccountService
{
    Task<UserResponseModel> SignUp(RegisterCommand registerCommand);
    Task<Boolean> SignIn(LoginCommand loginCommand);
    Task Logout();
}