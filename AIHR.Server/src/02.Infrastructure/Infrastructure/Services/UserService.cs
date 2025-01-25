using System.Security.Claims;
using AIHR.EventScheduler.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AIHR.EventSchedulerInfrastructure.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
    }
}