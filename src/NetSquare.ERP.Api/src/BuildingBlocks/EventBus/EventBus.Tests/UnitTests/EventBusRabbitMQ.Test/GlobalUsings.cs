//-----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="NetSquare Limited">
// Copyright (c) NetSquare Limited. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

global using System;
global using System.Net.Sockets;
global using System.Text.Json;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Moq;
global using Microsoft.Extensions.Logging;
global using RabbitMQ.Client;
global using Autofac;
global using NetSquare.ERP.EventBus;
global using EventBus.Test.Core.Constants;
global using EventBus.Test.Core.Fixtures;
global using EventBusRabbitMQ.Constants;
global using Polly;
global using Polly.Retry;
global using RabbitMQ.Client.Exceptions;
global using NetSquare.ERP.EventBus.Abstractions;
global using NetSquare.ERP.EventBusRabbitMQ;