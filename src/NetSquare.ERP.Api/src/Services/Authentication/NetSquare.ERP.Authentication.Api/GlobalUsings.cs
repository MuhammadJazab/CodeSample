//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

global using System.Reflection;
global using MediatR;
global using NetSquare.ERP.Authentication.Domain.Common;
global using System.Net;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using NetSquare.ERP.Authentication.Api.Protos;
global using NetSquare.ERP.Authentication.Api.Application.Constants;
global using NetSquare.ERP.Authentication.Api.Application.Features.Account.Requests.Queries;
global using NetSquare.ERP.Authentication.Api.Application.Models;
global using NetSquare.ERP.Authentication.Domain.Identity;
global using NetSquare.ERP.Authentication.Domain.SeedWork;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.Extensions.Options;
global using NetSquare.ERP.Authentication.Domain.Enums;
global using NetSquare.ERP.Authentication.Domain.Configurations;
global using System.Security.Claims;
global using System.Text;
global using System.Data;
global using Microsoft.IdentityModel.Tokens;
global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using NetSquare.ERP.Authentication.Infrastructure.Data;
global using NetSquare.ERP.Authentication.Infrastructure.Repositories;
global using System.Text.Json;
global using NetSquare.ERP.Authentication.Infrastructure.Services;
global using NetSquare.ERP.Authentication.Api.Application.Extensions;
global using NetSquare.ERP.Authentication.Api.Extensions;
global using NetSquare.ERP.ExceptionHandler.Middleware;
global using Microsoft.OpenApi.Models;
global using System.Diagnostics.CodeAnalysis;
global using NetSquare.ERP.Authentication.Infrastructure.Extensions;
global using System;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using NetSquare.ERP.Authentication.Api.Application.Validators;
global using NetSquare.ERP.Authentication.Api.Application.Features.Account.Requests.Commands;
global using Microsoft.AspNetCore.Authorization;
global using Grpc.Core;
gloal using Microsoft.Extensions.Configuration;