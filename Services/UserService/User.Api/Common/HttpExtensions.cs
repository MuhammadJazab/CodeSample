//file="HttpExtensions.cs"

namespace User.Api.Common;

/// <summary>
/// Defines the <see cref="HttpExtensions"/>.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "static class")]
public static class HttpExtensions
{

    /// <summary>
    /// To get current user role from token.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns>It returns the current user role. <see cref="string"/>.</returns>
    public static string GetUserRole(this IHttpContextAccessor httpContextAccessor)
    {
        string? result = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
        return result ?? string.Empty;
    }

    /// <summary>
    /// To get the use id from token.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns>It returns the current user id. <see cref="Guid"/>.</returns>
    public static Guid GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var value = GetClaimByType(httpContextAccessor.HttpContext?.Request!, Constants.ClaimTypeUserId);
        if (Guid.TryParse(value, out Guid userId))
        {
            return userId;
        }
        return Guid.Empty;
    }

    /// <summary>
    /// To get the use id from token.
    /// </summary>
    /// <param name="httpRequest">The object of httpRequest <see cref="HttpRequest"/></param>
    /// <returns>It returns the current user id. <see cref="Guid"/>.</returns>
    public static Guid GetUserId(this HttpRequest httpRequest)
    {
        var value = GetClaimByType(httpRequest, Constants.ClaimTypeUserId);
        if (Guid.TryParse(value, out Guid userId))
        {
            return userId;
        }
        return Guid.Empty;
    }

    /// <summary>
    /// Gets the authtoken.
    /// </summary>
    /// <param name="httpRequest">The HTTP request.</param>
    /// <returns>It returns the JWT</returns>
    public static string GetAuthtoken(this HttpRequest httpRequest)
    {
        return Convert.ToString(httpRequest.Headers[Constants.AuthorizationKey]!) ?? Convert.ToString(httpRequest.Query[Constants.AccessTokenKey]);
    }

    /// <summary>
    /// Gets the type of the claim by.
    /// </summary>
    /// <param name="httpRequest">The HTTP request.</param>
    /// <param name="claimType">Type of the claim.</param>
    /// <returns>claim value</returns>
    private static string GetClaimByType(HttpRequest httpRequest, string claimType)
    {
        string authHeader = httpRequest.GetAuthtoken();
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader.Replace(Constants.BearerKey, string.Empty);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var value = token.Claims?.FirstOrDefault(claim => string.Equals(claim.Type, claimType, StringComparison.InvariantCultureIgnoreCase))?.Value;
            return value ?? string.Empty;
        }
        return string.Empty;
    }
}

