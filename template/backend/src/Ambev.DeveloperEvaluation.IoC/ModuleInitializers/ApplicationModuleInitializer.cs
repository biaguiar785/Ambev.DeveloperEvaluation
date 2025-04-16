using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

        builder.Services.AddMassTransit(x =>
        {
            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
                    config.ReceiveEndpoint(nameof(SaleCreatedEvent), endpoint => { endpoint.Bind(nameof(CreateSaleCommand)); });
                    config.ReceiveEndpoint(nameof(SaleDeletedEvent), endpoint => { endpoint.Bind(nameof(DeleteSaleCommand)); });
                    config.ReceiveEndpoint(nameof(SaleCancelledEvent), endpoint => { endpoint.Bind(nameof(CancelSaleCommand)); });
                });
            });
        });
    }
}