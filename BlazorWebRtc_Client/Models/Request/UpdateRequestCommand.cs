namespace BlazorWebRtc_Client.Models.Request;

public class UpdateRequestCommand
{
    public Guid RequestId { get; set; }
    public StatusEnum Status { get; set; }
}