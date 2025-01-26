

using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace AIHR.EventScheduler.WebApi.Configs;

public class UserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims
            .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value!;
    }
}