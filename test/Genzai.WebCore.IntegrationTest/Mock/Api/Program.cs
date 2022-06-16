using Genzai.WebCore.Integration.Test.Mock.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = "IntegrationTesting",
    WebRootPath = "customwwwroot"

});

builder.Host.UseSerilog((context, services, configuration)
    => configuration
        .ReadFrom.Configuration(builder.Configuration)
    );


builder.Configuration.AddJsonFile(Directory.GetCurrentDirectory() + "/Mock/Api/appsettings.IntegrationTesting.json");
IConfiguration configuration = builder.Configuration;
IServiceCollection services = builder.Services;



//Configure service
SampleConfigurationManager.ConfigureServices(services, configuration);

//Audit

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddScoped(provider => new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
{
    new("emails", "mock@email.fake"),
    new(ClaimTypes.Name, "test@prosegur.com")
}, "mock")));

var app = builder.Build();
IWebHostEnvironment env = app.Environment;


//APP configuration


app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.yaml", "GwatchPortalAPI v1"); });

app.UseCors("CorsPolicy");
app.UseRouting();
app.UseSerilogRequestLogging();

//FIXME
//app.UseAuthentication();
//app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});


//Init
SampleInitializer.Init(app, env, configuration);


await app.RunAsync();