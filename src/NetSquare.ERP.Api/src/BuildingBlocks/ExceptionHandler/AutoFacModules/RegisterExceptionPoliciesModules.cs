//-----------------------------------------------------------------------
// <copyright file="RegisterExceptionPoliciesModules.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.ExceptionHandler.ExceptionHandler.AutoFacModules;

/// <summary>
/// Defines the <see cref="RegisterExceptionPoliciesModules" />.
/// </summary>
public class RegisterExceptionPoliciesModules : Module
{
    /// <summary>
    /// The Load.
    /// </summary>
    /// <param name="builder">The builder<see cref="ContainerBuilder" />.</param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AdminExceptionPolicy>()
            .Keyed<IExceptionHandler>(ExceptionPolicies.Admin.ToString())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterType<ClientExceptionPolicy>()
            .Keyed<IExceptionHandler>(ExceptionPolicies.Client.ToString())
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
}