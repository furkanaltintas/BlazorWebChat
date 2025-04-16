using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Enums;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Requests.Create;

public class CreateRequestCommand : IRequest<ResponseModel>
{
    public Guid ReceiverId { get; set; }
    public StatusEnum Status { get; set; }
}