//-----------------------------------------------------------------------
// <copyright file="UserLoginRequestQueryHandler.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Features.Account.Handlers.Queries;

/// <summary>
/// Defines the <see cref="UserLoginRequestQueryHandler" />.
/// </summary>
public class UserLoginRequestQueryHandler : IRequestHandler<UserLoginRequestQuery, AuthenticationResponse>
{
    /// <summary>
    /// Defines the loginService.
    /// </summary>
    private readonly IAuthenticationService<ApplicationUser> authenticationService;

    /// <summary>
    /// Defines the jwtSettings.
    /// </summary>
    private readonly JwtConfiguration jwtSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserLoginRequestQueryHandler"/> class.
    /// </summary>
    /// <param name="authenticationService">The loginService<see cref="ILoginService{ApplicationUser}"/>.</param>
    /// <param name="jwtSettings">The jwtSettings<see cref="IOptions{JwtSettings}"/>.</param>
    public UserLoginRequestQueryHandler(IAuthenticationService<ApplicationUser> authenticationService, IOptions<JwtConfiguration> jwtSettings)
    {
        this.authenticationService = authenticationService;
        this.jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="UserLoginRequestQuery"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{AuthenticationResponse}"/>.</returns>
    public async Task<AuthenticationResponse> Handle(UserLoginRequestQuery request, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(request.Email);

        if (user is null)
            return default!;

        var result = await authenticationService.SignInAsync(user, request.Password);

        if (!result.Succeeded)
            return default!;

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthenticationResponse response = new(id: user.Id, userName: user.UserName!, email: user.Email, token: new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));

        return response;
    }

    /// <summary>
    /// The GenerateToken.
    /// </summary>
    /// <param name="user">The user<see cref="ApplicationUser"/>.</param>
    /// <returns>The <see cref="Task{JwtSecurityToken}"/>.</returns>
    public async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await authenticationService.GetClaimsAsync(user);
        var roles = await authenticationService.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claims = new[]
        {
            new Claim(type:JwtRegisteredClaimNames.Sub, value: user.UserName!),
            new Claim(type:JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            new Claim(type:JwtRegisteredClaimNames.Email,value: user.Email!),
            new Claim(type:JwtRegisteredClaimNames.Sid, value : user.Id.ToString()),
            new Claim(type:JwtRegisteredClaimNames.Name, value : $"{user.FirstName} {user.LastName}")
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}