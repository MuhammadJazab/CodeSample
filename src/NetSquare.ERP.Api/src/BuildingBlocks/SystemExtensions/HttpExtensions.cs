//-----------------------------------------------------------------------
// <copyright file="HttpExtensions.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.SystemExtensions;

/// <summary>
/// Defines the <see cref="HttpExtensions"/>.
/// </summary>
public static class HttpExtensions
{
    /// <summary>
    /// To get the request origin from header.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>The Origin Url from where the request is comming. <see cref="string"/>.</returns>
    public static string? GetOrigin(this HttpContext httpContext)
    {
        if (httpContext?.Request?.Headers?.ContainsKey(Constants.OriginKey) ?? false)
        {
            return httpContext.Request.Headers.ContainsKey(Constants.OriginKey) ? httpContext?.Request?.Headers[Constants.OriginKey] : string.Empty;
        }

        return string.Empty;
    }

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
        string authHeader = httpContextAccessor.HttpContext.Request.Headers[Constants.AuthorizationKey]!;
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var value = token.Claims?.FirstOrDefault(claim => string.Equals(claim.Type, nameof(CustomClaimType.UserId), StringComparison.InvariantCultureIgnoreCase))?.Value;
            if (Guid.TryParse(value, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
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
        string authHeader = httpRequest.Headers[Constants.AuthorizationKey]!;
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var value = token.Claims?.FirstOrDefault(claim => string.Equals(claim.Type, Constants.ClaimTypeUserId, StringComparison.InvariantCultureIgnoreCase))?.Value;
            if (Guid.TryParse(value, out Guid userId))
            {
                return userId;
            }
            return Guid.Empty;
        }
        return Guid.Empty;
    }

    /// <summary>
    /// Determines whether [is token expired].
    /// </summary>
    /// <param name="httpRequest">The HTTP request.</param>
    /// <returns>
    ///   <c>true</c> if [is token expired] [the specified HTTP request]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsTokenExpired(this HttpRequest httpRequest)
    {
        string authHeader = httpRequest.Headers[Constants.AuthorizationKey]!;

        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey) && authHeader[Constants.BearerKey.Length..].Trim().Length > 0)
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);

            System.Diagnostics.Debug.WriteLine("Token Expire time " + token.ValidTo.ToLocalTime());

            return DateTime.UtcNow >= token.ValidTo;
        }
        return true;
    }

    /// <summary>
    /// Validates the current token.
    /// </summary>
    /// <param name="httpRequest">The HTTP request.</param>
    /// <param name="jwtConfigurationKey">The JWT configuration key.</param>
    /// <param name="jwtConfigurationIssuer">The JWT configuration issuer.</param>
    /// <param name="jwtConfigurationAudience">The JWT configuration audience.</param>
    /// <returns></returns>
    public static bool ValidateCurrentToken(this HttpRequest httpRequest, string jwtConfigurationKey, string jwtConfigurationIssuer, string jwtConfigurationAudience)
    {
        string authHeader = httpRequest.Headers[Constants.AuthorizationKey]!;
        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.ASCII.GetBytes(jwtConfigurationKey));
        if (authHeader == null || !authHeader.StartsWith(Constants.BearerKey)) return false;
        string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(tokenString, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtConfigurationIssuer,
                ValidAudience = jwtConfigurationAudience,
                IssuerSigningKey = symmetricSecurityKey
            }, out SecurityToken validatedToken);
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// To get the use id from token.
    /// </summary>
    /// <param name="httpRequest">The object of httpRequest <see cref="HttpRequest"/></param>
    /// <returns>It returns the current user id. <see cref="Guid"/>.</returns>
    public static string GetUserName(this HttpRequest httpRequest)
    {
        string authHeader = httpRequest.Headers[Constants.AuthorizationKey]!;
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var value = token.Claims?.FirstOrDefault(claim => string.Equals(claim.Type, Constants.ClaimTypeUsername, StringComparison.InvariantCultureIgnoreCase))?.Value;
            return value ?? string.Empty;
        }
        return String.Empty;
    }

    /// <summary>
    /// To get the role id from token.
    /// </summary>
    /// <param name="httpRequest">The object of httpRequest <see cref="HttpRequest"/></param>
    /// <returns>It returns the current user role id. <see cref="Guid"/>.</returns>
    public static string GetRoleId(this HttpRequest httpRequest)
    {
        string authHeader = httpRequest.Headers[Constants.AuthorizationKey]!;
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var value = token.Claims?.FirstOrDefault(claim => string.Equals(claim.Type, Constants.ClaimTypeRole, StringComparison.InvariantCultureIgnoreCase))?.Value;
            return value ?? string.Empty;
        }
        return string.Empty;
    }

    /// <summary>
    /// Gets the system identifier.
    /// </summary>
    /// <param name="httpRequest">The HTTP request.</param>
    /// <returns>It returns the system identifier</returns>
    public static string GetSystemId(this HttpRequest httpRequest)
    {
        string authHeader = httpRequest.Headers[Constants.AuthorizationKey]!;
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var value = token.Claims?.FirstOrDefault(claim => string.Equals(claim.Type, Constants.ClaimTypeSystemId, StringComparison.InvariantCultureIgnoreCase))?.Value;
            return value ?? string.Empty;
        }
        return string.Empty;
    }

    /// <summary>
    /// Gets the name of the system.
    /// </summary>
    /// <param name="httpRequest">The HTTP request.</param>
    /// <returns>It returns the system name</returns>
    public static string GetSystemName(this HttpRequest httpRequest)
    {
        string authHeader = httpRequest.Headers[Constants.AuthorizationKey]!;
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var value = token.Claims?.FirstOrDefault(claim => string.Equals(claim.Type, Constants.ClaimTypeSystemName, StringComparison.InvariantCultureIgnoreCase))?.Value;
            return value ?? string.Empty;
        }
        return string.Empty;
    }

    /// <summary>
    /// To get the claims from token.
    /// </summary>
    /// <param name="request">The object of HttpRequest<see cref="HttpRequest"/></param>
    /// <returns>The IEnumerable of Claim<see cref="IEnumerable<Claim>"/></returns>
    public static IEnumerable<Claim> GetClaims(this HttpRequest request)
    {
        string authHeader = request.Headers[Constants.AuthorizationKey]!;
        if (authHeader != null && authHeader.StartsWith(Constants.BearerKey))
        {
            string tokenString = authHeader[Constants.BearerKey.Length..].Trim();
            var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenString);
            var claims = token.Claims;
            return claims;
        }
        return new List<Claim>();
    }
}