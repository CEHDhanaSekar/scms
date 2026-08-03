namespace SCMS.API.Extensions;

public static class CorsServiceExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOrigins = configuration["CorsOrigins"]?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy("CustomCorsPolicy", builder =>
            {
                if (corsOrigins.Length > 0)
                {
                    builder.WithOrigins(corsOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // Optional: often needed for web clients with authentication
                }
                else
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            });
        });

        return services;
    }
}
