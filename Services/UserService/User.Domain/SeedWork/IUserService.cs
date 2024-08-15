
//file="IUserService.cs" >

namespace User.Domain.SeedWork;

/// <summary>
/// Defines the <see cref="IUserService{T}" />.
/// </summary>
/// <typeparam name="T">.</typeparam>
public interface IUserService<T>
{
    /// <summary>
    /// The ValidateCredentials.
    /// </summary>
    /// <param name="user">The user<see cref="T"/>.</param>
    /// <param name="password">The password<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{bool}"/>.</returns>
    Task<bool> ValidateCredentials(T user, string password);

    /// <summary>
    /// The FindByEmailAsync.
    /// </summary>
    /// <param name="email">The email<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{T?}"/>.</returns>
    Task<T?> FindByEmailAsync(string email);

    /// <summary>
    /// The SignIn.
    /// </summary>
    /// <param name="user">The user<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task SignIn(T user);

    /// <summary>
    /// The RegisterUser
    /// </summary>
    /// <param name="user"><see cref="ApplicationUser"/></param>
    /// <param name="password"><see cref="string"/></param>
    /// <returns><see cref="Task{IdentityResult}"/></returns>
    Task<IdentityResult> RegisterAsync(T user, string password);

    /// <summary>
    /// The UpdateUser
    /// </summary>
    /// <param name="user"><see cref="ApplicationUser"/></param>
    /// <returns><see cref="Task{IdentityResult}"/></returns>
    Task<IdentityResult> UpdateAsync(ApplicationUser user);

    /// <summary>
    /// The SignInAsync.
    /// </summary>
    /// <param name="user">The user<see cref="ApplicationUser"/>.</param>
    /// <param name="password">The password<see cref="string"/>.</param>
    /// <param name="isPersistent">The isPersistent<see cref="bool"/>.</param>
    /// <param name="LockOutOnFailure">The LockOutOnFailure<see cref="bool"/>.</param>
    /// <returns>The <see cref="Task{SignInResult}"/>.</returns>
    Task<SignInResult> SignInAsync(ApplicationUser user, string password, bool isPersistent = false, bool LockOutOnFailure = false);

    /// <summary>
    /// The GetClaimsAsync.
    /// </summary>
    /// <param name="user">The user<see cref="T"/>.</param>
    /// <returns>The <see cref="Task{IList{Claim}}"/>.</returns>
    Task<IList<Claim>> GetClaimsAsync(T user);

    /// <summary>
    /// Get All Users
    /// </summary>
    /// <returns>List of Identity Users. <see cref="List{ApplicationUser}"/></returns>
    Task<List<ApplicationUser>> Users();

    /// <summary>
    /// The FindByIdAsync.
    /// </summary>
    /// <param name="userId">The userId<see cref="string"/>.</param>
    /// <returns>The <see cref="Task{ApplicationUser?}"/>.</returns>
    Task<ApplicationUser?> FindByIdAsync(string userId);
}