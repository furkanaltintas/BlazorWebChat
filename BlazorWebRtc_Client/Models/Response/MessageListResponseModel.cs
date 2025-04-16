namespace BlazorWebRtc_Client.Models.Response;

public class MessageListResponseModel
{
    public DateTime CreateDate { get; set; }
    public String MessageContent { get; set; } = String.Empty;
    public Guid? SenderUserId { get; set; }
    public Guid? ReceiverUserId { get; set; }
}