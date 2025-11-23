
//file="Program.cs" >

var corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAutoFacContainer();
builder.AddSwaggerDoc();

// Add services to the container.
builder.Services.ConfigureApplicationServices();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureInfrastructureServices(builder.Configuration);

// Add OpenTelemetry
builder.AddOpenTelemetryExtension();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add CORS
builder.Services.AddCors(option => option.AddPolicy(name: corsPolicy, builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedingOrderApiData();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapPrometheusScrapingEndpoint();

app.MapControllers();

app.Run();

public static partial class Program { }