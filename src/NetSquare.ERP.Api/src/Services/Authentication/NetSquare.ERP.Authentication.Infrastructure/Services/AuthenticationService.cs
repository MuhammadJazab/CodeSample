//-----------------------------------------------------------------------
// <copyright file="AuthenticationService.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Infrastructure.Services;

/// <summary>
/// Defines the <see cref="AuthenticationService" />.
/// </summary>
public class AuthenticationService : IAuthenticationService<ApplicationUser>
{
    /// <summary>
    /// Defines the userManager.
    /// </summary>
    private readonly UserManager<ApplicationUser> userManager;

    /// <summary>
    /// Defines the signInManager.
    /// </summary>
    private readonly SignInManager<ApplicationUser> signInManager;

    /// <summary>
    /// Defines the roleManager.
    /// </summary>
    private readonly RoleManager<ApplicationRole> roleManager;

    /// <summary>
    /// The httpContextAccessor
    /// </summary>
    ////private readonly HttpContextAccessor httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    /// <param name="userManager">The userManager<see cref="UserManager{ApplicationUser}"/>.</param>
    /// <param name="signInManager">The signInManager<see cref="SignInManager{ApplicationUser}"/>.</param>
    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager
        /*HttpContextAccessor httpContextAccessor*/)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
        ////this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// The FindByEmailAsync.
    /// </summary>
    /// <param name="email">The email<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{ApplicationUser?}"/>.</returns>
    public async Task<ApplicationUser?> FindByEmailAsync(string email) => await this.userManager.FindByEmailAsync(email);

    /// <summary>
    /// Get the role name with Id
    /// </summary>
    /// <param name="user"></param>
    /// <param name="role"></param>
    /// <returns><see cref="Task{ApplicationRole}"/></returns>
    public async Task<string> FindRoleNameByIdAsync(string role) => (await this.roleManager.FindByIdAsync(role))?.Name!;

    /// <summary>
    /// The ValidateCredentials.
    /// </summary>
    /// <param name="user">The user<see cref="ApplicationUser"/>.</param>
    /// <param name="password">The password<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{bool}"/>.</returns>
    public async Task<bool> ValidateCredentials(ApplicationUser user, string password) => await this.userManager.CheckPasswordAsync(user, password);

    /// <summary>
    /// The SignIn.
    /// </summary>
    /// <param name="user">The user<see cref="ApplicationUser"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public Task SignIn(ApplicationUser user) => this.signInManager.SignInAsync(user, true);

    /// <summary>
    /// The SignInAsync.
    /// </summary>
    /// <param name="user">The user<see cref="ApplicationUser"/>.</param>
    /// <param name="password">The password<see cref="string"/>.</param>
    /// <param name="isPersistent">The isPersistent<see cref="bool"/>.</param>
    /// <param name="LockOutOnFailure">The LockOutOnFailure<see cref="bool"/>.</param>
    /// <returns>The <see cref="Task{SignInResult}"/>.</returns>
    public async Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool isPersistent = false, bool LockOutOnFailure = false) => await this.signInManager.PasswordSignInAsync(user, password, isPersistent, LockOutOnFailure);

    /// <summary>
    /// The GetClaimsAsync.
    /// </summary>
    /// <param name="user">The user<see cref="ApplicationUser"/>.</param>
    /// <returns>The <see cref="Task{IList{Claim}}"/>.</returns>
    public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user) => await this.userManager.GetClaimsAsync(user);

    /// <summary>
    /// The GetRolesAsync.
    /// </summary>
    /// <param name="user">The user<see cref="ApplicationUser"/>.</param>
    /// <returns>The <see cref="Task{IList{string}}"/>.</returns>
    public async Task<IList<string>> GetRolesAsync(ApplicationUser user) => await this.userManager.GetRolesAsync(user);

    /// <summary>
    /// The RegisterUser
    /// </summary>
    /// <param name="user"><see cref="ApplicationUser"/></param>
    /// <returns><see cref="Task{IdentityResult}"/></returns>
    public async Task<IdentityResult> RegisterAsync(ApplicationUser user) => await this.userManager.CreateAsync(user);

    /// <summary>
    /// The RoleExistsAsync
    /// </summary>
    /// <param name="role"><see cref="bool"/></param>
    /// <returns></returns>
    public async Task<bool> RoleExistsAsync(string role) => await this.roleManager.RoleExistsAsync(role);

    /// <summary>
    /// The GetRoleByIdAsync.
    /// </summary>
    /// <param name="rolrId">The user<see cref="Guid"/>.</param>
    /// <returns>The role name<see cref="Task{string}"/>.</returns>
    public async Task<string?> GetRoleByIdAsync(Guid roleId) => await this.roleManager.GetRoleNameAsync(new ApplicationRole { Id = roleId});

    /// <summary>
    /// The CreateRoleAsync
    /// </summary>
    /// <param name="role"><see cref="string"/></param>
    /// <returns><see cref="IdentityResult"/></returns>
    public async Task<IdentityResult> CreateRoleAsync(string role) => await this.roleManager.CreateAsync(new ApplicationRole
    {
        Name = role,
        CreatedOn = DateTime.UtcNow,
        ////CreatedBy = this.httpContextAccessor.GetUserId().ToString()
    });

    /// <summary>
    /// The AddToRoleAsync
    /// </summary>
    /// <param name="user"><see cref="ApplicationUser"/></param>
    /// <param name="role"><see cref="string"/></param>
    /// <returns><see cref="IdentityResult"/></returns>
    public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role) =>
        await this.userManager.AddToRoleAsync(user: user, role: role);

    /// <summary>
    /// Get All Users
    /// </summary>
    /// <returns>List of Identity Users. <see cref="List{ApplicationUser}"/></returns>
    public async Task<List<ApplicationUser>> Users() => await this.userManager.Users.ToListAsync();
}