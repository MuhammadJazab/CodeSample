//-----------------------------------------------------------------------
// <copyright file="CreateUserValidator.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Models;

/// <summary>
/// Defines the <see cref="RegisterUserRequest" />.
/// </summary>
public class RegisterUserRequest : BaseRequest
{
    /// <summary>
    /// Gets or sets the UserName.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Get or set the Employee No.
    /// </summary>
    public string? EmployeeNumber { get; set; }

    /// <summary>
    /// Get or set the BranchId
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Get or set the UserRole
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Get or set the if user is enabled
    /// </summary>
    public bool Enabled { get; set; }
}