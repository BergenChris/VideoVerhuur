using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using VideoVerhuur.Data;
using VideoVerhuur.Models;
using VideoVerhuur.Repos;
using VideoVerhuur.Services;
using VideoVerhuur.Data;

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
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("AutherizationConnection")));
			builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
				options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ApplicationDBContext>();

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
            app.UseAuthentication();
            app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
