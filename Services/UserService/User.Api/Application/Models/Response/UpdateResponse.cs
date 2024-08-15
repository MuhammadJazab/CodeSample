
//file="UpdateResponse.cs" >

namespace User.Api.Application.Models.Response;

/// <summary>
/// Defines the <see cref="UpdateResponse" />.
/// </summary>
public class UpdateResponse : BaseResponse
{
    // <summary>
    /// Initializes a new instance of the <see cref="UpdateResponse"/> class.
    /// </summary>
    public UpdateResponse()
    {
        MessageSummary = new();
        MessageSummary!.StatusCode = StatusCodes.Status200OK;
    }
}

