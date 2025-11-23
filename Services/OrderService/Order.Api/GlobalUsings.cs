//file="GlobalUsings.cs" >

global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi;
global using OpenTelemetry.Metrics;
global using OpenTelemetry.Trace;
global using Order.Api.Application.Constants;
global using Order.Api.Application.Features.Account.Requests.Command;
global using Order.Api.Application.Features.Account.Requests.Queries;
global using Order.Api.Application.Models.Requests;
global using Order.Api.Application.Models.Response;
global using Order.Api.Extensions;
global using Order.Api.IntegrationEvents.Events;
global using Order.Domain.Common;
global using Order.Domain.Entities;
global using Order.Domain.Enums;
global using Order.Domain.SeedWork;
global using Order.Infrastructure.Data;
global using Order.Infrastructure.Extensions;
global using Order.Infrastructure.Repositories;
global using Order.Infrastructure.Services;
global using RabbitMQ.Client;
global using System.Diagnostics.CodeAnalysis;
global using System.Text;
global using System.Text.Json;
