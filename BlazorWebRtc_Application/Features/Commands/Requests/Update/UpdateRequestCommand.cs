using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Enums;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Requests.Update;

public class UpdateRequestCommand : IRequest<ResponseModel>
{
    public Guid RequestId { get; set; }
    public StatusEnum Status { get; set; }
}