//-----------------------------------------------------------------------
// <copyright file="HttpMessageConstants.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.HumanResource.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="HttpMessageConstants" />.
/// </summary>
public static class HttpMessageConstants
{
    /// <summary>
    /// Defines the LoginSucceeded.
    /// </summary>
    public const string LoginSucceeded = "User Successfully loggedIn.";

    /// <summary>
    /// Defines the LoginFailed.
    /// </summary>
    public const string LoginFailed = "Invalid Username / Password.";

    /// <summary>
    /// Defines the Exception.
    /// </summary>
    public const string Exception = "Exception occurred";
}