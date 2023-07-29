//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

var corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHumanResourceApiDbContext(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.ConfigureAutoFacContainer();
builder.AddSwaggerDoc();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
}

app.SeedHumanResourceApiData();
app.UseExceptionFilterMiddleware();

app.UseCors(corsPolicy);

app.UseHttpsRedirection();
app.ConfigureCustomExceptionMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();