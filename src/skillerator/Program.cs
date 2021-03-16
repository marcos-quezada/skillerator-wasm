using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace skillerator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddScoped<Shared.SkilleratorAppService>();

            // Enable cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("https://proxy.skillerator.de")
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            /*app.UseCors(policyName => policyName.WithOrigins("https://localhost:5001")
                .AllowAnyMethod()
                .WithHeaders(HeaderNames.ContentType));

            var requestMessage = new HttpRequestMessage() {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri(SERVICE_ENDPOINT)
            };*/

            await builder.Build().RunAsync();
        }
    }
}
