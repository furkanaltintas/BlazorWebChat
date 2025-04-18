using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Features.Queries.Messages.GetAll;
using BlazorWebRtc_Application.Hubs;
using BlazorWebRtc_Application.Interface.Services.Manager;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Commands.Messages.Create;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ResponseModel>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConnectionManager _connectionManager;
    private readonly IHubContext<UserHub> _hubContext;
    private readonly AppDbContext _context;
    private readonly IMediator _mediator;

    public SendMessageCommandHandler(IHttpContextAccessor httpContextAccessor, IConnectionManager connectionManager, IHubContext<UserHub> hubContext, AppDbContext context, IMediator mediator)
    {
        _httpContextAccessor = httpContextAccessor;
        _connectionManager = connectionManager;
        _hubContext = hubContext;
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResponseModel> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        String? senderUserId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (String.IsNullOrEmpty(senderUserId)) return new() { IsSuccess = false };

        MessageRoom messageRoom = new()
        {
            SenderUserId = Guid.Parse(senderUserId),
            ReceiverUserId = request.ReceiverUserId,
            MessageContent = request.MessageContent
        };

        await _context.MessageRooms.AddAsync(messageRoom);
        await _context.SaveChangesAsync();

        List<MessageDto> messageDtos = new();

        List<String> _userIds = new() { senderUserId, request.ReceiverUserId.ToString() };
        List<String> userIds = _connectionManager.GetConnectionByUserId(_userIds);

        GetAllMessagesQuery query = new();
        query.MessageUserId = request.ReceiverUserId;
        ResponseModel obj = await _mediator.Send(query, cancellationToken);
        String serializeMessages = JsonConvert.SerializeObject(obj);


        await _hubContext.Clients.Clients(userIds).SendAsync("ReceiveMessage", serializeMessages);

        return new() { IsSuccess = true };
    }
}