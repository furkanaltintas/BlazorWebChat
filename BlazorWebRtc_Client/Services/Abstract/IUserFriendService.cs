using BlazorWebRtc_Client.Models.Response;

namespace BlazorWebRtc_Client.Services.Abstract;

public interface IUserFriendService
{
    Task<List<UserDTOResponseModel>> GetAllFriendsByUser();
}