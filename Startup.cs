using IDGS904_API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IDGS904_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                var frontendURL = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("conexion"))
            );
            services.AddDbContext<AppDb2Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("conexionComun"))
            );
            //var connectionString = Configuration.GetConnectionString("conexionComun");

            //// Registrar el contexto con la cadena de conexión
            //services.AddDbContext<AppDb2Context>(options => options.UseSqlServer(connectionString));
            // ...
        }



        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            // ...
        }
    }
}
