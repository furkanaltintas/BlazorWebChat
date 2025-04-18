using BlazorWebRtc_Client.Extensions;
using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;
using BlazorWebRtc_Client.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc_Client.Services.Concrete
{
    public class MessageService : IMessageService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly HttpClient _httpClient;

        public MessageService(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _httpClient = httpClient;
        }

        public async Task<List<MessageListResponseModel>> GetMessageList(MessageQueryModel messageQueryModel)
        {
            AuthenticationState result = await ((CustomStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Messages?messageUserId={messageQueryModel.MessageUserId}");
            string content = await response.Content.ReadAsStringAsync();
            ResponseModel? deserialize = JsonConvert.DeserializeObject<ResponseModel>(content);

            if (deserialize!.Data is null) return new();

            if (response.IsSuccessStatusCode)
            {
                List<MessageListResponseModel>? desObject = JsonConvert.DeserializeObject<List<MessageListResponseModel>>(deserialize!.Data.ToString()!);
                return desObject!;
            }

            return new();
        }

        public async Task<bool> SendMessage(SendMessageModel sendMessageModel)
        {
            String content = JsonConvert.SerializeObject(sendMessageModel);
            StringContent bodyContent = new(content, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("api/Messages", bodyContent);
            String contentData = await response.Content.ReadAsStringAsync();
            ResponseModel result = JsonConvert.DeserializeObject<ResponseModel>(contentData)!;

            if (response.IsSuccessStatusCode) return result.IsSuccess;
            return false;
        }
    }
}
