//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare">
// Copyright (c) NetSquare. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

global using System;
global using System.Net.Sockets;
global using System.Text;
global using System.Text.Json;
global using NetSquare.ERP.EventBus.Abstractions;
global using RabbitMQ.Client;
global using Autofac;
global using NetSquare.ERP.EventBus;
global using NetSquare.ERP.EventBus.Events;
global using NetSquare.ERP.EventBus.Extensions;
global using Microsoft.Extensions.Logging;
global using Polly;
global using Polly.Retry;
global using RabbitMQ.Client.Events;
global using RabbitMQ.Client.Exceptions;
global using System.Diagnostics.CodeAnalysis;
global using EventBusRabbitMQ;
global using EventBusRabbitMQ.Constants;