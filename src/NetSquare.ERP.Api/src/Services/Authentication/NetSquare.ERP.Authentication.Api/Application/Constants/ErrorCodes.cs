//-----------------------------------------------------------------------
// <copyright file="ErrorCodes.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="ErrorCodes" />.
/// </summary>
public static class ErrorCodes
{
    /// <summary>
    /// The already registered
    /// </summary>
    public const string AlreadyRegistered = "7001";

    /// <summary>
    /// The generic failed
    /// </summary>
    public const string GenericFailed = "7002";

    /// <summary>
    /// The not registered
    /// </summary>
    public const string NotRegistered = "7003";

    /// <summary>
    /// The enable users limit exceed
    /// </summary>
    public const string EnableUsersLimitExceed = "7004";

    /// <summary>
    /// The user name required error code
    /// </summary>
    public const string UserNameRequired = "7005";

    /// <summary>
    /// The email required
    /// </summary>
    public const string EmailRequired = "7006";

    /// <summary>
    /// The first name required
    /// </summary>
    public const string FirstNameRequired = "7007";

    /// <summary>
    /// The last name required
    /// </summary>
    public const string LastNameRequired = "7008";

    /// <summary>
    /// The employee no required
    /// </summary>
    public const string EmployeeNoRequired = "7009";

    /// <summary>
    /// The department required
    /// </summary>
    public const string DepartmentRequired = "7010";

    /// <summary>
    /// The user role required
    /// </summary>
    public const string UserRoleRequired = "7011";

    /// <summary>
    /// The created by required
    /// </summary>
    public const string CreatedByRequired = "7012";

    /// <summary>
    /// Creates new passwordrequired.
    /// </summary>
    public const string NewPasswordRequired = "7013";

    /// <summary>
    /// The old password required
    /// </summary>
    public const string OldPasswordRequired = "7014";

    /// <summary>
    /// The user identifier required
    /// </summary>
    public const string UserIdRequired = "7015";

    /// <summary>
    /// The search query required
    /// </summary>
    public const string SearchQueryRequired = "7016";

    /// <summary>
    /// The updated by
    /// </summary>
    public const string UpdatedByRequired = "7017";

    /// <summary>
    /// The user updated successfully
    /// </summary>
    public const string UserUpdatedSuccessfully = "7018";

    /// <summary>
    /// The user created successfully
    /// </summary>
    public const string UserCreatedSuccessfully = "7019";

    /// <summary>
    /// The role not exists
    /// </summary>
    public const string InvalidUserRole = "7020";

    /// <summary>
    /// The role not exists
    /// </summary>
    public const string UsersNotFound = "7021";

    /// <summary>
    /// The users found
    /// </summary>
    public const string UsersFound = "7022";

    /// <summary>
    /// The user found
    /// </summary>
    public const string UserFound = "7023";

    /// <summary>
    /// The user not found
    /// </summary>
    public const string UserNotFound = "7024";

    /// <summary>
    /// The dashboard successful
    /// </summary>
    public const string DashboardSuccessful = "7025";

    /// <summary>
    /// The dashboard failed
    /// </summary>
    public const string DashboardFailed = "7026";

    /// <summary>
    /// The functionalities found
    /// </summary>
    public const string FunctionalitiesFound = "7027";

    /// <summary>
    /// The functionalities not found
    /// </summary>
    public const string FunctionalitiesNotFound = "7028";

    /// <summary>
    /// The password successful
    /// </summary>
    public const string PasswordSuccessful = "7029";

    /// <summary>
    /// The password failed
    /// </summary>
    public const string PasswordFailed = "7030";

    /// <summary>
    /// The users enabled
    /// </summary>
    public const string UsersEnabled = "7031";

    /// <summary>
    /// The users disabled
    /// </summary>
    public const string UsersDisabled = "7032";

    /// <summary>
    /// The no roles found
    /// </summary>
    public const string NoRolesFound = "7033";

    /// <summary>
    /// The roles found
    /// </summary>
    public const string RolesFound = "7034";

    /// <summary>
    /// Something went wrong
    /// </summary>
    public const string SomethingWentWrong = "7035";

    /// <summary>
    /// The please select at least once user
    /// </summary>
    public const string PleaseSelectAtLeastOnceUser = "7036";

}