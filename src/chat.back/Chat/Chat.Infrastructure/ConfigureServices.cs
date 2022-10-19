﻿using Chat.AppCore.Common.Interfaces;
using Chat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Chat.Infrastructure.Persistence.Repository;

namespace Chat.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider =>
        {
            var context = provider.GetRequiredService<ApplicationDbContext>();
            if (context.Database.EnsureCreated()) context.Database.MigrateAsync();

            return provider.GetRequiredService<ApplicationDbContext>();
        });
        
        services.AddScoped<IChatRepository, ChatRepository>();
        
        return services;
    }
}