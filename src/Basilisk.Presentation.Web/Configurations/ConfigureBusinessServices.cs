using System;
using Basilisk.Business.Interface;
using Basilisk.Business.Repository;

namespace Basilisk.Presentation.Web.Configurations;

public static class ConfigureBusinessServices
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISalesmanRepository, SalesmanRepository>();
        return services;
    }
}
