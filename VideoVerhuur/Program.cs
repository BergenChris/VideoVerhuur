using Microsoft.EntityFrameworkCore;
using VideoVerhuur.Data;
using VideoVerhuur.Models;
using VideoVerhuur.Repos;
using VideoVerhuur.Services;

namespace VideoVerhuur
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<SampleDBContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
     

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<SQLVideoVerhuurRepo>();
            builder.Services.AddScoped<Repos.IVideoVerhuurRepo, SQLVideoVerhuurRepo>();
			builder.Services.AddSession();
            builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
