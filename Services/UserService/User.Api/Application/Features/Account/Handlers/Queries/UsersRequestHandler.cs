//file="UsersRequestHandler.cs" >

namespace User.Api.Application.Features.Account.Handlers.Queries;

/// <summary>
/// Defines the <see cref="UsersRequestHandler" />.
/// </summary>
public class UsersRequestHandler : IRequestHandler<UsersRequest, UsersResponse>
{
    /// <summary>
    /// Defines the userService.
    /// </summary>
    private readonly IUserService<ApplicationUser> userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersRequestHandler"/> class.
    /// </summary>
    /// <param name="userService"><see cref="IUserService{ApplicationUser}"/>.</param>
    public UsersRequestHandler(IUserService<ApplicationUser> userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="UsersRequest"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{AuthenticationResponse}"/>.</returns>
    public async Task<UsersResponse> Handle(UsersRequest request, CancellationToken cancellationToken)
    {
        UsersResponse response = new();
        response.MessageSummary!.StatusCode = StatusCodes.Status200OK;

        var users = await userService.Users();

        if (users == null)
        {
            response.MessageSummary!.Add(ResponseMessages.UsersNotFound);
            response.MessageSummary!.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            response.Users = users;
        }

        return response;
    }
}