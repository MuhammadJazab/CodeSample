//-----------------------------------------------------------------------
// <copyright file="AutofacCacheAttribute.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.CacheManager.Registration;

/// <summary>
/// Defines the <see cref="AutofacCacheAttribute" />.
/// </summary>
public class AutofacCacheAttribute : ExportAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutofacCacheAttribute"/> class.
    /// </summary>
    /// <param name="contractType">The contractType<see cref="Type"/>.</param>
    /// <param name="contractName">The contractName<see cref="string"/>.</param>
    /// <param name="contractVersion">The contractVersion<see cref="string"/>.</param>
    /// <param name="cacheType">The cacheType<see cref="CacheType"/>.</param>
    public AutofacCacheAttribute(Type contractType, string contractName, string contractVersion, CacheType cacheType)
        : base(contractName, contractType)
    {
        this.ContractVersion = contractVersion;
        this.CacheType = cacheType;
    }

    /// <summary>
    /// Gets the ContractVersion.
    /// </summary>
    public string ContractVersion { get; }

    /// <summary>
    /// Gets the CacheType.
    /// </summary>
    public CacheType? CacheType { get; }
}