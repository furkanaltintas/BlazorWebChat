using BlazorWebRtc_Domain.Common;
using BlazorWebRtc_Domain.Enums;

namespace BlazorWebRtc_Domain.Entities;

public class Request : BaseEntity
{
    public Guid SenderUserId { get; set; }
    public Guid ReceiverUserId { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Pending;

    public User? SenderUser { get; set; }
    public User? ReceiverUser { get; set; }
}