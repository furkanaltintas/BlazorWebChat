using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Hubs;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Queries.Messages.GetAll;

class GetAllMessagesHandler : IRequestHandler<GetAllMessagesQuery, ResponseModel>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHubContext<UserHub> _hubContext;
    private readonly AppDbContext _context;

    public GetAllMessagesHandler(IHttpContextAccessor httpContextAccessor, IHubContext<UserHub> hubContext, AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _hubContext = hubContext;
        _context = context;
    }

    public async Task<ResponseModel> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
    {
        String? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid parseUserId = userId is not null ? Guid.Parse(userId) : Guid.Empty;

        List<MessageDto> messageDtos = await _context.MessageRooms
            .Where(
            m => m.SenderUserId == parseUserId && m.ReceiverUserId == request.MessageUserId ||
            m.ReceiverUserId == parseUserId && m.SenderUserId == request.MessageUserId).Select(m => new MessageDto
            {
                SenderUserId = m.SenderUserId,
                ReceiverUserId = m.ReceiverUserId,
                MessageContent = m.MessageContent,
                CreateDate = m.CreatedDate
            }).OrderBy(m => m.CreateDate).ToListAsync();

        return messageDtos.Any() ? new() { IsSuccess = true, Data = messageDtos} : new();
    }
}