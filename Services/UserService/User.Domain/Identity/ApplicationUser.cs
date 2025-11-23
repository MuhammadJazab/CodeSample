//file="ApplicationUser.cs" >

namespace User.Domain.Identity;

/// <summary>
/// Defines the <see cref="ApplicationUser" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the FirstName.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the LastName.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Get or set the PasswordExpire
    /// </summary>
    public bool PasswordExpire { get; set; }

    /// <summary>
    /// Get or set the PasswordLocked
    /// </summary>
    public bool PasswordLocked { get; set; }

    /// <summary>
    /// Get or set the EnableUser
    /// </summary>
    public bool EnableUser { get; set; }
}