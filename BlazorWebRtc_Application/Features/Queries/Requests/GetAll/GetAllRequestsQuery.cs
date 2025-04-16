using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Features.Queries.Requests.GetAll;

public record GetAllRequestsQuery(Guid UserId) : IRequest<ResponseModel>;