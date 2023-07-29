//-----------------------------------------------------------------------
// <copyright file="GetUsersRequestQueryHandler.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Features.Account.Handlers.Queries;

/// <summary>
/// Defines the <see cref="GetUsersRequestQueryHandler" />.
/// </summary>
public class GetUsersRequestQueryHandler : IRequestHandler<GetUsersRequestQuery, List<GetUsersResponse>>
{
    /// <summary>
    /// Defines the loginService.
    /// </summary>
    private readonly IAuthenticationService<ApplicationUser> authenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersRequestQueryHandler"/> class.
    /// </summary>
    /// <param name="authenticationService">The loginService<see cref="ILoginService{ApplicationUser}"/>.</param>
    public GetUsersRequestQueryHandler(IAuthenticationService<ApplicationUser> authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    /// <summary>
    /// The Handle.
    /// </summary>
    /// <param name="request">The request<see cref="GetUsersRequestQuery"/>.</param>
    /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Task{GetUsersResponse}"/>.</returns>
    public async Task<List<GetUsersResponse>> Handle(GetUsersRequestQuery request, CancellationToken cancellationToken)
    {
        var users = await authenticationService.Users();

        if (users is null)
            return default!;

        return users.Select(user => new GetUsersResponse(user)).ToList()!;
    }
}

