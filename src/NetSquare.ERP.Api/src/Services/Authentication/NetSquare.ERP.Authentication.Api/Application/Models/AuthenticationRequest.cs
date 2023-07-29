//-----------------------------------------------------------------------
// <copyright file="AuthenticationRequest.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Models;

/// <summary>
/// Defines the <see cref="AuthenticationRequest" />.
/// </summary>
public class AuthenticationRequest
{
    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}