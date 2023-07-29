//-----------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Common;

/// <summary>
/// Defines the <see cref="BaseEntity" />.
/// </summary>
public abstract class BaseEntity
{
    public object? BaseProperty { get; set; }
}