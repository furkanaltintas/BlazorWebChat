using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlazorWebRtc_Application.Features.Commands.Account.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, User>
{
    private readonly AppDbContext _context;

    public RegisterCommandHandler(AppDbContext context) { _context = context; }

    public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.UserName == request.UserName)) throw new Exception("User already exist");
        if (request.Password != request.ConfirmPassword) throw new Exception("Passwords are not the same");

        var (passwordHash, passwordSalt) = HashPassword(request.Password);
        User user = new()
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    private (string hash, string salt) HashPassword(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(salt);
        }

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA256,
           iterationCount: 10000,
           numBytesRequested: 256 / 8));

        return (hashed, Convert.ToBase64String(salt));
    }

    private async Task<string> UploadProfile(IFormFile formFile)
    {
        const string profilePicturePath = "images/profile-pictures/";
        string path = Path.Combine("wwwroot", "images", "profile-pictures");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string fileName = $"{DateTime.UtcNow.Millisecond}_{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
        var filePath = Path.Combine(path, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
            await formFile.CopyToAsync(fileStream);

        return $"{profilePicturePath}{fileName}";
    }
}