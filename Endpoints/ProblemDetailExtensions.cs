using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace APIPedidos.Endpoints;

public static class ProblemDetailExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        return notifications
                .GroupBy(n => n.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
    {
        return error
                .GroupBy(e => e.Code)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
    }
}
