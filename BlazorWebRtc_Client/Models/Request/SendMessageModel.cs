namespace BlazorWebRtc_Client.Models.Request;

public class SendMessageModel
{
    public String ReceiverUserId { get; set; } = String.Empty;
    public String MessageContent { get; set; } = String.Empty;
}