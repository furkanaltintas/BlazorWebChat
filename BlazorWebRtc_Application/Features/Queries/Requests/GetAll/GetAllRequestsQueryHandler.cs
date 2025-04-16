using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Enums;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Queries.Requests.GetAll;

public class GetAllRequestsQueryHandler : IRequestHandler<GetAllRequestsQuery, ResponseModel>
{
    private readonly AppDbContext _context;

    public GetAllRequestsQueryHandler(AppDbContext context) { _context = context; }

    public async Task<ResponseModel> Handle(GetAllRequestsQuery request, CancellationToken cancellationToken)
    {
        List<GetRequestDto> requestDtos = await _context.Requests
            .Include(r => r.ReceiverUser)
            .Where(r => r.ReceiverUserId == request.UserId && r.Status == StatusEnum.Pending)
            .Select(r => new GetRequestDto
            {
                Id = r.Id,
                UserId = r.SenderUserId,
                UserName = r.SenderUser!.UserName,
                Email = r.SenderUser.Email,
                Profile = r.SenderUser.Profile!
            }).ToListAsync();

        return new() { IsSuccess = true, Data = requestDtos};
    }
}