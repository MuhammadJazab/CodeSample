//file="UserResponse.cs" >

namespace User.Api.Application.Models.Response;

/// <summary>
/// Defines the <see cref="UserResponse" />.
/// </summary>
public class UserResponse : BaseResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserResponse"/> class.
    /// </summary>
    public UserResponse()
    {
        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }

    /// <summary>
    /// Gets or sets the User.
    /// </summary>
    public ApplicationUser User { get; set; }
}