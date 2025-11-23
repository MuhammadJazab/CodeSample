//file="UpdateRequestCommand.cs" >

namespace User.Api.Application.Features.Account.Requests.Command;

/// <summary>
/// Defines the <see cref="UpdateRequestCommand" />.
/// </summary>
public class UpdateRequestCommand : IRequest<UpdateResponse>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRequestCommand"/> class.
    /// </summary>
    /// <param name="UpdateRequest"><see cref="updateRequest"/></param>
    public UpdateRequestCommand(Application.Models.Requests.UpdateRequest updateRequest)
    {
        this.UserId = updateRequest.UserId;
        this.UserName = updateRequest.UserName;
        this.FirstName = updateRequest.FirstName;
        this.LastName = updateRequest.LastName;
    }
}

