namespace BlazorWebRtc_Application.Dtos;

public class MessageDto
{
    public DateTime CreateDate { get; set; }
    public String MessageContent { get; set; } = String.Empty;
    public Guid? SenderUserId { get; set; }
    public Guid? ReceiverUserId { get; set; }
}