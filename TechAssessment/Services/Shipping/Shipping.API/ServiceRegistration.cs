using AutoMapper;
using Moq;
using Shipping.Application.Services;
using Shipping.Infrastructure.Adapters;
using Shipping.Infrastructure.Clients;
using Shipping.Infrastructure.DTO;
using Shipping.Infrastructure.Mapping;

namespace Shipping.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var random = new Random();
            var configurarion = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = configurarion.CreateMapper();

            services.AddTransient<Api1ProviderAdapter>(c => {
                var clientRestMock = new Mock<IClientRest>();
                var url = configuration.GetValue<Uri>("APIs:API1:Url");
                var credentials = configuration.GetValue<string>("APIs:API1:Credentials");
                RateApi1DTO rateApi1DTO = new RateApi1DTO()
                {
                    Total = random.Next(10000,1000000)
                };
                clientRestMock.Setup(c => c.CallRestEndPointAsync(url, credentials, It.IsAny<PackageApi1DTO>())).ReturnsAsync(rateApi1DTO);
                return new Api1ProviderAdapter(url, credentials, clientRestMock.Object, mapper); 
            });
            services.AddTransient<Api2ProviderAdapter>(c => {
                var clientRestMock = new Mock<IClientRest>();
                var url = configuration.GetValue<Uri>("APIs:API2:Url");
                var credentials = configuration.GetValue<string>("APIs:API2:Credentials");
                RateApi2DTO rateApi2DTO = new RateApi2DTO()
                {
                    Amount = random.Next(10000, 1000000)
                };
                clientRestMock.Setup(c => c.CallRestEndPointAsync(url, credentials, It.IsAny<PackageApi2DTO>())).ReturnsAsync(rateApi2DTO);
                return new Api2ProviderAdapter(url, credentials, clientRestMock.Object, mapper);
            });
            services.AddTransient<Api3ProviderAdapter>(c => {
                var clientRestMock = new Mock<IClientRest>();
                var url = configuration.GetValue<Uri>("APIs:API3:Url");
                var credentials = configuration.GetValue<string>("APIs:API3:Credentials");
                RateApi3DTO rateApi3DTO = new RateApi3DTO()
                {
                    Quote = random.Next(10000, 1000000)
                };
                clientRestMock.Setup(c => c.CallRestEndPointAsync(url, credentials, It.IsAny<PackageApi3DTO>())).ReturnsAsync(rateApi3DTO);
                return new Api3ProviderAdapter(url, credentials, clientRestMock.Object, mapper);
            });

            services.AddTransient<IShippingPackageService, ShippingPackageService>();

            services.AddTransient<IShippingProviderService, ShippingProviderService>(c =>

                new ShippingProviderService(c.GetRequiredService<Api1ProviderAdapter>())
            )
                .AddTransient<IShippingProviderService, ShippingProviderService>(c =>

                new ShippingProviderService(c.GetRequiredService<Api2ProviderAdapter>())
            )
                .AddTransient<IShippingProviderService, ShippingProviderService>(c =>

                new ShippingProviderService(c.GetRequiredService<Api3ProviderAdapter>())
            );

            return services;
        }
    }
}
