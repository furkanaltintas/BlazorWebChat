using BlazorWebRtc_Client.Models.Response;

namespace BlazorWebRtc_Client.Services.Abstract;

public interface  IUploadService
{
    Task<ResponseModel> UploadFileAsync(MultipartFormDataContent content);
}