using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;

    public virtual List<UserFriend> Friends { get; set; } = new List<UserFriend>();
}