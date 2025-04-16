using BlazorWebRtc_Client.Extensions;
using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;
using BlazorWebRtc_Client.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace BlazorWebRtc_Client.Services.Concrete;

public class RequestService : IRequestService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;

    public RequestService(AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
    }

    public async Task<List<GetRequestFriendshipList>> GetFriendshipRequest()
    {
        var result = await ((CustomStateProvider)_authenticationStateProvider).GetAuthenticationStateAsync();

        HttpResponseMessage response = await _httpClient.GetAsync($"api/Requests/{result.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");
        string content = await response.Content.ReadAsStringAsync();
        ResponseModel? deserialize = JsonConvert.DeserializeObject<ResponseModel>(content);

        if(deserialize.Data is null) return new();

        if (response.IsSuccessStatusCode)
        {
            List<GetRequestFriendshipList>? desObject = JsonConvert.DeserializeObject<List<GetRequestFriendshipList>>(deserialize!.Data.ToString()!);
            return desObject!;
        }

        return new();
    }

    public async Task<ResponseModel> SendFriendshipRequest(RequestFriendShipCommand requestFriendShipCommand)
    {
        String content = JsonConvert.SerializeObject(requestFriendShipCommand);
        StringContent bodyContent = new(content, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("api/Requests", bodyContent);
        String contentData = await response.Content.ReadAsStringAsync();
        ResponseModel result = JsonConvert.DeserializeObject<ResponseModel>(contentData)!;

        if (response.IsSuccessStatusCode) return result;

        return new ResponseModel { IsSuccess = false };
    }

    public async Task<ResponseModel> UpdateRequest(UpdateRequestCommand updateRequestCommand)
    {
        String content = JsonConvert.SerializeObject(updateRequestCommand);
        StringContent bodyContent = new(content, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync("api/Requests", bodyContent);
        String contentData = await response.Content.ReadAsStringAsync();
        ResponseModel result = JsonConvert.DeserializeObject<ResponseModel>(contentData)!;

        if (response.IsSuccessStatusCode) return result;

        return new() { IsSuccess = false };
    }
}