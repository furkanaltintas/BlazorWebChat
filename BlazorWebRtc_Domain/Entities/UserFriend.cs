using BlazorWebRtc_Domain.Common;

namespace BlazorWebRtc_Domain.Entities;

public class UserFriend : BaseEntity
{
    public virtual List<User> Users { get; set; } = new List<User>();
}