using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;          

namespace HorizonFutureVest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Obtener la cadena de conexión del archivo appsettings.json
            // Asegúrate de tener la sección "ConnectionStrings": { "DefaultConnection": "..." } en tu JSON.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // 2. Registrar el DbContext con SQL Server
            builder.Services.AddDbContext<Persistence.Contexts.AppContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
