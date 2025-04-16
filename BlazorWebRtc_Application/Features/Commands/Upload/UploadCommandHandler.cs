using BlazorWebRtc_Domain.Entities;
using BlazorWebRtc_Persistence.Context;
using MediatR;

namespace BlazorWebRtc_Application.Features.Commands.Upload;

public class UploadCommandHandler : IRequestHandler<UploadCommand, bool>
{
    private readonly AppDbContext _context;
    public UploadCommandHandler(AppDbContext context) { _context = context; }

    public async Task<bool> Handle(UploadCommand request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.FindAsync(request.UserId, cancellationToken);
        if(user is null) return false;

        user.Profile = request.File;
        await _context.SaveChangesAsync();
        return true;
    }
}