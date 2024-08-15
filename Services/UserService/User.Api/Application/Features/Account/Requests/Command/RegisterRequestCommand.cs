//file="RegisterRequestCommand.cs" >

namespace User.Api.Application.Features.Account.Requests.Command;

/// <summary>
/// Defines the <see cref="RegisterRequestCommand" />.
/// </summary>
public class RegisterRequestCommand : IRequest<RegisterResponse>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserRequestCommand"/> class.
    /// </summary>
    /// <param name="registerUserRequest"><see cref="RegisterUserRequest"/></param>
    public RegisterRequestCommand(Application.Models.Requests.RegisterRequest registerUserRequest)
    {
        this.UserName = registerUserRequest.UserName;
        this.Email = registerUserRequest.Email;
        this.FirstName = registerUserRequest.FirstName;
        this.LastName = registerUserRequest.LastName;
        this.Password = registerUserRequest.Password;
    }
}