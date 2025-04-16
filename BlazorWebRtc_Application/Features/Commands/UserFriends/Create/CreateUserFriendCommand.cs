using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.UserFriends.Create;

public class CreateUserFriendCommand : IRequest<bool>
{
    public Guid RequesterId { get; set; } // İsteği yollayan
    public Guid ReceiverId { get; set; } // İsteği alan
}