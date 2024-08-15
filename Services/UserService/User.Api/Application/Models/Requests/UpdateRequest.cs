//file="UpdateRequest.cs" >

namespace User.Api.Application.Models.Requests;

/// <summary>
/// Defines the <see cref="UpdateRequest" />.
/// </summary>
public class UpdateRequest
{
    /// <summary>
    /// Gets or sets the UserId.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets the UserName.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string? LastName { get; set; }
}