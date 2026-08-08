using Scalar.AspNetCore;
using scms.Application;
using scms.Infrastructure;
using scms.Infrastructure.Extensions;
using SCMS.API.Extensions;
using ValidationDI = scms.Application.DependencyInjection;

using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<scms.Application.Mapper.TenantMappingProfile>());
builder.Services.AddOpenApi();

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.AddCustomLogging();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomCors(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("CustomCorsPolicy");

app.UseGlobalExceptionHandler();

app.UseTenantResolver();         // Must run before UseAuthentication

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
