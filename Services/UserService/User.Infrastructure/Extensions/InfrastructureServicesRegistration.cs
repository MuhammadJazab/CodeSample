
//file="InfrastructureServicesRegistration.cs" >

namespace User.Infrastructure.Extensions;

/// <summary>
/// Defines the <see cref="InfrastructureServicesRegistration" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class InfrastructureServicesRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddIdentity<ApplicationUser, ApplicationRole>()
                 .AddUserStore<UserStore<ApplicationUser, ApplicationRole, UserDbContext, Guid>>()
                 .AddRoleStore<RoleStore<ApplicationRole, UserDbContext, Guid>>()
                 .AddRoleManager<RoleManager<ApplicationRole>>()
                 .AddEntityFrameworkStores<UserDbContext>()
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

