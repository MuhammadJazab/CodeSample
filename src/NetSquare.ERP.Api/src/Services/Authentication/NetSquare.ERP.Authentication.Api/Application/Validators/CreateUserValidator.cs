//-----------------------------------------------------------------------
// <copyright file="CreateUserValidator.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Validators;

/// <summary>
/// Defines the <see cref="CreateUserValidator" />.
/// </summary>
public class CreateUserValidator : AbstractValidator<RegisterUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
    /// </summary>
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
             .NotEmpty()
             .EmailAddress()
             .WithErrorCode(ErrorCodes.EmailRequired)
             .WithMessage(ErrorMessages.EmailRequired);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .WithErrorCode(ErrorCodes.FirstNameRequired)
            .WithMessage(ErrorMessages.FirstNameRequired);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .WithErrorCode(ErrorCodes.LastNameRequired)
            .WithMessage(ErrorMessages.LastNameRequired);

        RuleFor(x => x.EmployeeNumber)
            .NotEmpty()
            .MaximumLength(50)
            .WithErrorCode(ErrorCodes.EmployeeNoRequired)
            .WithMessage(ErrorMessages.EmployeeNoRequired);

        RuleFor(x => x.Role)
           .NotEmpty()
           .WithErrorCode(ErrorCodes.UserRoleRequired)
           .WithMessage(ErrorMessages.UserRoleRequired);
    }
}