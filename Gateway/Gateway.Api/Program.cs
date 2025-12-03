var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<YarpConfiguration>(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.AddOpenTelemetryExtension();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gateway API",
        Version = "v1"
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/notification/swagger.json", "Notification Service");
        c.SwaggerEndpoint("/swagger/order/swagger.json", "Order Service");
        c.SwaggerEndpoint("/swagger/user/swagger.json", "User Service");
    });
}

app.MapReverseProxy();

app.UseAuthorization();

app.MapControllers();

app.MapPrometheusScrapingEndpoint();

app.Run();
