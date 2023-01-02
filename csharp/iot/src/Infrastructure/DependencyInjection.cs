using Application.Common.Interfaces;
using Application.Common.Interfaces.Config;
using Domain.Entities;
using Infrastructure.Clients;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IAppclicationDbContext,ApplicationDbContext>();
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<IRawRepository<RawData>, RawRepository>();
            services.AddTransient<IDetailRepository<Detail>, DetailRepository>();
            services.AddTransient<IStatRepository<Detail>, StatRepository>();
            services.AddTransient<IPriceRepository<Price>, PriceRepository>();
            services.AddTransient<IPriceDetailRepository<PriceDetail>, PriceDetailRepository>();
            services.AddTransient<IExchangeRateRepository<ExchengeRate>, ExchangeRateRepository>();
            services.AddTransient<IWebApiClientExchangeRate, WebApiClientExchangeRate>();
            services.AddTransient<IMqttManagedClient, AMSMqttManagedClient>();
            services.AddSingleton<IConfig, Config.Config>();
            // services.AddTransient<AMSMqttManagedClient>();
            return services;
        }
    }
    
}