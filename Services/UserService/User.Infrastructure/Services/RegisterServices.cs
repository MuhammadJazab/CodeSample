
//file="RegisterServices.cs" >



namespace User.Infrastructure.Services;

/// <summary>
/// Defines the <see cref="RegisterServices" />.
/// </summary>
[ExcludeFromCodeCoverage]
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

