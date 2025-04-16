using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebRtc_Client.Models.Request;

public class RegisterCommand
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "ConfirmPassword is required")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public IFormFile Profile { get; set; } = null!;
}