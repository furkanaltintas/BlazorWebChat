namespace BlazorWebRtc_Client.Models.Request;

public class GetRequestFriendshipList
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
}