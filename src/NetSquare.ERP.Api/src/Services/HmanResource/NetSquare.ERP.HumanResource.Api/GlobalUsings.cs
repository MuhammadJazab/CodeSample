//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

global using System.Reflection;
global using MediatR;
global using System.Net;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using NetSquare.ERP.HumanResource.Api.Application.Constants;
global using Microsoft.Extensions.Options;
global using System.Security.Claims;
global using System.Text;
global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using NetSquare.ERP.HumanResource.Api.Application.Extensions;
global using NetSquare.ERP.HumanResource.Api.Extensions;
global using Microsoft.OpenApi.Models;
global using NetSquare.ERP.HumanResource.Domain.SeedWork;
global using NetSquare.ERP.HumanResource.Infrastructure.Repositories;
global using NetSquare.ERP.HumanResource.Infrastructure.Services;
global using Newtonsoft.Json;
global using System.Reflection.PortableExecutable;
global using FluentValidation.AspNetCore;
global using NetSquare.ERP.HumanResource.Infrastructure.Extensions;
global using NetSquare.ERP.HumanResource.Infrastructure.Data;
global using NetSquare.ERP.ExceptionHandler.Middleware;
global using NetSquare.ERP.HumanResource.Domain.Common;