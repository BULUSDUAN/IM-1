// Copyright(c) Cragon. All rights reserved.

namespace Cragon
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Runtime;
    using Orleans.Runtime.Configuration;
    using NLog.Extensions.Logging;

    public class Program
    {
        //---------------------------------------------------------------------
        static readonly ManualResetEvent _siloStopped = new ManualResetEvent(false);
        static ISiloHost silo;
        static bool siloStopping = false;
        static readonly object syncLock = new object();

        //---------------------------------------------------------------------
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        //---------------------------------------------------------------------
        private static async Task<int> RunMainAsync()
        {
            //var ucenter_context = new UCenterContext();
            //await ucenter_context.Setup();
            //var ucenter_cfg = ucenter_context.ConfigCfg;

            SetupApplicationShutdown();

            var builder = new SiloHostBuilder()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "a";
                    options.ServiceId = "b";
                })
                .Configure<EndpointOptions>(options =>
                {
                    options.AdvertisedIPAddress = IPAddress.Loopback;
                    options.GatewayPort = 30001;
                    options.SiloPort = 11112;
                })
                .Configure<StatisticsOptions>(options =>
                {
                    options.CollectionLevel = StatisticsLevel.Critical;
                })
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(GrainAccount).Assembly).WithReferences();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                })
                .UseLocalhostClustering()
                .UseSiloUnobservedExceptionsHandler()
                .UseInMemoryReminderService()
                .AddMemoryGrainStorageAsDefault()
                .AddMemoryGrainStorage("PubSubStore")
                .AddSimpleMessageStreamProvider("SMSProvider")
                .EnableDirectClient()
                .AddStartupTask<Startup>();

            silo = builder.Build();

            await silo.StartAsync();

            _siloStopped.WaitOne();

            await silo.StopAsync();

            return 0;
        }

        //---------------------------------------------------------------------
        static void SetupApplicationShutdown()
        {
            // Capture the user pressing Ctrl+C
            Console.CancelKeyPress += (s, a) =>
            {
                // Prevent the application from crashing ungracefully.
                a.Cancel = true;

                // Don't allow the following code to repeat if the user presses Ctrl+C repeatedly.
                lock (syncLock)
                {
                    if (!siloStopping)
                    {
                        siloStopping = true;
                        Task.Run(StopSilo).Ignore();
                    }
                }

                // Event handler execution exits immediately, leaving the silo shutdown running on a background thread,
                // but the app doesn't crash because a.Cancel has been set = true
            };
        }

        //---------------------------------------------------------------------
        static async Task StopSilo()
        {
            await silo.StopAsync();
            _siloStopped.Set();
        }
    }
}