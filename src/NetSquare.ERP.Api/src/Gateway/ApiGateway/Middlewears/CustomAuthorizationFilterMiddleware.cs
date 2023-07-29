//-----------------------------------------------------------------------
// <copyright file="CustomAuthorizationFilterMiddleware.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Middlewears;

/// <summary>
/// Defines the <see cref="CustomAuthorizationFilterMiddleware" />.
/// </summary>
public class CustomAuthorizationFilterMiddleware
{
    /// <summary>
    /// Defines the _next.
    /// </summary>
    private readonly RequestDelegate next;

    /// <summary>
    /// The JWT settings
    /// </summary>
    private readonly JwtConfigurations jwtConfigurations;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomAuthorizationFilterMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next.</param>
    /// <param name="jwtConfigurations">The JWT settings.</param>
    public CustomAuthorizationFilterMiddleware(RequestDelegate next, IOptions<JwtConfigurations> jwtConfigurations)
    {
        this.next = next;
        this.jwtConfigurations = jwtConfigurations.Value;
    }

    /// <summary>
    /// The InvokeAsync.
    /// </summary>
    /// <param name="context">The context<see cref="HttpContext"/>.</param>
    /// <returns>The Task<see cref="Task"/>.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        string authHeader = context.Request.Headers[ReverseProxyConstants.AuthorizationKey]!;

        if (authHeader != null && authHeader.StartsWith(ReverseProxyConstants.BearerKey!) && context!.Request!.ValidateCurrentToken(jwtConfigurations!.Key!, jwtConfigurations!.Issuer!, jwtConfigurations!.Audience!))
        {
            if (context.Request.IsTokenExpired() /*|| !await HasRightOnClaimResource(context.Request)*/)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(context.Request.IsTokenExpired() ? ReverseProxyConstants.TokenExpireMessage : ReverseProxyConstants.NotHaveRightMessage);
                return;
            }
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync(ReverseProxyConstants.TokenInvalidMessage);
            return;
        }
        await next(context);
    }

    /// <summary>
    /// Determines whether [has right on claim resource] [the specified request].
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///   <c>true</c> if [has right on claim resource] [the specified request]; otherwise, <c>false</c>.
    /// </returns>
    ////private async Task<bool> HasRightOnClaimResource(HttpRequest request)
    ////{
    ////    Metadata headers = new()
    ////    {
    ////        { ReverseProxyConstants.AuthorizationKey, request.Headers[ReverseProxyConstants.AuthorizationKey]! }
    ////    };

    ////    var userInfoByUsernameResponse = await clientUserDetail!.GetUserInfoByUsernameAsync(new UserInfoByUsernameRequest { UserName = request.GetUserName() }, headers: headers, null, CancellationToken.None);

    ////    if (userInfoByUsernameResponse == null) return false;

    ////    request.Headers[ReverseProxyConstants.AuthorizationKey] = $"{ReverseProxyConstants.BearerKey}{new JwtSecurityTokenHandler().WriteToken(GenerateToken(request.GetUserName(), userInfoByUsernameResponse, string.Empty))}";

    ////    headers = new()
    ////    {
    ////        { ReverseProxyConstants.AuthorizationKey, request.Headers[ReverseProxyConstants.AuthorizationKey]! }
    ////    };

    ////    var roleId = userInfoByUsernameResponse.RoleId;
    ////    var userId = userInfoByUsernameResponse.UserId;

    ////    var authorizationResponse = await clientAuthorization!.GetUserPermissionsAsync(new AuthorizationRequest { RolId = roleId, UserId = userId }, headers: headers, null, CancellationToken.None);

    ////    request.Headers[ReverseProxyConstants.AuthorizationKey] = $"{ReverseProxyConstants.BearerKey}{new JwtSecurityTokenHandler().WriteToken(GenerateToken(request.GetUserName(), userInfoByUsernameResponse, JsonSerializer.Serialize(authorizationResponse)))}";

    ////    string uriPath = request!.Path!.Value!;
    ////    var lastValue = uriPath.Split(ReverseProxyConstants.URISeprator).Last();
    ////    authorizationResponse!.ClaimResourceUrls.ToList().ForEach(p => p.Uri = ReverseProxyConstants.URIRegex().Replace(p.Uri, lastValue));
    ////    return authorizationResponse!.ClaimResourceUrls.Any(c => c.Uri!.Equals($"{ReverseProxyConstants.URISeprator}{string.Join(ReverseProxyConstants.URISeprator, uriPath.Split(ReverseProxyConstants.URISeprator).Skip(ReverseProxyConstants.ProxyPathIndexes))}", StringComparison.OrdinalIgnoreCase) && c.HttpVerb!.Equals(request.Method, StringComparison.OrdinalIgnoreCase));
    ////}
    
    /// <summary>
    /// Generates the token.
    /// </summary>
    /// <param name="username">The username.<see cref="string"/></param>
    /// <param name="jwtSettings">The JWT settings.<see cref="JwtSettings"/></param>
    /// <param name="uRIRights">The uRIRights.<see cref="string"/></param>
    /// <param name="userInfoByUsernameResponse">The user information by username response.<see cref="UserInfoByUsernameResponse"/></param>
    /// <returns></returns>
    ////private JwtSecurityToken GenerateToken(string username, UserInfoByUsernameResponse userInfoByUsernameResponse, string claimRights)
    ////{
    ////    List<Claim> groupClaims = new();

    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.RoleId), value: userInfoByUsernameResponse.RoleId));
    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.RoleName), value: userInfoByUsernameResponse.RoleName));
    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.SystemId), value: userInfoByUsernameResponse.SystemId));
    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.AirlineCode), value: userInfoByUsernameResponse.SystemCode));
    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.UserId), value: userInfoByUsernameResponse.UserId));
    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.SystemName), value: userInfoByUsernameResponse.SystemName));
    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.Username), value: username));
    ////    groupClaims!.Add(new Claim(type: nameof(CustomClaimType.ClaimRights), value: claimRights));

    ////    SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(jwtConfigurations.Key!));

    ////    SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    ////    JwtSecurityToken jwtSecurityToken = new(
    ////        issuer: jwtConfigurations.Issuer,
    ////        audience: jwtConfigurations.Audience,
    ////        claims: groupClaims,
    ////        expires: DateTime.UtcNow.AddMinutes(jwtConfigurations.DurationInMinutes),
    ////        signingCredentials: signingCredentials);
    ////    return jwtSecurityToken;
    ////}
}