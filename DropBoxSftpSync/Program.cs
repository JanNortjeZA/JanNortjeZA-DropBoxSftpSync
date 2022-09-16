using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DanceRadioSync.BLL;
using DanceRadioSync.DAL;
using DanceRadioSync.Models;
using Microsoft.AspNetCore.Hosting;

namespace DanceRadioSync
{
    public class Program
    {
         static async Task Main(string[] args)
        {
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //var config = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            // .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //// .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            //.AddEnvironmentVariables()
            //.Build();
            Global.dbConnectionString = "tubelessMySql";
            Global.isProduction = true;
            if (args.Length > 0)
            {
                foreach (string arg in args)
                {

                    //if (arg == "-debug")
                    //{
                    //    Global.dbConnectionString = "tubelessMySql";
                    //    Global.isProduction = false;
                    //    Console.ForegroundColor = ConsoleColor.Green;
                    //    Console.WriteLine("debug mode - using core_wh");
                    //    Console.WriteLine("DB SAVE disabled!");
                    //    Console.WriteLine("SMS SEND disabled!");
                    //    Console.ResetColor();
                    //}
                    if (arg.Contains("--port"))
                    {
                       var portStr = arg.Split(" ").ToArray();
                        Global.Port = Int16.Parse(portStr[1]);
                    }

                }
            }

            var builder = new HostBuilder();
            // builder.RunConsoleAsync
            if (Global.isProduction)
                // builder.UseEnvironment("Development");

                builder.ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices()
                     .AddTimers()
                     .AddAzureStorageBlobs()
                     .AddAzureStorageQueues()
                     .AddServiceBus()
                     .AddEventHubs();
                    

                })
                .ConfigureAppConfiguration(b =>
                {
                    b.AddCommandLine(args);
                })
                .ConfigureLogging((context, b) =>
                {
                    b.SetMinimumLevel(LogLevel.Debug);
                    b.AddSimpleConsole();
                   // b.AddConsoleFormatter()
                   b.AddAzureWebAppDiagnostics();


                    //  If this key exists in any config, use it to enable App Insights
                    string appInsightsKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                    if (!string.IsNullOrEmpty(appInsightsKey))
                    {
                        
                        //  b.AddApplicationInsightsWebJobs( o => ( o.InstrumentationKey = appInsightsKey);
                    }
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                    services.AddSingleton<ISyncService, SyncService>();
                    services.AddSingleton<IDoSync, DoSync>();
                    services.AddSingleton<IWebServiceWorker, WebServiceWorker>();

                })
                
                .UseConsoleLifetime();




            var host = builder.Build();
            // await   builder.Build().StartAsync();

            using (host)
            {
              
                await host.RunAsync();
            }

            
        }
    }
}

