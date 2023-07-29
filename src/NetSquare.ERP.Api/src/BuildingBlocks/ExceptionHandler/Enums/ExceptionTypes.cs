//-----------------------------------------------------------------------
// <copyright file="ExceptionTypes.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Enums;

/// <summary>
/// Defines the ExceptionTypes.
/// </summary>
public enum ExceptionTypes
{
    /// <summary>
    /// Defines the Application.
    /// </summary>
    [Description("Application Error")]
    Application,

    /// <summary>
    /// Defines the Authentication.
    /// </summary>
    [Description("Authentication Error")]
    Authentication,

    /// <summary>
    /// Defines the Connection.
    /// </summary>
    [Description("Connection Error")]
    Connection,

    /// <summary>
    /// Defines the Validation.
    /// </summary>
    [Description("Validation Error")]
    Validation,
}