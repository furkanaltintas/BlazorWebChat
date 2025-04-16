using BlazorWebRtc_Application.Models;
using MediatR;

namespace BlazorWebRtc_Application.Features.Queries.Users.GetAll;

public record GetAllUsersQuery() : IRequest<ResponseModel>;
