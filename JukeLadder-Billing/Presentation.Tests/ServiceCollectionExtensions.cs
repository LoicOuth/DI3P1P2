using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Stripe;

namespace Presentation.Tests;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Remove<TService>(this IServiceCollection services)
    {
        var serviceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(TService));

        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }

    public static IServiceCollection PrepareTest(this IServiceCollection services)
    {
        var paymentIntendHelper = new Mock<IPaymentIntentHelper>();
        var productHelper = new Mock<IProductHelper>();

        paymentIntendHelper.Setup(x => x.CreateAsync(1000, "eur", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Stripe.PaymentIntent { Id = "KnowedId", ClientSecret = "ClientSecret" });
        
        productHelper.Setup(x => x.GetProductAsync("KnowedId", "default_price"))
            .ReturnsAsync(new Product { Id = "KnowedId", Description = "", DefaultPrice = new Price { Id = "KnowedId", UnitAmount = 1000, Currency = "eur" } });

        productHelper.Setup(x => x.GetProductAsync("prod_MSwTAmtUmsxQ9A", "default_price"))
            .ReturnsAsync(new Product { Id = "prod_MSwTAmtUmsxQ9A", Description = "", DefaultPrice = new Price { Id = "KnowedId", UnitAmount = 1000, Currency = "eur" } });

        productHelper.Setup(x => x.GetProductAsync("prod_MSwUWCD74YEaGd", "default_price"))
            .ReturnsAsync(new Product { Id = "prod_MSwUWCD74YEaGd", Description = "", DefaultPrice = new Price { Id = "KnowedId", UnitAmount = 1000, Currency = "eur" } });

        productHelper.Setup(x => x.UpdateProductPrice("KnowedId", It.IsAny<long>(), It.IsAny<string>()))
            .ReturnsAsync(new Product { Id = "KnowedId", DefaultPrice = new Price { Id = "NewKnowedId", UnitAmount = 1000, Currency = "eur" } });

        services.Remove<IPaymentIntentHelper>();
        services.Remove<IProductHelper>();

        services.AddSingleton(paymentIntendHelper.Object);
        services.AddSingleton(productHelper.Object);

        return services;
    }
}
