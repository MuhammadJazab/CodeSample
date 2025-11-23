namespace Gateway.Api.Middlewares;

public static class SwaggerReverseProxyMiddleware
{
    public static void AddSwaggerReverseProxyMiddleware(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            var path = context.Request.Path.Value;

            switch (path)
            {
                case "/swagger/notification/swagger.json":
                    await HandleSwaggerProxy(context, "https://localhost:7175/swagger/v1/swagger.json", "https://localhost:7175", "https://localhost:5000");
                    return;

                case "/swagger/order/swagger.json":
                    await HandleSwaggerProxy(context, "https://localhost:7176/swagger/v1/swagger.json", "https://localhost:7176", "https://localhost:5000");
                    return;

                case "/swagger/user/swagger.json":
                    await HandleSwaggerProxy(context, "https://localhost:7085/swagger/v1/swagger.json", "https://localhost:7085", "https://localhost:5000");
                    return;

                default:
                    await next();
                    break;
            }
        });
    }

    private static async Task HandleSwaggerProxy(HttpContext context, string sourceUrl, string originalUrl, string gatewayUrl)
    {
        using var http = new HttpClient();
        var json = await http.GetStringAsync(sourceUrl);

        // Rewrite server URL so requests go via gateway
        json = json.Replace($"\"url\":\"{originalUrl}\"", $"\"url\":\"{gatewayUrl}\"");

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(json);
    }
}
