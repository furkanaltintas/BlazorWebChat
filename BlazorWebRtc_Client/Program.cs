using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebRtc_Client;
using BlazorWebRtc_Client.Services.Abstract;
using BlazorWebRtc_Client.Services.Concrete;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorWebRtc_Client.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Http.Connections;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserFriendService, UserFriendService>();
builder.Services.AddScoped<IMessageService, MessageService>();
#endregion

#region Auth
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomStateProvider>();
#endregion

builder.Services.AddScoped(st =>
{
    var hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:7090/userhub", options =>
        {
            options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
        })
    .Build();

    return hubConnection;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7090/") });

await builder.Build().RunAsync();
