using Microsoft.EntityFrameworkCore;
using PharmaceuticalManagement_BO.Models;
using PharmaceuticalManagement_DAO;
using PharmaceuticalManagement_Repo;

namespace PharmaceuticalManagement_NguyenVietNamKhanh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IStoreAccountRepo, StoreAccountRepo>();
            builder.Services.AddScoped<IMedicineInformationRepo, MedicineInformationRepo>();
            builder.Services.AddScoped<IManufacturerRepo, ManufacturerRepo>();
            builder.Services.AddScoped(typeof(IBaseDAO<>), typeof(BaseDAO<>));
            builder.Services.AddSession();
            builder.Services.AddDbContext<Fall24PharmaceuticalDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
