using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain.Entities;

public class MessageRoom : BaseEntity
{
    public Guid? SenderUserId { get; set; }
    public Guid ReceiverUserId { get; set; }
    public string MessageContent { get; set; } = string.Empty;

    public User? SenderUser { get; set; }
    public User? ReceiverUser { get; set; }
}