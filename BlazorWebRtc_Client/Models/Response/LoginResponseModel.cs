namespace BlazorWebRtc_Client.Models.Response;

public record LoginResponseModel
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; } = false;
    public object Data { get; set; } = null!;
}