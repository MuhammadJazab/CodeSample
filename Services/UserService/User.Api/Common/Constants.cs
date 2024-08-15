// file="Constants.cs"

namespace User.Api.Common;

/// <summary>
/// Defines the <see cref="Constants" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class Constants
{
    /// <summary>
    /// Defines the authorization key
    /// </summary>
    public const string AuthorizationKey = "Authorization";

    /// <summary>
    /// Defines the bearer key
    /// </summary>
    public const string BearerKey = "Bearer ";

    /// <summary>
    /// The claim type role name
    /// </summary>
    public const string ClaimTypeRoleName = "RoleName";

    /// <summary>
    /// The claim type role 
    /// </summary>
    public const string ClaimTypeRole = "RoleId";

    /// <summary>
    /// The access token key
    /// </summary>
    public const string AccessTokenKey = "access_token";

    /// <summary>
    /// The claim type Username 
    /// </summary>
    public const string ClaimTypeUsername = "Name";

    /// <summary>
    /// The claim type email identifier
    /// </summary>
    public const string Email = "Email";

    /// <summary>
    /// The claim type user identifier
    /// </summary>
    public const string ClaimTypeUserId = "UserId";
}