using Microsoft.AspNetCore.Http;

namespace BlazorWebRtc_Application.Models;

public class UploadFileModel
{
    public string UserId { get; set; } = string.Empty;
    public IFormFile File { get; set; } = null!;
}