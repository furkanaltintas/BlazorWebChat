using BlazorWebRtc_Client.Models.Response;
using BlazorWebRtc_Client.Services.Abstract;
using System.Net.Http.Json;

namespace BlazorWebRtc_Client.Services.Concrete;

public class UploadService : IUploadService
{
    private readonly HttpClient _httpClient;

    public UploadService(HttpClient httpClient) { _httpClient = httpClient; }

    public async Task<ResponseModel> UploadFileAsync(MultipartFormDataContent content)
    {
        var response = await _httpClient.PostAsync("api/Uploads/Upload", content);
        if (response.IsSuccessStatusCode)
        {
            ResponseModel responseModel = (await response.Content.ReadFromJsonAsync<ResponseModel>())!;
            return responseModel.IsSuccess ? new ResponseModel { Message = "Başarılı", IsSuccess = true } : new ResponseModel { Message = "Başarısız", IsSuccess = false };
        }

        return new ResponseModel { Message = "File upload failed", IsSuccess = false };
    }
}