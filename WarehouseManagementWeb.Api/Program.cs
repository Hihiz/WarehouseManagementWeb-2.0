using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WarehouseManagementWeb.Api.Middlewares;
using WarehouseManagementWeb.Application;
using WarehouseManagementWeb.Infrastructure;
using WarehouseManagementWeb.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

// Добавляем роботу токена в Swagger.
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WarehouseManagementWeb",
        Version = "v1"
    });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Введите JWT токен",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                          new string[] { }
                    }
                });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;

    ApplicationDbContext db = serviceProvider.GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync();

    await SeedDataIdentityRole.SeedRoleAsync(serviceProvider);
}

app.UseHttpsRedirection();

app.UseCors("ApiCorsPolicy")
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();

app.Run();
