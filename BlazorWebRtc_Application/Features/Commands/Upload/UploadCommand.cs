using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Upload;

public class UploadCommand : IRequest<bool>
{
    public string UserId { get; set; }
    public string File { get; set; }

    public UploadCommand()
    {
        UserId = string.Empty;
        File = string.Empty;
    }

    public UploadCommand(string userId, string file)
    {
        UserId = userId;
        File = file;
    }
}