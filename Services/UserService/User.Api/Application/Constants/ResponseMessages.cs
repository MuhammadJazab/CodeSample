//file="ResponseMessages.cs" >

namespace User.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="ResponseMessages" />.
/// </summary>
public static class ResponseMessages
{
    /// <summary>
    /// The token expired
    /// </summary>
    public static readonly Message UsersNotFound = new() { Code = "4001", Text = "Userd not found. Please try again!", Title = "Login Failed", MessageIndicatorType = nameof(MessageIndicatorTypes.Error) };

    /// <summary>
    /// The invalid user
    /// </summary>
    public static readonly Message InvalidUser = new() { Code = "4004", Text = "Invalid user", Title = "Login Failed", MessageIndicatorType = nameof(MessageIndicatorTypes.Error) };
}
