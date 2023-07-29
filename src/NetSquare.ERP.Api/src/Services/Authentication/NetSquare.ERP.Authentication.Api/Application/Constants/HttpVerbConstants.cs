//-----------------------------------------------------------------------
// <copyright file="HttpVerbConstants.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Application.Constants;

/// <summary>
/// Defines the <see cref="HttpVerbConstants" />.
/// </summary>
public static class HttpVerbConstants
{
    /// <summary>
    /// Defines the Login.
    /// </summary>
    public const string Login = "Login";

    /// <summary>
    /// Defines the Register.
    /// </summary>
    public const string Register = "Register";

    /// <summary>
    /// Defines the search
    /// </summary>
    public const string SEARCH = "searchuser/{searchQuery}";

    /// <summary>
    /// Defines the id
    /// </summary>
    public const string ID = "{id}";
}