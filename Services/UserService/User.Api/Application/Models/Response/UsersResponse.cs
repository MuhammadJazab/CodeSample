
//file="Response.cs" >

namespace User.Api.Application.Models.Response;

/// <summary>
/// Defines the <see cref="UsersResponse" />.
/// </summary>
public class UsersResponse : BaseResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UsersResponse"/> class.
    /// </summary>
    public UsersResponse()
    {
        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }

    /// <summary>
    /// Gets or sets the Users.
    /// </summary>
    public List<ApplicationUser> Users { get; set; }
}