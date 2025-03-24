using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain.Entities;

public class Message : BaseEntity
{
    public Guid MessageRoomId { get; set; }
    public Guid UserId { get; set; }
    public string MessageContent { get; set; } = string.Empty;

    public MessageRoom MessageRoom { get; set; }
    public User User { get; set; }
}