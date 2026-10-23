using System;
using Basilisk.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basilisk.Business;

public class Dependencies
{
    public static void AddDataAccessServices(IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<BasiliskTFContext>(c =>
        c.UseSqlServer(configuration.GetConnectionString("BasiliskConnection")));
    }
}
