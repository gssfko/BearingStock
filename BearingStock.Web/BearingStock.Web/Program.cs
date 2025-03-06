using BearingStock.Web.Components;
using BearingStock.Web.Services;

namespace BearingStock.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents()
				.AddInteractiveWebAssemblyComponents();

			builder.Services.AddHttpClient<BearingService>(client =>
			{
				client.BaseAddress = new Uri("http://localhost:5255/"); // API URL
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseAntiforgery();

			app.MapRazorComponents<App>()
				.AddInteractiveServerRenderMode()
				.AddInteractiveWebAssemblyRenderMode()
				.AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

			app.Run();
		}
	}
}
