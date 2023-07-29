//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

var corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationDbContext(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.ConfigureAutoFacContainer();
builder.ConfigureFluentValidatorFilter();

builder.Services.AddSwaggerGen();
builder.AddSwaggerDoc();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add CORS
builder.Services.AddCors(option => option.AddPolicy(name: corsPolicy, builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors(corsPolicy);

app.SeedAuthenticationApiData();

app.UseExceptionFilterMiddleware();
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
