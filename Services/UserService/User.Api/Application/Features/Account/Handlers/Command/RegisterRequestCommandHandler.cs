//file="RegisterRequestCommandHandler.cs" >

namespace User.Api.Application.Features.Account.Handlers.Command;

/// <summary>
/// Defines the <see cref="RegisterRequestCommandHandler" />.
/// </summary>
public class RegisterRequestCommandHandler : IRequestHandler<RegisterRequestCommand, RegisterResponse>
{
    /// <summary>
    /// Defines the userService.
    /// </summary>
    private readonly IUserService<ApplicationUser> userService;

    /// <param name="userService"><see cref="IUserService{ApplicationUser}"/>.</param>
    /// <param name="publishEndpoint"><see cref="IPublishEndpoint"/>.</param>
    public RegisterRequestCommandHandler(IUserService<ApplicationUser> userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="RegisterRequestCommand"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{RegisterResponse}"/>.</returns>
    public async Task<RegisterResponse> Handle(RegisterRequestCommand request, CancellationToken cancellationToken)
    {
        RegisterResponse registerUserResponse = new();
        registerUserResponse.MessageSummary!.StatusCode = StatusCodes.Status200OK;

        var user = await this.userService.FindByEmailAsync(request.Email!);

        if (user is not null)
        {
            registerUserResponse.MessageSummary!.StatusCode = StatusCodes.Status404NotFound;
            registerUserResponse.MessageSummary!.AddError(ErrorMessages.AlreadyRegistered, MessageDisplayTypes.All, ErrorCodes.AlreadyRegistered);
            return registerUserResponse;
        }

        ApplicationUser applicationUser = new()
        {
            UserName = request.UserName,
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            Email = request.Email,
            EnableUser = true
        };

        var result = await this.userService.RegisterAsync(applicationUser, request.Password);

        if (!result.Succeeded)
        {
            registerUserResponse.MessageSummary!.StatusCode = StatusCodes.Status400BadRequest;
            registerUserResponse.MessageSummary!.AddRange(result.Errors.Select(x => new Message() { Code = x.Code, Text = x.Description }));
            registerUserResponse.MessageSummary!.AddError(ErrorMessages.GenericFailed, MessageDisplayTypes.All, ErrorCodes.GenericFailed);
            return registerUserResponse;
        }

        return registerUserResponse;
    }
}