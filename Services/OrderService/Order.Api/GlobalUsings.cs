//file="GlobalUsings.cs" >

global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using Order.Api.Application.Constants;
global using Order.Api.Application.Features.Account.Requests.Queries;
global using Order.Api.Application.Models.Requests;
global using Order.Api.Application.Models.Response;
global using Order.Domain.Common;
global using Order.Domain.Enums;
global using Order.Infrastructure.Services;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using System.Diagnostics.CodeAnalysis;
global using Order.Api.Application.Features.Account.Requests.Command;
global using Order.Domain.Entities;
global using Order.Domain.SeedWork;
global using Order.Api.Extensions;
global using Order.Infrastructure.Extensions;
global using OpenTelemetry.Metrics;
global using OpenTelemetry.Trace;
global using Order.Infrastructure.Data;
global using Order.Infrastructure.Repositories;
global using Order.Api.IntegrationEvents.Events;