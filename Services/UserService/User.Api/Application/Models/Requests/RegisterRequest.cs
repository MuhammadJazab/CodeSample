
//file="RegisterRequest.cs" >

namespace User.Api.Application.Models.Requests;

/// <summary>
/// Defines the <see cref="RegisterRequest" />.
/// </summary>
public class RegisterRequest
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
    /// Gets or sets the FirstName.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}