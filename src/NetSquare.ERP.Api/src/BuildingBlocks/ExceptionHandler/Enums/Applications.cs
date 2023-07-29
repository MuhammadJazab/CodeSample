//-----------------------------------------------------------------------
// <copyright file="Applications.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.Enums;

/// <summary>
/// Defines the Applications.
/// </summary>
public enum Applications
{
    /// <summary>
    /// Defines the Gateway.
    /// </summary>
    [Description("GATEWAY")]
    Gateway,

    /// <summary>
    /// Defines the Authentication.
    /// </summary>
    [Description("AUTHENTICATION")]
    Authentication,

    /// <summary>
    /// Defines the Authentication.
    /// </summary>
    [Description("BRANCH")]
    Branch,

    /// <summary>
    /// Defines the Authentication.
    /// </summary>
    [Description("DEPARTMENT")]
    Department,

    /// <summary>
    /// Defines the Authentication.
    /// </summary>
    [Description("gRPC")]
    gRPCService,

    /// <summary>
    /// Defines the GroupCapacity.
    /// </summary>
    [Description("ATTANDENCE")]
    HumanResource,

    /// <summary>
    /// Defines the Web.
    /// </summary>
    [Description("WEB")]
    Web
}