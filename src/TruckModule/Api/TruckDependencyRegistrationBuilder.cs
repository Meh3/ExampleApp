using ErpApp.TruckModule.Domain;
using ErpApp.TruckModule.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpApp.TruckModule.Api;

public static class TruckDependencyRegistrationBuilder
{
    public static IServiceCollection RegisterTruckModuleDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDomain()
                .RegisterInfrastructure(configuration);

        services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Filter().Select().Expand());

        return services;
    }
                   
    private static IServiceCollection RegisterDomain(this IServiceCollection services) =>
        services.AddTransient<TruckCreationService>()
                .AddTransient<TruckEditionService>();
    private static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ErpAppDb");
        if (connectionString?.Contains("ProvideConnectionString") ?? true)
        {
            throw new InvalidDataException("Connection string not provided. Fill appsettings.json file.");
        }
        services.AddDbContext<TruckDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ErpAppDb")));

        return services.AddScoped<ITruckRepository, TruckDbContext>();
    }

    private static IEdmModel GetEdmModel() => 
        new ODataConventionModelBuilder()
            .BuildTruckModel()
            .GetEdmModel();
}
