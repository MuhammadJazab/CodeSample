//file="Program.cs" >

var corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add OpenTelemetry
builder.AddOpenTelemetryExtension();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<OrderCreationIntegratoinEventHandler>();

builder.Services.AddCors(option => option.AddPolicy(name: corsPolicy, builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapPrometheusScrapingEndpoint();

app.MapControllers();

app.Run();