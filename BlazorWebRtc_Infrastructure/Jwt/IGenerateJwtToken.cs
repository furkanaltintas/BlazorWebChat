using BlazorWebRtc_Domain.Entities;

namespace BlazorWebRtc_Infrastructure.Jwt;

public interface IGenerateJwtToken
{
    string CreateToken(User user);
}