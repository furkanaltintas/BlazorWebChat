using BlazorWebRtc_Api.Controllers.Base;
using BlazorWebRtc_Application.Features.Commands.Upload;
using BlazorWebRtc_Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebRtc_Api.Controllers;

public class UploadsController : BaseController
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UploadsController(IMediator mediator, IWebHostEnvironment webHostEnvironment) : base(mediator)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Upload([FromForm] UploadFileModel uploadFileModel)
    {
        if (uploadFileModel.File == null || uploadFileModel.File.Length == 0) return BadRequest("Not file uploaded");

        String uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "profile-pictures");
        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        String fileName = Path.GetRandomFileName() + Path.GetExtension(uploadFileModel.File.FileName);
        String filePath = Path.Combine(uploadPath, fileName);

        using (FileStream stream = new(filePath, FileMode.Create))
            await uploadFileModel.File.CopyToAsync(stream);

        String imageUrl = $"/images/profile-pictures/{fileName}";

        UploadCommand uploadCommand = new(uploadFileModel.UserId, imageUrl);
        Boolean result = await _mediator.Send(uploadCommand);
        return Ok(result);
    }
}
