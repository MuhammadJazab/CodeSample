//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

var corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtConfigurations>(builder.Configuration.GetSection(nameof(JwtConfigurations)));
builder.Services.Configure<YarpConfig>(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddOpenTelemetry()
    .WithTracing(builder => builder.AddAspNetCoreInstrumentation().AddConsoleExporter())
    .WithMetrics(builder => builder.AddAspNetCoreInstrumentation().AddConsoleExporter());

// Add CORS
builder.Services.AddCors(option => option.AddPolicy(name: corsPolicy, builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.WithExposedHeaders(ReverseProxyConstants.AuthorizationKey);
}));

var app = builder.Build();

app.UseCors(corsPolicy);

app.UseCustomMiddlewareConfiguration();

[ExcludeFromCodeCoverage]
public static partial class Program { }