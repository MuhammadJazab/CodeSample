//file="ErrorMessages.cs" >

namespace Order.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="ErrorMessages" />.
/// </summary>
public static class ErrorMessages
{
    // <summary>
    /// Defines the UnAuthorized message.
    /// </summary>
    public const string UnAuthorized = "Unauthorized to perform this operation.";

    /// <summary>
    /// Defines the Generic Failed.
    /// </summary>
    public const string GenericFailed = "Request failed to complete";

    /// <summary>
    /// The disabled
    /// </summary>
    public const string Disabled = "Disabled user.";
}
