//-----------------------------------------------------------------------
// <copyright file="UserLoginRequestQuery.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Features.Account.Requests.Queries;

/// <summary>
/// Defines the <see cref="UserLoginRequestQuery" />.
/// </summary>
public class UserLoginRequestQuery : IRequest<AuthenticationResponse>
{
    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserLoginRequestQuery"/> class.
    /// </summary>
    /// <param name="email"><see cref="string"/></param>
    /// <param name="password"><see cref="string"/></param>
    public UserLoginRequestQuery(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }
}