//file="UserRequestHandler.cs" >

namespace User.Api.Application.Features.Account.Handlers.Queries;

/// <summary>
/// Defines the <see cref="UserRequestHandler" />.
/// </summary>
public class UserRequestHandler : IRequestHandler<UserRequest, UserResponse>
{
    /// <summary>
    /// Defines the userService.
    /// </summary>
    private readonly IUserService<ApplicationUser> userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRequestHandler"/> class.
    /// </summary>
    /// <param name="userService"><see cref="IUserService{ApplicationUser}"/>.</param>
    public UserRequestHandler(IUserService<ApplicationUser> userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="UsersRequest"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{AuthenticationResponse}"/>.</returns>
    public async Task<UserResponse> Handle(UserRequest request, CancellationToken cancellationToken)
    {
        UserResponse response = new();
        response.MessageSummary!.StatusCode = StatusCodes.Status200OK;

        var users = await userService.FindByIdAsync(request.UserId);

        if (users == null)
        {
            response.MessageSummary!.Add(ResponseMessages.UsersNotFound);
            response.MessageSummary!.StatusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            response.User = users;
        }

        return response;
    }
}