//-----------------------------------------------------------------------
// <copyright file="RegisterUserRequestCommand.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Features.Account.Requests.Commands;

/// <summary>
/// Defines the <see cref="RegisterUserRequestCommand" />.
/// </summary>
public class RegisterUserRequestCommand : IRequest<RegisterUserResponse>
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
    /// Get or set the Password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Get or set the UserRole
    /// </summary>
    public string? Role { get; set; }

    /// <summary>
    /// Get or set the if user is enabled
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserRequestCommand"/> class.
    /// </summary>
    /// <param name="registerUserRequest"><see cref="RegisterUserRequest"/></param>
    public RegisterUserRequestCommand(RegisterUserRequest registerUserRequest)
	{
        this.UserName = registerUserRequest.UserName;
        this.Email = registerUserRequest.Email;
        this.Phone = registerUserRequest.Phone;
        this.FirstName = registerUserRequest.FirstName;
        this.LastName = registerUserRequest.LastName;
        this.EmployeeNumber = registerUserRequest.EmployeeNumber;
        this.BranchId = registerUserRequest.BranchId;
        this.Role = registerUserRequest.Role;
        this.Password = ProgramConstants.DefaultPassword;
        this.Enabled = registerUserRequest.Enabled;
	}
}