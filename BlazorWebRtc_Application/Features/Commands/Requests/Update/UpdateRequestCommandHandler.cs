using BlazorWebRtc_Application.Features.Commands.UserFriends.Create;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Domain.Enums;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Commands.Requests.Update;

public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, ResponseModel>
{
    private readonly AppDbContext _context;
    public UpdateRequestCommandHandler(AppDbContext context) { _context = context; }

    public async Task<ResponseModel> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        Request? requestEntity = await _context.Requests.FirstOrDefaultAsync(r => r.Id == request.RequestId);
        if (requestEntity == null) return new() { IsSuccess = false };

        requestEntity.Status = request.Status;

        if (request.Status == StatusEnum.Accept) // Arkadaşlık ekleme
        {
            await _context.UserFriends.AddAsync(new UserFriend
            {
                ReceiverUserId = requestEntity.ReceiverUserId, // Alıcı
                RequesterId = requestEntity.SenderUserId // Gönderen (İsteği yapan)
            });
        }

        await _context.SaveChangesAsync();
        return new() { IsSuccess = true };
    }
}