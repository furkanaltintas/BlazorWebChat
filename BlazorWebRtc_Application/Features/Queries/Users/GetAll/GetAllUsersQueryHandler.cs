using BlazorWebRtc_Application.Dtos;
using BlazorWebRtc_Application.Models;
using BlazorWebRtc_Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlazorWebRtc_Application.Features.Queries.Users.GetAll;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResponseModel>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;

    public GetAllUsersQueryHandler(IHttpContextAccessor httpContextAccessor, AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ResponseModel> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return new();

        List<UserDto> userDtos = await _context.Users.Where(u => u.Id != Guid.Parse(userId)).Select(u => new UserDto
        {
            UserId = u.Id,
            Email = u.Email,
            UserName = u.UserName,
            Profile = u.Profile!
        }).ToListAsync(cancellationToken);

        return new ResponseModel { IsSuccess = true, Data = userDtos, Message = string.Empty };
    }
}