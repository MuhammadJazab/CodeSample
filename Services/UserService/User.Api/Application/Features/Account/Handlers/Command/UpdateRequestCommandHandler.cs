//file="UpdateRequestCommandHandler.cs" >

namespace User.Api.Application.Features.Account.Handlers.Command;

/// <summary>
/// Defines the <see cref="UpdateRequestCommandHandler" />.
/// </summary>
public class UpdateRequestCommandHandler : IRequestHandler<UpdateRequestCommand, UpdateResponse>
{
    /// <summary>
    /// Defines the userService.
    /// </summary>
    private readonly IUserService<ApplicationUser> userService;

    /// <summary>
    /// The context
    /// </summary>
    private readonly IHttpContextAccessor context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRequestCommandHandler"/> class.
    /// </summary>
    /// <param name="userService"><see cref="IUserService{ApplicationUser}"/>.</param>
    /// <param name="context"><see cref="HttpContextAccessor"/>.</param>
    public UpdateRequestCommandHandler(IUserService<ApplicationUser> userService, IHttpContextAccessor context)
    {
        this.userService = userService;
        this.context = context;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="UpdateRequestCommand"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{UpdateResponse}"/>.</returns>
    public async Task<UpdateResponse> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
    {
        UpdateResponse updateUserResponse = new();
        updateUserResponse.MessageSummary!.StatusCode = StatusCodes.Status200OK;

        var user = await this.userService.FindByIdAsync(request.UserId!);
        if (user is null)
        {
            updateUserResponse.MessageSummary!.StatusCode = StatusCodes.Status404NotFound;
            updateUserResponse.MessageSummary!.AddError(ErrorMessages.AlreadyRegistered, MessageDisplayTypes.All, ErrorCodes.AlreadyRegistered);
            return updateUserResponse;
        }

        if (!user.EnableUser)
        {
            updateUserResponse.MessageSummary!.StatusCode = StatusCodes.Status401Unauthorized;
            updateUserResponse.MessageSummary!.AddError(ErrorMessages.Disabled, MessageDisplayTypes.All, ErrorCodes.Disabled);
            return updateUserResponse;
        }

        user!.UserName = request.UserName;
        user!.FirstName = request.FirstName!;
        user!.LastName = request.LastName!;

        var result = await this.userService.UpdateAsync(user);

        if (!result.Succeeded)
        {
            updateUserResponse.MessageSummary!.StatusCode = StatusCodes.Status400BadRequest;
            updateUserResponse.MessageSummary!.AddRange(result.Errors.Select(x => new Message() { Code = x.Code, Text = x.Description }));
            return updateUserResponse;
        }

        return updateUserResponse;
    }
}

