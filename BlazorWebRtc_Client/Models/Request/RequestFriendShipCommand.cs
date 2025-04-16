namespace BlazorWebRtc_Client.Models.Request;

public class RequestFriendShipCommand
{
    public Guid ReceiverId { get; set; }
    public StatusEnum Status { get; set; }

    public RequestFriendShipCommand()
    {
        
    }

    public RequestFriendShipCommand(Guid receiverId, StatusEnum status)
    {
        ReceiverId = receiverId;
        Status = status;
    }
}

public enum StatusEnum
{
    Pending, // Bekliyor
    Accept, // Kabul Edildi
    Denied // Reddedildi
}