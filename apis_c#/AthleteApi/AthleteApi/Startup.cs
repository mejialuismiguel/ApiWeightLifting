using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using AthleteApi.Data;
using AthleteApi.Services;


public class Startup
{
    // Constructor de la clase Startup que recibe una instancia de IConfiguration
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; }

    // Método para configurar los servicios
    public void ConfigureServices(IServiceCollection services)
    {
        // Agregar soporte para controladores
        services.AddControllers();

        // Configurar el contexto de la base de datos con SQL Server
        services.AddDbContext<AthleteContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Registrar servicios personalizados en el contenedor de dependencias
        services.AddScoped<IAthleteService, AthleteService>();
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IWeightCategoryService, WeightCategoryService>();
        services.AddScoped<ITournamentParticipationService, TournamentParticipationService>();
        services.AddScoped<IAttemptService, AttemptService>();
        services.AddScoped<IAthleteAttemptSummaryService, AthleteAttemptSummaryService>();
        services.AddScoped<ICompetitionResultService, CompetitionResultService>();

        // Registrar el generador de Swagger, definiendo uno o más documentos Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Athlete API",
                Description = "A simple example ASP.NET Core Web API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Your Name",
                    Email = string.Empty,
                    Url = new Uri("https://twitter.com/yourname"),
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://example.com/license"),
                }
            });

            // Activar anotaciones para documentacion Swagger
            c.EnableAnnotations();

            // Agregar autenticación JWT a Swagger
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securityScheme, new[] { "Bearer" } }
            };

            c.AddSecurityRequirement(securityRequirement);
        });

        // Configurar la autenticación JWT
        var jwtKey = Configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "JWT Key is not configured.");
        var key = Encoding.ASCII.GetBytes(jwtKey);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }

    // Método para configurar el pipeline de la aplicación
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configuración específica para el entorno de desarrollo
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Habilitar redirección HTTPS
        app.UseHttpsRedirection();

        // Configurar enrutamiento
        app.UseRouting();

        // Habilitar autenticación y autorización
        app.UseAuthentication();
        app.UseAuthorization();

        // Configurar los endpoints para los controladores
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Habilitar el middleware para servir el JSON generado por Swagger como un endpoint
        app.UseSwagger();

        // Habilitar el middleware para servir Swagger UI (HTML, JS, CSS, etc.),
        // especificando el endpoint JSON de Swagger
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Athlete API V1");
            c.RoutePrefix = string.Empty; // Colocar Swagger UI en la raíz de la aplicación
        });
    }
}
