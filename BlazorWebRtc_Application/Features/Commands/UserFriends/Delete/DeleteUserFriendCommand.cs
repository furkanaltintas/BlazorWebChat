using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.UserFriends.Delete;

public class DeleteUserFriendCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}