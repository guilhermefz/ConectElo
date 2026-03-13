using AutoMapper;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Application.Areas.EventosArea.Mappers;
using ConectElo.Application.Areas.EventosArea.Services;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Application.Areas.Social.Mappers;
using ConectElo.Application.Areas.Social.Services;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Eventos.Repositories;
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
                    cfg.AddProfile<EventoProfile>();
                },
                NullLoggerFactory.Instance
            );

            builder.Services.AddSingleton(mapperConfig.CreateMapper());

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ConectElo.Infra")));

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<Usuario>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();
            
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
            builder.Services.AddScoped<IEventoRepository, EventoRepository>();
            builder.Services.AddScoped<IEventoService, EventoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Equals("/register", StringComparison.OrdinalIgnoreCase)
                    && context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status410Gone;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        sucesso = false,
                        mensagem = "Este endpoint foi desativado. Use POST /api/Usuario/Salvar para registrar um novo usuário."
                    });
                    return;
                }
                await next(context);
            });

            app.MapControllers();

            app.MapIdentityApi<Usuario>();

            app.Run();
        }
    }
}
