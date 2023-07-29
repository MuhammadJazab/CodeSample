//-----------------------------------------------------------------------
// <copyright file="CacheType.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.CacheManager.Enums;

/// <summary>
/// Defines the CacheType.
/// </summary>
public enum CacheType
{
    /// <summary>
    /// Defines the RedisCache.
    /// </summary>
    RedisCache,

    /// <summary>
    /// Defines the InMemoryCache.
    /// </summary>
    InMemoryCache,
}