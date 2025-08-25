using Business.Abstract;
using Business.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAPI4
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Servisler burada eklenir (IoC Container)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IProductService, ProductManager>();//IProductService interface’ini isteyen bir yere ProductManager nesnesini ver.
            //Yani controller veya başka sınıf IProductService isterse, IoC Container arka planda otomatik new ProductManager() yapıp verir.


            // Örneğin Swagger
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

            // Örneğin dependency injection için:
            // services.AddScoped<IProductService, ProductManager>();
            // services.AddScoped<IProductDal, EfProductDal>();
        }

        // Middleware pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
