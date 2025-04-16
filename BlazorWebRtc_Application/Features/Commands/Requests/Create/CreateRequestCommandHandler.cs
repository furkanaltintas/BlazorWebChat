using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Commands.Requests.Create;

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, ResponseModel>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public CreateRequestCommandHandler(IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ResponseModel> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.ReceiverId);
        if (user is null) return new ResponseModel { IsSuccess = false, Data = false, Message = "Kullanıcı bulunamadı" };

        Guid senderId = Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        Boolean isRequestExists = await _context.Requests.AnyAsync(
            (r => r.SenderUserId == senderId && r.ReceiverUserId == request.ReceiverId ||
            r.SenderUserId == request.ReceiverId && r.ReceiverUserId == senderId)); // Böyle bir istek var mı ?

        if (isRequestExists) return new ResponseModel { IsSuccess = false, Data = false, Message = "Böyle bir istek zaten var" };

        Request modelRequest = new()
        {
            ReceiverUserId = request.ReceiverId,
            SenderUserId = senderId,
            Status = request.Status
        };
        await _context.Requests.AddAsync(modelRequest);
        return await _context.SaveChangesAsync() > 0 ? new ResponseModel { IsSuccess = true, Data = true } : new ResponseModel { IsSuccess = false, Data = false };
    }
}