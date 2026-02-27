using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Application.Areas.Social.Services;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Repositories;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace ConectElo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ConectElo.Infra")));
            
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
