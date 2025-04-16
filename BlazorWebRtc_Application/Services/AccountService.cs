using BlazorWebRtc_Application.Features.Commands.Account.Login;
using BlazorWebRtc_Application.Features.Commands.Account.Register;
using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Entities;
using MediatR;

namespace BlazorWebRtc_Application.Services;

public class AccountService : IAccountService
{
    private readonly IMediator _mediator;
    public AccountService(IMediator mediator) { _mediator = mediator; }

    public async Task<ResponseModel> SignIn(LoginCommand loginCommand)
    {
        ResponseModel responseModel = await _mediator.Send(loginCommand);
        return responseModel;
    }

    public async Task<ResponseModel> SignUp(RegisterCommand registerCommand)
    {
        User user = await _mediator.Send(registerCommand);

        if (user == null) new ResponseModel { IsSuccess = false};
        return new ResponseModel { Message = "İşlem başarılı", IsSuccess = true, Data = user! };
    }
}