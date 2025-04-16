using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Queries.UserFriends.GetAll;

public class GetAllUserFriendsQueryHandler : IRequestHandler<GetAllUserFriendsQuery, ResponseModel>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public GetAllUserFriendsQueryHandler(IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ResponseModel> Handle(GetAllUserFriendsQuery request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var friends = await _context.UserFriends.Include(u => u.ReceiverUser).Include(u => u.Requester).Where(u => u.RequesterId == userId || u.ReceiverUserId == userId).ToListAsync();

        List<UserFriendDto> userFriendDtos = new List<UserFriendDto>();

        UserFriendDto userFriendDto = new();
        foreach (var friend in friends)
        {
            if (userId == friend.RequesterId)
            {
                userFriendDto.UserId = friend.ReceiverUserId;
                userFriendDto.UserName = friend.ReceiverUser!.UserName;
                userFriendDto.Email = friend.ReceiverUser.Email;
                userFriendDto.Profile = friend.ReceiverUser.Profile!;
            }
            if (userId == friend.ReceiverUserId)
            {
                userFriendDto.UserId = friend.RequesterId;
                userFriendDto.UserName = friend.Requester!.UserName;
                userFriendDto.Email = friend.Requester.Email;
                userFriendDto.Profile = friend.Requester.Profile!;
            }
            userFriendDtos.Add(userFriendDto);
            userFriendDto = new();
        }

        return new() { IsSuccess = true, Data = userFriendDtos };
    }
}