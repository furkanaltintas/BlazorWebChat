using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Features.Queries.Messages.GetAll;

public class GetAllMessagesQuery : IRequest<ResponseModel>
{
    public Guid MessageUserId { get; set; }
}
