using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ImageWorkListAggregatorManager.Contracts;

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

        // Cast to IApplicationBuilder to resolve ambiguity
       ((IApplicationBuilder)app).UseServiceModel(serviceBuilder =>
            {
                // Configure service endpoints
serviceBuilder.AddService<WorklistAggregationManagerService>();
      serviceBuilder.AddServiceEndpoint<WorklistAggregationManagerService, IWorklistAggregationManager>(
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