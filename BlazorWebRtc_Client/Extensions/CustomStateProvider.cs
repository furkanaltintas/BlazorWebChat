using Blazored.LocalStorage;
using BlazorWebRtc_Client.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BlazorWebRtc_Client.Extensions;

public class CustomStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public CustomStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        String? token = await _localStorageService.GetItemAsync<string>(BaseConstants.LocalToken);

        if (token is null)
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseJwtToClaim(token), "jwtAuthType")));
    }


    // Kullanıcı login olduğunda bildirim basacak
    public void NotifyUserLoggedIn(string token)
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseJwtToClaim(token), "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }


    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        NotifyAuthenticationStateChanged(authState);
    }
}