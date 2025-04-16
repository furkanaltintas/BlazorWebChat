using BlazorWebRtc_Client.Models.Request;
using BlazorWebRtc_Client.Models.Response;

namespace BlazorWebRtc_Client.Services.Abstract;

public interface IRequestService
{
    Task<ResponseModel> SendFriendshipRequest(RequestFriendShipCommand requestFriendShipCommand);

    Task<List<GetRequestFriendshipList>> GetFriendshipRequest();

    Task<ResponseModel> UpdateRequest(UpdateRequestCommand updateRequestCommand);
}