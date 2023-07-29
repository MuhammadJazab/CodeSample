//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<BranchService>();

app.Run();

[ExcludeFromCodeCoverage]
public static partial class Program { }