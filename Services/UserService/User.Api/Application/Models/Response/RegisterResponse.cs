
//file="RegisterResponse.cs" >

namespace User.Api.Application.Models.Response;

/// <summary>
/// Defines the <see cref="RegisterResponse" />.
/// </summary>
public class RegisterResponse : BaseResponse
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterResponse"/> class.
    /// </summary>
    public RegisterResponse()
    {
        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }
}

