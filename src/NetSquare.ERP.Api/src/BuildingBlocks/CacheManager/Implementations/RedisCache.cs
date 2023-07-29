//-----------------------------------------------------------------------
// <copyright file="RedisCache.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.CacheManager.Implementations;

/// <summary>
/// Defines the <see cref="RedisCache" />.
/// </summary>
[AutofacCache(typeof(ICache), "RedisCache", "1.0", CacheType.RedisCache)]
public class RedisCache : ICache, IDisposable
{
    /// <summary>
    /// Defines the manager.
    /// </summary>
    private ICacheManager<string> manager;

    /// <summary>
    /// Defines the disposedValue.
    /// </summary>
    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisCache"/> class.
    /// </summary>
    /// <param name="manager">The manager<see cref="ICacheManager{string}"/>.</param>
    public RedisCache(ICacheManager<string> manager)
    {
        this.manager = manager;
    }

    /// <summary>
    /// The Add.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Add(string key, string value)
    {
        return this.manager.Add(key, value);
    }

    /// <summary>
    /// The Add.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="value">The value<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Add(string key, string value, string region)
    {
        return this.manager.Add(key, value, region);
    }

    /// <summary>
    /// The ClearRegion.
    /// </summary>
    /// <param name="region">The region<see cref="string"/>.</param>
    public void ClearRegion(string region)
    {
        this.manager.ClearRegion(region);
    }

    /// <summary>
    /// The Exists.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Exists(string key, string region)
    {
        return this.manager.Exists(key, region);
    }

    /// <summary>
    /// The Exists.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Exists(string key)
    {
        return this.manager.Exists(key);
    }

    /// <summary>
    /// The Expire.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="mode">The mode<see cref="CacheExpirationMode"/>.</param>
    /// <param name="timeout">The timeout<see cref="TimeSpan"/>.</param>
    public void Expire(string key, CacheExpirationMode mode, TimeSpan timeout)
    {
        this.manager.Expire(key, (ExpirationMode)mode, timeout);
    }

    /// <summary>
    /// The Expire.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <param name="mode">The mode<see cref="CacheExpirationMode"/>.</param>
    /// <param name="timeout">The timeout<see cref="TimeSpan"/>.</param>
    public void Expire(string key, string region, CacheExpirationMode mode, TimeSpan timeout)
    {
        this.manager.Expire(key, region, (ExpirationMode)mode, timeout);
    }

    /// <summary>
    /// The Get.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Get(string key)
    {
        return this.manager.Get(key);
    }

    /// <summary>
    /// The Get.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Get(string key, string region)
    {
        return this.manager.Get(key, region);
    }

    /// <summary>
    /// The Get.
    /// </summary>
    /// <typeparam name="TOut">.</typeparam>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="TOut"/>.</returns>
    public TOut Get<TOut>(string key, string region)
    {
        return this.manager.Get<TOut>(key, region);
    }

    /// <summary>
    /// The Get.
    /// </summary>
    /// <typeparam name="TOut">.</typeparam>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="TOut"/>.</returns>
    public TOut Get<TOut>(string key)
    {
        return this.manager.Get<TOut>(key);
    }

    /// <summary>
    /// The Remove.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Remove(string key, string region)
    {
        return this.manager.Remove(key, region);
    }

    /// <summary>
    /// The Remove.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <returns>The <see cref="bool"/>.</returns>
    public bool Remove(string key)
    {
        return this.manager.Remove(key);
    }

    /// <summary>
    /// The Update.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="updateValue">The updateValue<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Update(string key, string updateValue)
    {
        return this.manager.Update(key, x => updateValue);
    }

    /// <summary>
    /// The Update.
    /// </summary>
    /// <param name="key">The key<see cref="string"/>.</param>
    /// <param name="region">The region<see cref="string"/>.</param>
    /// <param name="updateValue">The updateValue<see cref="string"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public string Update(string key, string region, string updateValue)
    {
        return this.manager.Update(key, region, x => updateValue);
    }

    /// <summary>
    /// The Dispose.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// The Dispose.
    /// </summary>
    /// <param name="disposing">The disposing<see cref="bool"/>.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposedValue)
        {
            if (disposing)
            {
                this.manager.Dispose();
                this.manager = null!;
            }

            this.disposedValue = true;
        }
    }
}