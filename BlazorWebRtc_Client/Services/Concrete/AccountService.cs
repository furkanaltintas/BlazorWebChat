using Blazored.LocalStorage;
using BlazorWebRtc_Client.Constants;
using BlazorWebRtc_Client.Extensions;
using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;
using BlazorWebRtc_Client.Services.Abstract;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text;

namespace BlazorWebRtc_Client.Services.Concrete;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AccountService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task Logout()
    {
        await _localStorageService.RemoveItemAsync(BaseConstants.LocalToken);
        ((CustomStateProvider)_authenticationStateProvider).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<Boolean> SignIn(LoginCommand loginCommand)
    {
        String content = JsonConvert.SerializeObject(loginCommand);
        StringContent bodyContent = new(content, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("api/Users/Login", bodyContent);
        String contentData = await response.Content.ReadAsStringAsync();
        ResponseModel result = JsonConvert.DeserializeObject<ResponseModel>(contentData)!;

        if (response.IsSuccessStatusCode)
        {
            await _localStorageService.SetItemAsync(BaseConstants.LocalToken, result.Data.ToString());
            ((CustomStateProvider)_authenticationStateProvider).NotifyUserLoggedIn(result.Data.ToString()!);
            return result.IsSuccess!;
        }
        else
            return false;
    }

    public async Task<UserResponseModel> SignUp(RegisterCommand registerCommand)
    {
        String content = JsonConvert.SerializeObject(registerCommand);
        StringContent bodyContent = new(content, Encoding.UTF8,  "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("api/Users/Register", bodyContent);
        String contentData = await response.Content.ReadAsStringAsync();
        ResponseModel result = JsonConvert.DeserializeObject<ResponseModel>(contentData)!;

        UserResponseModel? responseModel = JsonConvert.DeserializeObject<UserResponseModel>(result.Data.ToString()!);
        if (response.IsSuccessStatusCode)
            return responseModel!;
        else
            return null!;
    }
}