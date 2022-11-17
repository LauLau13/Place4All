using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebApi.Modelos;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este metodo es llamado por el runtime. Usar este método para añadir servicios al contenedor.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings))
            );

            services.AddSingleton<IDatabaseSettings>(
                sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value
            );

            //Añadir cada servicio de la siguiente manera: services.AddSingleton<{Nombre del servicio}>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }
        // Este metido es llamado por el runtime. Usa este método para configurat el HTTP request pipeline. 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
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
