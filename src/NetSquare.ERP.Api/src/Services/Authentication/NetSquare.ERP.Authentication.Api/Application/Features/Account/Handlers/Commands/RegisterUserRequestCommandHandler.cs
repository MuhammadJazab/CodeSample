//-----------------------------------------------------------------------
// <copyright file="RegisterUserRequestCommandHandler.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Grpc.Net.Client;

namespace NetSquare.ERP.Authentication.Api.Application.Features.Account.Handlers.Commands;

/// <summary>
/// Defines the <see cref="RegisterUserRequestCommandHandler" />.
/// </summary>
public class RegisterUserRequestCommandHandler : IRequestHandler<RegisterUserRequestCommand, RegisterUserResponse>
{
    /// <summary>
    /// Defines the loginService.
    /// </summary>
    private readonly IAuthenticationService<ApplicationUser> authenticationService;

    /// <summary>
    /// Defines the IHttpContextAccessor
    /// </summary>
    private readonly IHttpContextAccessor context;

    /// <summary>
    /// Defines the GrpcConfiguration
    /// </summary>
    private readonly GrpcConfiguration grpcConfiguration;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterUserRequestCommandHandler"/> class.
    /// </summary>
    /// <param name="authenticationService">The loginService<see cref="ILoginService{ApplicationUser}"/>.</param>
    /// <param name="jwtConfig">The jwtSettings<see cref="IOptions{JwtSettings}"/>.</param>
    /// <param name="context">The jwtSettings<see cref="IHttpContextAccessor"/>.</param>
    public RegisterUserRequestCommandHandler(
        IAuthenticationService<ApplicationUser> authenticationService,
        IOptions<JwtConfiguration> jwtConfig,
        IOptions<GrpcConfiguration> grpcConfig,
        IHttpContextAccessor context)
    {
        this.authenticationService = authenticationService;
        this.context = context;
        this.grpcConfiguration = grpcConfig.Value;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="UserLoginRequestQuery"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{RegisterUserResponse}"/>.</returns>
    public async Task<RegisterUserResponse> Handle(RegisterUserRequestCommand request, CancellationToken cancellationToken)
    {
        RegisterUserResponse registerUserResponse = new();
        registerUserResponse.MessageSummary!.StatusCode = StatusCodes.Status200OK;
        registerUserResponse.MessageSummary.TraceId = context!.HttpContext!.TraceIdentifier;

        var user = await this.authenticationService.FindByEmailAsync(request.Email!);

        if (user is not null)
        {
            registerUserResponse.MessageSummary!.StatusCode = StatusCodes.Status404NotFound;
            registerUserResponse.MessageSummary!.AddError(ErrorMessages.AlreadyRegistered, MessageDisplayTypes.All, ErrorCodes.AlreadyRegistered);
            return registerUserResponse;
        }

        var role = await this.authenticationService.FindRoleNameByIdAsync(request.Role!);
        if (string.IsNullOrEmpty(role))
        {
            registerUserResponse.MessageSummary!.AddError(ErrorMessages.InvalidUserRole, MessageDisplayTypes.All, ErrorCodes.InvalidUserRole);
            registerUserResponse.MessageSummary!.StatusCode = StatusCodes.Status403Forbidden;
            return registerUserResponse;
        }

        ApplicationUser applicationUser = new()
        {
            UserName = request.UserName,
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            Email = request.Email,
            PhoneNumber = request.Phone,
            CreatedOn = DateTime.UtcNow,
            ////CreatedBy = context
        };

        var result = await this.authenticationService.RegisterAsync(applicationUser);

        if (!result.Succeeded)
        {
            registerUserResponse.MessageSummary!.StatusCode = StatusCodes.Status400BadRequest;
            registerUserResponse.MessageSummary!.AddRange(result.Errors.Select(x => new Message() { Code = x.Code, Text = x.Description }));
            registerUserResponse.MessageSummary!.AddError(ErrorMessages.GenericFailed, MessageDisplayTypes.All, ErrorCodes.GenericFailed);
            return registerUserResponse;
        }

        await this.authenticationService.AddToRoleAsync(user: applicationUser, role: request.Role!);

        await AddBranchUser(userId: applicationUser.Id, branchId: request.BranchId, context: context);

        return registerUserResponse;

        //// added user's role now need to add user's associated , branches, Allowences table.

    }

    /// <summary>
    /// Defines the Grpc request to add user to the branch
    /// </summary>
    /// <returns></returns>
    private async void AddBranchUser(Guid userId, Guid branchId, IHttpContextAccessor context)
    {
        Metadata headers = new()
        {
            { grpcConfiguration.AuthorizationHeader!, context.HttpContext?.Request.Headers[grpcConfiguration.AuthorizationHeader!]! }
        };

        var channel = GrpcChannel.ForAddress(grpcConfiguration.GrpcServiceUrl!);
        BranchGrpc.BranchGrpcClient branchGrpcClient = new(channel);

        AddUserToBranchRequest addUserToBranchRequest = new()
        {
            UserId = userId.ToString(),
            BranchId = branchId.ToString()
        };

        var response = await branchGrpcClient.AddUserToBranchAsync(request: addUserToBranchRequest, headers: headers);

       
    }
}