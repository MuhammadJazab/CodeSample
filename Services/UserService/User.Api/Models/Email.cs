//file="Email.cs" >

namespace User.Api.Models;

/// <summary>
/// Defines the <see cref="Email" />.
/// </summary>
public class Email
{
    /// <summary>
    /// Defines the To.
    /// </summary>
    public string? To { get; set; }

    /// <summary>
    /// Defines the Subject.
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Defines the Body.
    /// </summary>
    public string? Body { get; set; }
}

