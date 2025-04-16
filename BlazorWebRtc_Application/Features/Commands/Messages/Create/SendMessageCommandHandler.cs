using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Commands.Messages.Create;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ResponseModel>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public SendMessageCommandHandler(IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ResponseModel> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        String? userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (String.IsNullOrEmpty(userId)) return new() { IsSuccess = false };

        MessageRoom messageRoom = new()
        {
            SenderUserId = Guid.Parse(userId),
            ReceiverUserId = request.ReceiverUserId,
            MessageContent = request.MessageContent
        };

        await _context.MessageRooms.AddAsync(messageRoom);
        await _context.SaveChangesAsync();
        return new() { IsSuccess = true };
    }
}