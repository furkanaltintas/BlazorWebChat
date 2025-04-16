using System.ComponentModel.DataAnnotations;

namespace BlazorWebRtc_Client.Models.Request;

public class LoginCommand
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}