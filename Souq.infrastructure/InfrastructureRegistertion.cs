using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Souq.core.Interfaces;
using Souq.infrastructure.Data;
using Souq.infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souq.infrastructure
{
    public static class InfrastructureRegistertion
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("SouqDatabase"));
            });
            return services;
        }
    }
}
