//-----------------------------------------------------------------------
// <copyright file="ICache.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.CacheManager.Cache;

/// <summary>
/// Defines the <see cref="ICache" />.
/// </summary>
public interface ICache
{
    /// <summary>
    /// The Add.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    bool Add(string key, string value);

    /// <summary>
    /// The Add.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    bool Add(string key, string value, string region);

    /// <summary>
    /// The Get.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Get(string key, string region);

    /// <summary>
    /// The Get.
    /// </summary>
    /// <typeparam name="TOut">.</typeparam>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="TOut"/>.</returns>
    public TOut Get<TOut>(string key, string region);

    /// <summary>
    /// The Get.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Get(string key);

    /// <summary>
    /// The Get.
    /// </summary>
    /// <typeparam name="TOut">.</typeparam>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="TOut"/>.</returns>
    public TOut Get<TOut>(string key);

    /// <summary>
    /// The Exists.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Exists(string key, string region);

    /// <summary>
    /// The Exists.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Exists(string key);

    /// <summary>
    /// The Update.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="updateValue">The updateValue<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Update(string key, string updateValue);

    /// <summary>
    /// The Update.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <param name="updateValue">The updateValue<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Update(string key, string region, string updateValue);

    /// <summary>
    /// The Remove.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Remove(string key, string region);

    /// <summary>
    /// The Remove.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Remove(string key);

    /// <summary>
    /// The ClearRegion.
    /// </summary>
    /// <param name="region">The region<see cref="string"/>.</param>
    void ClearRegion(string region);

    /// <summary>
    /// The Expire.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="mode">The mode<see cref="CacheExpirationMode"/>.</param>
    /// <param name="timeout">The timeout<see cref="TimeSpan"/>.</param>
    public void Expire(string key, CacheExpirationMode mode, TimeSpan timeout);

    /// <summary>
    /// The Expire.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <param name="mode">The mode<see cref="CacheExpirationMode"/>.</param>
    /// <param name="timeout">The timeout<see cref="TimeSpan"/>.</param>
    public void Expire(string key, string region, CacheExpirationMode mode, TimeSpan timeout);
}