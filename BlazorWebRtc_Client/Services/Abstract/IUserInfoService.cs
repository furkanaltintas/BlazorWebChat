using BlazorWebRtc_Client.Models.Response;

namespace BlazorWebRtc_Client.Services.Abstract
{
    public interface IUserInfoService
    {
        Task<List<UserDTOResponseModel>> GetAllUsersAsync();
    }
}
