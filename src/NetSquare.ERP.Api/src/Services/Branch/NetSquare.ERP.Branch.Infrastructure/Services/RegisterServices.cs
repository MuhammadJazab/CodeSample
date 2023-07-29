//-----------------------------------------------------------------------
// <copyright file="RegisterServices.cs" company="NetSquare.ERP Limited">
// Copyright (c) NetSquare.ERP Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Infrastructure.Services;

/// <summary>
/// Defines the <see cref="RegisterServices" />.
/// </summary>
public class RegisterServices : Autofac.Module
{
    /// <summary>
    /// The Load.
    /// </summary>
    /// <param name="builder">The builder<see cref="ContainerBuilder"/>.</param>
    protected override void Load(ContainerBuilder builder)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerDependency();

        base.Load(builder);
    }
}