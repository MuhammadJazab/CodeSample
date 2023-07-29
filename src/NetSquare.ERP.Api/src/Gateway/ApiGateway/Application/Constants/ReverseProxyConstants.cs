//-----------------------------------------------------------------------
// <copyright file="ReverseProxyConstants.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Gateway.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="ReverseProxyConstants" />.
/// </summary>
public static partial class ReverseProxyConstants
{
    #region DestinationId

    /// <summary>
    /// The notification api url key
    /// </summary>
    public const string AuthenticationDestinationId = "authentication";

    /// <summary>
    /// The permission api url key
    /// </summary>
    public const string HumanResourceDestinationId = "humanresource";

    /// <summary>
    /// The group capacity api url key
    /// </summary>
    public const string BranchDestinationId = "branch";

    /// <summary>
    /// The customer api url key
    /// </summary>
    public const string DepartmentDestinationId = "department";

    #endregion

    #region ClusterId

    /// <summary>
    /// The HumanResource cluster identifier
    /// </summary>
    public const string HumanResourceClusterId = "human-resource-cluster";

    /// <summary>
    /// The branch cluster identifier
    /// </summary>
    public const string BranchClusterId = "branch-cluster";

    /// <summary>
    /// The customer cluster identifier
    /// </summary>
    public const string DepartmentClusterId = "department-cluster";

    /// <summary>
    /// The customer cluster identifier
    /// </summary>
    public const string AuthenticationClusterId = "authentication-cluster";

    #endregion

    /// <summary>
    /// The authorization key
    /// </summary>
    public const string AuthorizationKey = "Authorization";

    /// <summary>
    /// The bearer key
    /// </summary>
    public const string BearerKey = "Bearer";

    /// <summary>
    /// The payload key
    /// </summary>
    public const string PayloadKey = "payload";

    /// <summary>
    /// The claim right path
    /// </summary>
    public const string ClaimRightPath = "/api/ClaimRight";

    /// <summary>
    /// The URI seprator
    /// </summary>
    public const char URISeprator = '/';

    /// <summary>
    /// The Authentication uri
    /// </summary>
    public const string AuthenticationURI = "Authentication";

    /// <summary>
    /// The token expire message
    /// </summary>
    public const string TokenExpireMessage = "Your token has expired";

    /// <summary>
    /// The token invalid message
    /// </summary>
    public const string TokenInvalidMessage = "Your token is invalid";

    /// <summary>
    /// The not have right message
    /// </summary>
    public const string NotHaveRightMessage = "You dont have right on resource";

    /// <summary>
    /// The proxy path indexes
    /// </summary>
    public const int ProxyPathIndexes = 2;

    /// <summary>
    /// URIs the regex.
    /// </summary>
    /// <returns></returns>
    [System.Text.RegularExpressions.GeneratedRegex("\\([^)]*\\)")]
    public static partial System.Text.RegularExpressions.Regex URIRegex();
}