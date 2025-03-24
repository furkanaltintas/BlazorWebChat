using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain.Entities;

public class MessageRoom : BaseEntity
{
    public Guid? SenderUserId { get; set; }
    public Guid ReceiverUserId { get; set; }

    public User SenderUser { get; set; }
    public User ReceiverUser { get; set; }
    public virtual List<Message> Messages { get; set; } = new List<Message>();
}