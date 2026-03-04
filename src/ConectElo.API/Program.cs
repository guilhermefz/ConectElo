using AutoMapper;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Application.Areas.Social.Mappers;
using ConectElo.Application.Areas.Social.Services;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Social.Repositories;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
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

            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<GrupoProfile>();
                    cfg.AddProfile<UsuarioProfile>();
                    cfg.AddProfile<MembroGrupoProfile>();
                    cfg.AddProfile<PostagemProfile>();
                },
                NullLoggerFactory.Instance
            );

            builder.Services.AddSingleton(mapperConfig.CreateMapper());

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ConectElo.Infra")));
            
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();
            builder.Services.AddScoped<IGrupoService, GrupoService>();
            builder.Services.AddScoped<IMembrosGrupoRepository, MembrosGrupoRepository>();
            builder.Services.AddScoped<IMembrosGrupoService, MembroGrupoService>();
            builder.Services.AddScoped<IMuralRepository, MuralRepository>();
            builder.Services.AddScoped<IMuralService, MuralService>();
            builder.Services.AddScoped<IPostagemRepository, PostagemRepository>();
            builder.Services.AddScoped<IPostagemService, PostagemService>();

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
