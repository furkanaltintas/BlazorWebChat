﻿namespace BlazorWebRtc_Application.Dtos;

public class UserDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Profile { get; set; } = string.Empty;
}