using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Messages.Create;

public class SendMessageCommand : IRequest<ResponseModel>
{
    public Guid ReceiverUserId { get; set; }
    public string MessageContent { get; set; } = string.Empty;
}