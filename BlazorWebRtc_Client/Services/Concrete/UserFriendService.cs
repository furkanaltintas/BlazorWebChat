using BlazorWebRtc_Client.Models.Response;
using BlazorWebRtc_Client.Services.Abstract;
using Newtonsoft.Json;

namespace BlazorWebRtc_Client.Services.Concrete
{
    public class UserFriendService : IUserFriendService
    {
        private readonly HttpClient _httpClient;

        public UserFriendService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<List<UserDTOResponseModel>> GetAllFriendsByUser()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/UserFriends");
            string content = await response.Content.ReadAsStringAsync();
            ResponseModel? deserialize = JsonConvert.DeserializeObject<ResponseModel>(content);
            if (response.IsSuccessStatusCode)
            {
                var desObject = JsonConvert.DeserializeObject<List<UserDTOResponseModel>>(deserialize!.Data.ToString()!);
                return desObject!;
            }

            return new();
        }
    }
}
