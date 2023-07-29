//-----------------------------------------------------------------------
// <copyright file="GetUsersResponse.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Models;

/// <summary>
/// Defines the <see cref="GetUsersResponse" />.
/// </summary>
public class GetUsersResponse : BaseResponse
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
    /// Get of set the User status
    /// </summary>
    public bool IsActive{ get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersResponse"/> class.
    /// </summary>
    public GetUsersResponse()
    {}

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersResponse"/> class.
    /// </summary>
    /// <param name="users"><see cref="ApplicationUser"/></param>
    public GetUsersResponse(ApplicationUser users)
    {
        this.Id = users.Id;
        this.UserName = users.UserName;
        this.Email = users.Email;
        this.IsActive = users.IsActive;
    }
}