using DevWebReceitas.Application.Interfaces;
using DevWebReceitas.Application.Mappers.Receitas;
using DevWebReceitas.Application.Services;
using DevWebReceitas.Domain.Interfaces.Repositories;
using DevWebReceitas.Domain.Services;
using DevWebReceitas.Domain.Services.Interfaces;
using DevWebReceitas.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DevWebReceitas.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IHostingEnvironment>(new HostingEnvironment());

            // Mappers
            services.AddSingleton<IReceitaEditDtoMapper, ReceitaEditDtoMapper>();
            services.AddSingleton<IReceitaDtoMapper, ReceitaDtoMapper>();
            services.AddSingleton<IReceitaMapper, ReceitaMapper>();

            //DomainServices            
            services.AddScoped<IReceitaDomainService, ReceitaDomainService>();
            services.AddScoped<ICategoriaDomainService, CategoriaDomainService>();

            //AppServices
            services.AddScoped<IReceitaService, ReceitaService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            // Infra - Data
            services.AddScoped<IReceitaRepository, ReceitaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IUnitOfWorkTransaction, UnitOfWorkTransaction>();
        }
    }
}
