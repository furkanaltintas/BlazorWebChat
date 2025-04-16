using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Infrastructure.Jwt;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebRtc_Application.Features.Commands.Account.Login;

// Metot sınıfımız (İş kodları buraya yazılır)
public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseModel>
{
    private readonly IGenerateJwtToken _generateJwtToken;
    private readonly AppDbContext _context;

    public LoginCommandHandler(IGenerateJwtToken generateJwtToken, AppDbContext context)
    {
        _generateJwtToken = generateJwtToken;
        _context = context;
    }

    public async Task<ResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null) return new ResponseModel { Message = "HATA!!!", IsSuccess = false, Data = null! };
        if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt)) return new ResponseModel { Message = "HATA!!!", IsSuccess = false, Data = null! };

        String token = _generateJwtToken.CreateToken(user);
        return new ResponseModel { IsSuccess = true, Data = token };
    }

    private bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        Byte[] salt = Convert.FromBase64String(storedSalt);

        String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA256,
           iterationCount: 10000,
           numBytesRequested: 256 / 8));

        return hashed == storedHash;
    }
}