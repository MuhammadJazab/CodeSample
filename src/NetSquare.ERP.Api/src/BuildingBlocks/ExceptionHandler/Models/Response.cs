//-----------------------------------------------------------------------
// <copyright file="Response.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Models;

/// <summary>
/// Defines the <see cref="Response" />.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class Response
{
    /// <summary>
    /// Gets or sets the MessagesSummary
    /// Gets or sets the MessagesSummary..
    /// </summary>
    public MessagesSummary MessagesSummary { get; set; }
}