using Genzai.WebCore.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using TestContainers.Container.Abstractions;
using TestContainers.Container.Abstractions.Hosting;
using TestContainers.Container.Database.Hosting;
using TestContainers.Container.Database.MySql;

namespace Genzai.WebCore.Integration.Test.Common
{
    public sealed class WebCoreIntegrationTestStartup
    {

        private static readonly WebCoreIntegrationTestStartup _instance = new WebCoreIntegrationTestStartup();

        private readonly MyWebApplication _app;

        private WebCoreIntegrationTestStartup()
        {
            //Mysql
            MySqlContainer container = new ContainerBuilder<MySqlContainer>()
                            .ConfigureDatabaseConfiguration("gedge", "gedge", "gedge")
                            .ConfigureContainer((h, c) =>
                            {
                                c.Command.Add("--lower_case_table_names=1");
                                c.PortBindings.Add(MySqlContainer.DefaultPort, 3049);
                            })
                            .ConfigureLogging(builder =>
                            {
                                builder.AddConsole();
                                builder.SetMinimumLevel(LogLevel.Debug);
                            })
                            .Build();
            Task database = container.StartAsync();



            //Redis
            GenericContainer redis = new ContainerBuilder<GenericContainer>()
                 .ConfigureHostConfiguration(builder => builder.AddInMemoryCollection())
                 .ConfigureAppConfiguration((context, builder) => builder.AddInMemoryCollection())
                  .ConfigureDockerImageName("redis:latest")
                 .ConfigureLogging(builder =>
                 {
                     builder.AddConsole();
                     builder.SetMinimumLevel(LogLevel.Debug);
                 })
                 .ConfigureContainer((context, container) =>
                 {

                     container.ExposedPorts.Add(6360);
                     container.PortBindings.Add(6379, 6360);
                 })
                 .Build();
            Task redisTask = redis.StartAsync();


            //azurite
            GenericContainer azurite = new ContainerBuilder<GenericContainer>()
                 .ConfigureHostConfiguration(builder => builder.AddInMemoryCollection())
                 .ConfigureAppConfiguration((context, builder) => builder.AddInMemoryCollection())
                 .ConfigureDockerImageName("mcr.microsoft.com/azure-storage/azurite:latest")

                 .ConfigureLogging(builder =>
                 {
                     builder.AddConsole();
                     builder.SetMinimumLevel(LogLevel.Debug);
                 })
                 .ConfigureContainer((context, container) =>
                 {
                     container.ExposedPorts.Add(10003);
                     container.PortBindings.Add(10000, 10003);
                 })
                 .Build();
            Task azuriteTask = azurite.StartAsync();


            azuriteTask.Wait();
            redisTask.Wait();
            database.Wait();

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            _app = new MyWebApplication();


            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => this.OnProcessExit();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                this.OnProcessExit();
                eventArgs.Cancel = true;
            };
        }

        public HttpClient getClient()
        {
            HttpClient client = this._app.CreateClient();
            return client;
        }

        public static WebCoreIntegrationTestStartup Instance => _instance;


        private void OnProcessExit()
        {
            if (this._app != null)
            {
                this._app.Dispose();
            }
        }
    }


    class MyWebApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTesting");
            builder.UseSerilog();
            builder.ConfigureServices(
                services =>
            {

                services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
                services.AddApplicationInsightsTelemetry();
                //services.AddMvc().AddApplicationPart(typeof(SampleQueryController).Assembly).AddControllersAsServices();
            }
         );

            // shared extra set up goes here
            return base.CreateHost(builder);
        }
    }
}
