namespace BlazorWebRtc_Client.Models.Response;

public class UserDTOResponseModel
{
    public Guid UserId { get; set; }
    public String UserName { get; set; } = String.Empty;
    public String Email { get; set; } = String.Empty;
    public String Profile { get; set; } = String.Empty;

    public Boolean IsOnline { get; set; } = false;
}