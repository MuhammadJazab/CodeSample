
//file="InfrastructureServicesRegistration.cs" >

namespace User.Infrastructure.Extensions;

/// <summary>
/// Defines the <see cref="InfrastructureServicesRegistration" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class InfrastructureServicesRegistration
{
    /// <summary>
    /// The ConfigureInfrastructureServices.
    /// </summary> 
    /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddUserStore<UserStore<ApplicationUser>>()
                 .AddRoleStore<RoleStore<IdentityRole>>()
                 .AddRoleManager<RoleManager<IdentityRole>>()
                     .AddDefaultTokenProviders();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
                };
            });

        return services;
    }
}

