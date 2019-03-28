using DesignPatterns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DesignPatterns.UserContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using DesignPatterns.ProcessingData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPIGameStatistics
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddScoped<IRepositoryTable<UserData>>(a => new RepositoryTable<UserData>(StringHelper.NameFiles.SavedUserData));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddDistributedMemoryCache();
			services.AddSession();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseDefaultFiles();
			app.UseSession();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseMvc();

			//app.UseMvc(routes =>
			//{
			//	routes.MapRoute(
			//		name: "default",
			//		template: "{controller=Authorization}/{action=AuthorizeUser}");
			//});
		}
	}
}
