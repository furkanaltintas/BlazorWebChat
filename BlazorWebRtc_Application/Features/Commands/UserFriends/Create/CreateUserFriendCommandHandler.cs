using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Persistence.Context;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.UserFriends.Create;

public class CreateUserFriendCommandHandler : IRequestHandler<CreateUserFriendCommand, bool>
{
    private readonly AppDbContext _context;

    public CreateUserFriendCommandHandler(AppDbContext context) { _context = context; }

    public async Task<bool> Handle(CreateUserFriendCommand request, CancellationToken cancellationToken)
    {
        UserFriend userFriend = new(request.RequesterId, request.ReceiverId);
        await _context.UserFriends.AddAsync(userFriend, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}