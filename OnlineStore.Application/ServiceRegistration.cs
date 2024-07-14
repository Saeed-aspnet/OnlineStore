using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Interfaces.Services;
using OnlineStore.Application.Services;
using System.Reflection;

namespace OnlineStore.Application
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
        }
    }
}