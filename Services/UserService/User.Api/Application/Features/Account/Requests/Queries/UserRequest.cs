//file="UserRequest.cs" >

namespace User.Api.Application.Features.Account.Requests.Queries;

/// <summary>
/// Defines the <see cref="UserRequest" />.
/// </summary>
public class UserRequest : IRequest<UserResponse>
{
    /// <summary>
    /// Gets or sets the UserId.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Define the <see cref="UserRequest"/>
    /// </summary>
    /// <param name="userId"></param>
    public UserRequest(string userId)
    {
        this.UserId = userId;
    }
}
