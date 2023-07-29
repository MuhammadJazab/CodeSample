//-----------------------------------------------------------------------
// <copyright file="BaseResponse.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Domain.Common;

/// <summary>
/// Defines the <see cref="BaseResponse" />.
/// </summary>
public abstract class BaseResponse
{
    /// <summary>
    /// Gets or sets the MessageSummary.
    /// </summary>
    public MessagesSummary? MessageSummary { get; set; }
}
