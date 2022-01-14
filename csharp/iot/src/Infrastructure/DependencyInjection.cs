﻿using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IAppclicationDbContext,ApplicationDbContext>();
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<IRawRepository<RawData>, RawRepository>();
            services.AddTransient<IDetailRepository<Detail>, DetailRepository>();
            
            return services;
        }
    }
    
}