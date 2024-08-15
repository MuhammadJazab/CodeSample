//file="ErrorMessages.cs" >

namespace User.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="ErrorMessages" />.
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// Defines the UserIdRequired message.
    /// </summary>
    public const string UserIdRequired = "UserId is required.";

    // <summary>
    /// Defines the UnAuthorized message.
    /// </summary>
    public const string UnAuthorized = "Unauthorized to perform this operation.";

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
    /// The invalid user role
    /// </summary>
    public const string InvalidUserRole = "Invalid UserRole";

    /// <summary>
    /// The user not found
    /// </summary>
    public const string UserNotFound = "User not found";

    /// <summary>
    /// The user found
    /// </summary>
    public const string UserFound = "User found";

    /// <summary>
    /// The disabled
    /// </summary>
    public const string Disabled = "Disabled user.";
}
