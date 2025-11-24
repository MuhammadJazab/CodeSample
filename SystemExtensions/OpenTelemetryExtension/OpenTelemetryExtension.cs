namespace OpenTelemetryExtension;

/// <summary>
/// OpenTelemetry configuration extensions for microservices observability
/// </summary>
public static class OpenTelemetryExtensions
{
    private const string ServiceNamespace = "CodeSample.Microservices";

    /// <summary>
    /// Adds comprehensive OpenTelemetry configuration with tracing, metrics, and logging
    /// </summary>
    /// <param name="builder">The web application builder</param>
    /// <returns>The builder for method chaining</returns>
    public static WebApplicationBuilder AddOpenTelemetryExtension(this WebApplicationBuilder builder)
    {
        var serviceName = GetServiceName(builder);
        var serviceVersion = GetServiceVersion();

        // Configure OpenTelemetry resource
        var resourceBuilder = ResourceBuilder.CreateDefault()
            .AddService(serviceName: serviceName, serviceVersion: serviceVersion, serviceNamespace: ServiceNamespace)
            .AddTelemetrySdk()
            .AddEnvironmentVariableDetector();

        // Configure OpenTelemetry
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.Clear().AddService(serviceName, serviceVersion, ServiceNamespace)
                .AddTelemetrySdk()
                .AddEnvironmentVariableDetector())
            .WithTracing(tracing => ConfigureTracing(tracing, builder))
            .WithMetrics(metrics => ConfigureMetrics(metrics, builder));

        // Configure logging separately
        builder.Logging.AddOpenTelemetry(logging => ConfigureLogging(logging, builder));

        return builder;
    }

    private static void ConfigureTracing(TracerProviderBuilder tracing, WebApplicationBuilder builder)
    {
        tracing
            .AddAspNetCoreInstrumentation(options =>
            {
                options.RecordException = true;
                options.Filter = httpContext =>
                {
                    var path = httpContext.Request.Path.Value?.ToLowerInvariant();
                    return !IsHealthCheckOrMetricsEndpoint(path);
                };
                options.EnrichWithHttpRequest = (activity, httpRequest) =>
                {
                    activity.SetTag("http.request.method", httpRequest.Method);
                    activity.SetTag("http.request.scheme", httpRequest.Scheme);
                    activity.SetTag("http.request.host", httpRequest.Host.Value);
                };
                options.EnrichWithHttpResponse = (activity, httpResponse) =>
                {
                    activity.SetTag("http.response.status_code", httpResponse.StatusCode);
                };
            })
            .AddHttpClientInstrumentation(options =>
            {
                options.RecordException = true;
                options.FilterHttpRequestMessage = request =>
                {
                    var uri = request.RequestUri?.ToString().ToLowerInvariant();
                    return !IsHealthCheckOrMetricsEndpoint(uri);
                };
            })
            .AddEntityFrameworkCoreInstrumentation()
            .AddSource("MassTransit")
            .AddSource("System.Net.Http");

        // Add OTLP exporter for Tempo (distributed tracing)
        var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"] ?? "http://localhost:4317";
        tracing.AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(otlpEndpoint);
            options.Protocol = OtlpExportProtocol.Grpc;
        });

        // Add console exporter for development
        if (builder.Environment.IsDevelopment())
        {
            tracing.AddConsoleExporter();
        }
    }

    private static void ConfigureMetrics(MeterProviderBuilder metrics, WebApplicationBuilder builder)
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddMeter("System.Net.Http")
            .AddMeter("MassTransit");

        // Add OTLP exporter for metrics
        var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"] ?? "http://localhost:4317";
        metrics.AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(otlpEndpoint);
            options.Protocol = OtlpExportProtocol.Grpc;
        });

        // Add Prometheus exporter
        metrics.AddPrometheusExporter();

        // Add console exporter for development
        if (builder.Environment.IsDevelopment())
        {
            metrics.AddConsoleExporter();
        }
    }

    private static void ConfigureLogging(OpenTelemetryLoggerOptions logging, WebApplicationBuilder builder)
    {
        logging
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
                serviceName: GetServiceName(builder),
                serviceVersion: GetServiceVersion(),
                serviceNamespace: ServiceNamespace))
            .AddConsoleExporter();

        // Add OTLP exporter for Loki (log aggregation)
        var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"] ?? "http://localhost:4317";
        logging.AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(otlpEndpoint);
            options.Protocol = OtlpExportProtocol.Grpc;
        });
    }

    private static string GetServiceName(WebApplicationBuilder builder)
    {
        return builder.Configuration["OpenTelemetry:ServiceName"]
               ?? builder.Environment.ApplicationName
               ?? Assembly.GetEntryAssembly()?.GetName().Name
               ?? "Unknown";
    }

    private static string GetServiceVersion()
    {
        return Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
               ?? Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
               ?? "1.0.0";
    }

    private static bool IsHealthCheckOrMetricsEndpoint(string? path)
    {
        if (string.IsNullOrEmpty(path)) return false;

        return path.Contains("/health") ||
               path.Contains("/metrics") ||
               path.Contains("/ping") ||
               path.Contains("/ready") ||
               path.Contains("/live");
    }
}