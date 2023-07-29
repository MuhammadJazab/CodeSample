//-----------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.SeedWork;

/// <summary>
/// Defines the <see cref="IAuthenticationService{T}" />.
/// </summary>
/// <typeparam name="T">.</typeparam>
public interface IAuthenticationService<T>
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
    /// The RegisterAsync
    /// </summary>
    /// <param name="user">The user<see cref="T"/>.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    Task<IdentityResult> RegisterAsync(T user);

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
    /// The GetRolesAsync.
    /// </summary>
    /// <param name="user">The user<see cref="T"/>.</param>
    /// <returns>The <see cref="Task{IList{string}}"/>.</returns>
    Task<IList<string>> GetRolesAsync(T user);

    /// <summary>
    /// The RoleExistsAsync.
    /// </summary>
    /// <param name="role"><see cref="string"/></param>
    /// <returns>The <see cref="Task{bool}"/>.</returns>
    Task<bool> RoleExistsAsync(string role);

    /// <summary>
    /// The GetRoleByIdAsync.
    /// </summary>
    /// <param name="rolrId">The user<see cref="Guid"/>.</param>
    /// <returns>The role name<see cref="Task{string}"/>.</returns>
    Task<string?> GetRoleByIdAsync(Guid roleId);

    /// <summary>
    /// The CreateRoleAsync
    /// </summary>
    /// <param name="role"><see cref="string"/></param>
    /// <returns><see cref="Task{IdentityResult}"/></returns>
    Task<IdentityResult> CreateRoleAsync(string role);

    /// <summary>
    /// The AddToRoleAsync
    /// </summary>
    /// <param name="user"><see cref="ApplicationUser"/></param>
    /// <param name="role"><see cref="string"/></param>
    /// <returns><see cref="IdentityResult"/></returns>
    Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

    /// <summary>
    /// Get All Users
    /// </summary>
    /// <returns>List of Identity Users. <see cref="List{ApplicationUser}"/></returns>
    Task<List<ApplicationUser>> Users();

    /// <summary>
    /// Get the role name with Id
    /// </summary>
    /// <param name="role"></param>
    /// <returns><see cref="Task{ApplicationRole}"/></returns>
    Task<string> FindRoleNameByIdAsync(string role);
}