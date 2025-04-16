using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Persistence.Context;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.UserFriends.Delete;

public class DeleteUserFriendCommandHandler : IRequestHandler<DeleteUserFriendCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteUserFriendCommandHandler(AppDbContext context) {  _context = context; }

    public async Task<bool> Handle(DeleteUserFriendCommand request, CancellationToken cancellationToken)
    {
        UserFriend? userFriend = await _context.UserFriends.FindAsync(request.Id);
        if (userFriend is null) return false;

        _context.UserFriends.Remove(userFriend);
        await _context.SaveChangesAsync();
        return true;
    }
}