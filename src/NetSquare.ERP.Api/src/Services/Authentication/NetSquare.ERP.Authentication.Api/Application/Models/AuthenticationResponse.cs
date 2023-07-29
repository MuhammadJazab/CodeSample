//-----------------------------------------------------------------------
// <copyright file="AuthenticationResponse.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Models;

/// <summary>
/// Defines the <see cref="AuthenticationResponse" />.
/// </summary>
public class AuthenticationResponse : BaseResponse
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or sets the UserName.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the Token.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationResponse"/> class.
    /// </summary>
    /// <param name="id"><see cref="Guid"/></param>
    /// <param name="userName"><see cref="string"/></param>
    /// <param name="email"><see cref="string"/></param>
    /// <param name="token"><see cref="string"/></param>
    public AuthenticationResponse(Guid? id, string? userName, string? email, string? token)
    {
        this.Id = id;
        this.UserName = userName;
        this.Email = email;
        this.Token = token;
    }
}