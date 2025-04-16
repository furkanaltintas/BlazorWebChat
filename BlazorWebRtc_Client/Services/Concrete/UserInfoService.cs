using Blazored.LocalStorage;
using BlazorWebRtc_Client.Constants;
using BlazorWebRtc_Client.Models.Response;
using BlazorWebRtc_Client.Services.Abstract;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BlazorWebRtc_Client.Services.Concrete
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public UserInfoService(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<List<UserDTOResponseModel>> GetAllUsersAsync()
        {
            String token = (await _localStorageService.GetItemAsync<string>(BaseConstants.LocalToken))!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync("api/UserInfo");
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