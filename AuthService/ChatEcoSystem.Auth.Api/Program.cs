using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ChatEcoSystem.Auth.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureKestrel((context, options) =>
                        {
                            options.Configure(context.Configuration.GetSection("Kestrel"));
                        });
                });
    }
}
