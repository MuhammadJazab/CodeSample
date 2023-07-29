//-----------------------------------------------------------------------
// <copyright file="GenericTypeExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.EventBus.Extensions;

/// <summary>
/// Defines the <see cref="GenericTypeExtensions" />.
/// </summary>
public static class GenericTypeExtensions
{
    /// <summary>
    /// The GetGenericTypeName.
    /// </summary>
    /// <param name="type">The type<see cref="Type"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public static string GetGenericTypeName(this Type type)
    {
        string typeName;

        if (type.IsGenericType)
        {
            var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
            typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
        }
        else
        {
            typeName = type.Name;
        }

        return typeName;
    }

    /// <summary>
    /// The GetGenericTypeName.
    /// </summary>
    /// <param name="@object">The object<see cref="object"/>.</param>
    /// <returns>The <see cref="string"/>.</returns>
    public static string GetGenericTypeName(this object @object)
    {
        return @object.GetType().GetGenericTypeName();
    }
}