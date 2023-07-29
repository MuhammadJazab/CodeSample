//-----------------------------------------------------------------------
// <copyright file="CacheExpirationMode.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.CacheManager.Enums;

/// <summary>
/// Defines the CacheExpirationMode.
/// </summary>
public enum CacheExpirationMode
{
    /// <summary>
    ///     The default.
    /// </summary>
    Default = 0,
    /// <summary>
    ///     The none.
    /// </summary>
    None = 1,
    /// <summary>
    ///     The sliding.
    /// </summary>
    Sliding = 2,
    /// <summary>
    ///     The absolute.
    /// </summary>
    Absolute = 3,
}