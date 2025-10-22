using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageWorkListAggregatorManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CoreWCF services
            builder.Services.AddServiceModelServices();
            builder.Services.AddServiceModelMetadata();

            var app = builder.Build();

            app.UseServiceModel(builder =>
            {
                // Configure service endpoints
                builder.AddService<WorklistAggregationManagerService>();
                builder.AddServiceEndpoint<WorklistAggregationManagerService, IWorklistAggregationManagerService>(
                    new BasicHttpBinding(),
                    "/ImageWorkListAggregator/WorklistAggregationManagerService.svc");

                // Enable metadata
                var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
                serviceMetadataBehavior.HttpGetEnabled = true;
            });

            app.Run();
        }
    }
}