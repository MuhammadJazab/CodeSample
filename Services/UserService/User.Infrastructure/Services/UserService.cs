//file="LoginService.cs" >

namespace User.Infrastructure.Services;

/// <summary>
/// Defines the <see cref="UserService" />.
/// </summary>
public class UserService : IUserService<ApplicationUser>
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
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="userManager">The userManager<see cref="UserManager{ApplicationUser}"/>.</param>
    /// <param name="signInManager">The signInManager<see cref="SignInManager{ApplicationUser}"/>.</param>
    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    /// <summary>
    /// The FindByEmailAsync.
    /// </summary>
    /// <param name="email">The email<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{ApplicationUser?}"/>.</returns>
    public async Task<ApplicationUser?> FindByEmailAsync(string email) => await this.userManager.FindByEmailAsync(email);

    /// <summary>
    /// The FindByIdAsync.
    /// </summary>
    /// <param name="userId">The userId<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{ApplicationUser?}"/>.</returns>
    public async Task<ApplicationUser?> FindByIdAsync(string userId) => await this.userManager.FindByIdAsync(userId);

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
    /// The RegisterUser
    /// </summary>
    /// <param name="user"><see cref="ApplicationUser"/></param>
    /// <param name="password"><see cref="string"/></param>
    /// <returns><see cref="Task{IdentityResult}"/></returns>
    public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password) => await this.userManager.CreateAsync(user, password);

    /// <summary>
    /// The UpdateUser
    /// </summary>
    /// <param name="user"><see cref="ApplicationUser"/></param>
    /// <returns><see cref="Task{IdentityResult}"/></returns>
    public async Task<IdentityResult> UpdateAsync(ApplicationUser user) => await this.userManager.UpdateAsync(user);

    /// <summary>
    /// Get All Users
    /// </summary>
    /// <returns>List of Identity Users. <see cref="List{ApplicationUser}"/></returns>
    public async Task<List<ApplicationUser>> Users() => await this.userManager.Users.ToListAsync();
}