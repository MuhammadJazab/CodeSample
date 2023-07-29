//-----------------------------------------------------------------------
// <copyright file="ConfigureFluentValidatorFilterExtention.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace NetSquare.ERP.Branch.Api.Extensions;

/// <summary>
/// Definition of ConfigureFluentValidatorFilterExtention
/// </summary>
/// <param name="appBuilder">The object of WebApplicationBuilder <see cref="WebApplicationBuilder"/></param>
public static class ConfigureFluentValidatorFilterExtention
{
    public static void ConfigureFluentValidatorFilter(this WebApplicationBuilder appBuilder)
    {
        appBuilder.Services.AddFluentValidationAutoValidation();
        appBuilder.Services.AddFluentValidationClientsideAdapters();
    }
}