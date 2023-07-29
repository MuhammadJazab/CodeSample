//-----------------------------------------------------------------------
// <copyright file="ConfigureSwaggerExtensions.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Authentication.Api.Extensions;

/// <summary>
/// Defines the <see cref="ConfigureSwaggerExtensions" />.
/// </summary>
public static class ConfigureSwaggerExtensions
{
    /// <summary>
    /// The AddSwaggerDoc.
    /// </summary>
    /// <param name="builder">The builder<see cref="WebApplicationBuilder"/>.</param>
    public static void AddSwaggerDoc(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Authentication API",

            });
        });

        builder.Services.AddSwaggerGen(gen =>
        {
            gen.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });
    }
}