using BidItSaitynai_naujas.Client;
using BidItSaitynai_naujas.Client.Pages;
using BidItSaitynai_naujas.Client.Shared.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BidItSaitynai_naujas.Client
{
   public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");


			builder.Services.AddScoped<HttpService>();
			builder.Services.AddSingleton<Utilities.ILocalStorage, Utilities.LocalStorage>();

			

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress), });
			builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
			builder.Services.AddAuthorizationCore();
			await builder.Build().RunAsync();
		}
	}
}