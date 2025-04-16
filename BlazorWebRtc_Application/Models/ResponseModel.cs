namespace BlazorWebRtc_Application.Models;

public class ResponseModel
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public object Data { get; set; } = null!;
}