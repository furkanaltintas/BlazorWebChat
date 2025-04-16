using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;

namespace BlazorWebRtc_Client.Services.Abstract;

public interface IMessageService
{
    Task<List<MessageListResponseModel>> GetMessageList(MessageQueryModel messageQueryModel);

    Task<Boolean> SendMessage(SendMessageModel sendMessageModel);
}