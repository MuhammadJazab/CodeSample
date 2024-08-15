//file="RegisterRepositories.cs" >

namespace Order.Infrastructure.Repositories;

/// <summary>
/// Defines the <see cref="RegisterRepositories" />.
/// </summary>
public class RegisterRepositories : Autofac.Module
{
    /// <summary>
    /// The Load.
    /// </summary>
    /// <param name="builder">The builder<see cref="ContainerBuilder"/>.</param>
    protected override void Load(ContainerBuilder builder)
    {
        System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerDependency();

        base.Load(builder);
    }
}