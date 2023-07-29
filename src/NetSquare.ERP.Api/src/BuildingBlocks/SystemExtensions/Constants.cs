//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SystemExtensions;

/// <summary>
/// Defines the <see cref="Constants" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Constants
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
    /// Defines the origin key
    /// </summary>
    public const string OriginKey = "Origin";

    /// <summary>
    /// The claim type role 
    /// </summary>
    public const string ClaimTypeRole = "Role";

    /// <summary>
    /// The claim type Username 
    /// </summary>
    public const string ClaimTypeUsername = "Username";

    /// <summary>
    /// The claim type user identifier
    /// </summary>
    public const string ClaimTypeUserId = "UserId";

    /// <summary>
    /// The claim type system name
    /// </summary>
    public const string ClaimTypeSystemName = "SystemName";

    /// <summary>
    /// The claim type system identifier
    /// </summary>
    public const string ClaimTypeSystemId = "SystemId";
}