using System.ComponentModel;
using Basilisk.Business;
using Basilisk.Business.Interface;
using Basilisk.Business.Repository;
using Basilisk.Presentation.Api.Categories;
using Basilisk.Presentation.Api.Salesman;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace Basilisk.Presentation.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole();


        Dependencies.AddDataAccessServices(builder.Services, builder.Configuration);

        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ISalesmanRepository, SalesmanRepository>();

        builder.Services.AddScoped<CategoryService>();
        builder.Services.AddScoped<SalesmanService>();

        builder.Services.AddControllers();

        // Menambahkan swagger / OpenAPI UI (dengan menambahkan bbeberapa package Swashbuckle)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Basilisk API",
                Description = "An ASP.NET Core Web APi for Basilisk export and import business",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
        });

        var app = builder.Build();

        // Mengaktifkan dokumentasi Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}
