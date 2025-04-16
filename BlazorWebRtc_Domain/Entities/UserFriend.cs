using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain.Entities;

public class UserFriend : BaseEntity
{
    public Guid RequesterId { get; set; }
    public Guid ReceiverUserId { get; set; }

    public User? Requester { get; set; }
    public User? ReceiverUser { get; set; }

    public UserFriend() { }

    public UserFriend(Guid requesterId, Guid receiverId)
    {
        RequesterId = requesterId;
        ReceiverUserId = receiverId;
    }
}