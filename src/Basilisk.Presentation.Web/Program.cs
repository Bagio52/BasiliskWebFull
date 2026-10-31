using Basilisk.Business;
using Basilisk.Business.Interface;
using Basilisk.Business.Repository;
using Basilisk.Presentation.Web.Configurations;
using Basilisk.Presentation.Web.Services;

namespace Basilisk.Presentation.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Menambahkan koneksi database sebagai service
        Dependencies.AddDataAccessServices(builder.Services, builder.Configuration);

        // Menambahkan Service dari Business layer
        builder.Services.AddBusinessServices();

        // Menambahkan srvice yang ada dalam aplikasi ini
        builder.Services.AddScoped<CategoryService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<SupplierService>();
        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<SalesmanService>();
        builder.Services.AddScoped<ISalesmanRepository, SalesmanRepository>();
        builder.Services.AddScoped<RegionService>();
        builder.Services.AddScoped<IRegionRepository, RegionRepository>();
        builder.Services.AddScoped<CustomerService>();
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<DeliveryService>();
        builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        builder.Services.AddScoped<OrderService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        builder.Services.AddControllersWithViews();

        // #TODO : Tambahin config buat Controller dan routing. and the done
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapControllerRoute(
            name: "Default",
            // pattern: "{controller=Home}/{action=Index}/{id}"
            pattern: "{controller=Dashboard}/{action=Index}"
        );

        app.Run();

    }
}