//-----------------------------------------------------------------------
// <copyright file="BaseRequest.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Common;

/// <summary>
/// Defines the <see cref="BaseRequest" />.
/// </summary>
public abstract class BaseRequest
{
    public object? BaseProperty { get; set; }
}