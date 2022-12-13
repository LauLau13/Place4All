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
            //Nombre de la politica
            var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            //Creación del contenedor de la aplicación llamado builder
            var builder = WebApplication.CreateBuilder(args);

            //Creamos la politica de CORS
            builder.Services.AddCors(options =>
            {
                //Le añadimos un el nombre que hemos creado arriba
                options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
                {
                    //Todos los headers, todos los métodos y todos los origines
                    //TODO Restringir el origen de las llamadas
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            //Configuración de la base de datos en el contenedor builder
            builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));

            //Para evitar errores en la ejecución de la API
            builder.Services.AddMvc(options =>
            {
                //Método que cuando es true recorta el sufijo Async aplicado en los métodos
                options.SuppressAsyncSuffixInActionNames = false;
            });

            //Permite el acceso de los servicios a la base de datos
            builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            //Añadir cada servicio de la siguiente manera: services.AddSingleton<{Nombre del servicio}>();
            builder.Services.AddSingleton<ServicioServicio>();
            builder.Services.AddSingleton<DireccionServicio>();
            builder.Services.AddSingleton<UsuarioServicio>();
            builder.Services.AddSingleton<RestauranteServicio>();
            builder.Services.AddSingleton<ReservaServicio>();
            
            //Añade los controladores de los servicios
            builder.Services.AddControllers();

            //Añade un documento swagger para controlar la API
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            //Tras realizar los pasos anteriores se ejecuta el builder
            var app = builder.Build();
            
            //Configuración de las peticiones con HTTP
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
        
        
    }
}

