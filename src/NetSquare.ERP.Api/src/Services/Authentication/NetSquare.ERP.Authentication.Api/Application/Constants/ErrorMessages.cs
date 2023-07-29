//-----------------------------------------------------------------------
// <copyright file="ErrorMessages.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="ErrorMessages" />.
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// Defines the UserIdRequired message.
    /// </summary>
    public const string SearchQueryRequired = "SearchQuery should not be empty.";

    /// <summary>
    /// Defines the UserIdRequired message.
    /// </summary>
    public const string UserIdRequired = "UserId is required.";

    /// <summary>
    /// Defines the IdRequired message.
    /// </summary>
    public const string IdRequired = "Id is required.";

    /// <summary>
    /// Defines the EmailRequired message.
    /// </summary>
    public const string EmailRequired = "Email is required.";

    /// <summary>
    /// Defines the UserNameRequired message.
    /// </summary>
    public const string UserNameRequired = "UserName is required.";

    /// <summary>
    /// Defines the FirstNameRequired message.
    /// </summary>
    public const string FirstNameRequired = "FirstName is required.";

    /// <summary>
    /// Defines the LastNameRequired message.
    /// </summary>
    public const string LastNameRequired = "LastName is required.";

    /// <summary>
    /// Defines the  EmployeeNo message.
    /// </summary>
    public const string EmployeeNoRequired = " Employee number is required.";

    /// <summary>
    /// The department required
    /// </summary>
    public const string DepartmentRequired = "Department is required.";

    /// <summary>
    /// The user role required
    /// </summary>
    public const string UserRoleRequired = "User role is required.";

    /// <summary>
    /// The created by required
    /// </summary>
    public const string CreatedByRequired = "Created by is required.";

    /// <summary>
    /// The updated by required
    /// </summary>
    public const string UpdatedByRequired = "Updated by is required.";

    /// <summary>
    /// The updated by required
    /// </summary>
    public const string OldPasswordRequired = "Current password by is required.";

    /// <summary>
    /// The updated by required
    /// </summary>
    public const string NewPasswordRequired = "New password is required.";

    /// <summary>
    /// The enable users limit exceed on create customer
    /// </summary>
    public const string EnableUsersLimitExceedOnCreateCustomer = "You have exceeded the limit for active users. Your information has been saved. To add a new user, please deactivate one of your existing users first.";

    /// <summary>
    /// The enable users limit exceed on create customer
    /// </summary>
    public const string EnableUsersLimitExceedOnUpdateCustomer = "Unable to enable user, due to number of permitted user reached. Please disable an existing user to enable a new user.";

    /// <summary>
    /// Defines the already registered.
    /// </summary>
    public const string AlreadyRegistered = "User with requested data already exists.";

    /// <summary>
    /// Defines the user is not registered.
    /// </summary>
    public const string NotRegistered = "User does not exists.";

    /// <summary>
    /// Defines the Generic Failed.
    /// </summary>
    public const string GenericFailed = "Request failed to complete";

    /// <summary>
    /// The user updated successfully
    /// </summary>
    public const string UserUpdatedSuccessfully = "User updated successfully";

    /// <summary>
    /// The user created successfully
    /// </summary>
    public const string UserCreatedSuccessfully = "User created successfully";

    /// <summary>
    /// The invalid user role
    /// </summary>
    public const string InvalidUserRole = "Invalid UserRole";

    /// <summary>
    /// The user not found
    /// </summary>
    public const string UserNotFound = "User not found";

    /// <summary>
    /// The invalid user role
    /// </summary>
    public const string UsersNotFound = "Users not found";

    /// <summary>
    /// The user found
    /// </summary>
    public const string UserFound = "User found";

    /// <summary>
    /// The users found
    /// </summary>
    public const string UsersFound = "Users found";

    /// <summary>
    /// The dashboard successful
    /// </summary>
    public const string DashboardSuccessful = "Dashboard item found";

    /// <summary>
    /// The dashboard failed
    /// </summary>
    public const string DashboardFailed = "Dashboard item not found";

    /// <summary>
    /// The functionalities found
    /// </summary>
    public const string FunctionalitiesFound = "Functionalities found successfully.";

    /// <summary>
    /// The functionalities not found
    /// </summary>
    public const string FunctionalitiesNotFound = "Functionalities not found.";

    /// <summary>
    /// The password successful
    /// </summary>
    public const string PasswordSuccessful = "Password updated successfully.";

    /// <summary>
    /// The password failed
    /// </summary>
    public const string PasswordFailed = "Failed to updated password.";

    /// <summary>
    /// The users enabled
    /// </summary>
    public const string UsersEnabled = "Users enabled successfully.";

    /// <summary>
    /// The users disabled
    /// </summary>
    public const string UsersDisabled = "Users disabled successfully.";

    /// <summary>
    /// The users disabled
    /// </summary>
    public const string SomethingWentWrong = "Something went wrong while updating data.";

    /// <summary>
    /// The no roles found
    /// </summary>
    public const string NoRolesFound = "No user types found for this user type";

    /// <summary>
    /// The roles found
    /// </summary>
    public const string RolesFound = "User types found";

    /// <summary>
    /// The please select at least once user
    /// </summary>
    public const string PleaseSelectAtLeastOnceUser = "Please select at least one user from the list";
}

