using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebApi.Modelos;
using WebApi.Servicios;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();//Eliminar

            //Creaci�n del contenedor de la aplicaci�n llamado builder
            var builder = WebApplication.CreateBuilder(args);

            //Configuraci�n de la base de datos en el contenedor builder
            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));

            //Para evitar errores en la ejecuci�n de la API
            builder.Services.AddMvc(options =>
            {
                //M�todo que cuando es true recorta el sufijo Async aplicado en los m�todos
                options.SuppressAsyncSuffixInActionNames = false;
            });

            //Permite el acceso de los servicios a la base de datos
            builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            //A�adir cada servicio de la siguiente manera: services.AddSingleton<{Nombre del servicio}>();
            builder.Services.AddSingleton<ServicioServicio>();
            builder.Services.AddSingleton<DireccionServicio>();
            builder.Services.AddSingleton<UsuarioServicio>();
            builder.Services.AddSingleton<RestauranteServicio>();
            //A�ade los controladores de los servicios
            builder.Services.AddControllers();

            //A�ade un documento swagger para controlar la API
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            //Tras realizar los pasos anteriores se ejecuta el builder
            var app = builder.Build();
            
            //Configuraci�n de las peticiones con HTTP
            if (builder.Environment.IsDevelopment())
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

            app.Run();
        }
        
        
    }
}

