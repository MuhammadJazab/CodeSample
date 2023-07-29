//-----------------------------------------------------------------------
// <copyright file="CacheContainerFactory.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.CacheManager.Registration;

/// <summary>
/// Defines the <see cref="CacheContainerFactory" />.
/// </summary>
public static class CacheContainerFactory
{
    /// <summary>
    /// Gets or sets the Container.
    /// </summary>
    public static IContainer? Container { get; set; }

    /// <summary>
    /// The ResolveNamedManager.
    /// </summary>
    /// <typeparam name="TICache">.</typeparam>
    /// <param name="serviceName">The serviceName<see cref="string"/>.</param>
    /// <param name="version">The version<see cref="string"/>.</param>
    /// <param name="type">The type<see cref="CacheType"/>.</param>
    /// <param name="manager">The manager<see cref="ICacheManager{string}"/>.</param>
    /// <returns>The <see cref="TICache"/>.</returns>
    public static void ResolveNamedManager<TICache>(string serviceName, string version, CacheType type, ICacheManager<string> manager)
    {
        ContainerBuilder builder = new();

        builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            .Where(t => t.GetCustomAttribute<AutofacCacheAttribute>() != null)
            .Named<TICache>(t => t.GetNameForRegistration())
            .SingleInstance();

        return;
    }

    /// <summary>
    /// The GetNameForRegistration.
    /// </summary>
    /// <param name="type">The type<see cref="Type"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    private static string GetNameForRegistration(this Type type)
    {
        if (type != null && type.IsClass && !type.IsAbstract)
        {
            if (type.GetCustomAttribute(typeof(AutofacCacheAttribute)) is AutofacCacheAttribute attribute)
            {
                List<string> items = new()
                {
                    attribute?.ContractName!,
                    attribute?.ContractVersion!,
                    attribute?.ContractType.FullName!,
                    attribute!.CacheType.HasValue ? attribute?.CacheType.ToString()! : string.Empty,
                };

                return string.Join("_", items);
            }

            return string.Empty;
        }

        // need to return exception
        return string.Empty;
    }
}