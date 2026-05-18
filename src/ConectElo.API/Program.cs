using AutoMapper;
using ConectElo.API.Areas.Comunicacao.Hubs;
using ConectElo.Application.Areas.Autenticacao.InterfacesService;
using ConectElo.Application.Areas.Autenticacao.Services;
using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using ConectElo.Application.Areas.Comunicacao.Mappers;
using ConectElo.Application.Areas.Comunicacao.Services;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Application.Areas.EventosArea.Mappers;
using ConectElo.Application.Areas.EventosArea.Services;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Application.Areas.Social.Mappers;
using ConectElo.Application.Areas.Social.Services;
using ConectElo.Domain.Areas.Comunicacao.InterfacesRepository;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Comunicacao.Repositories;
using ConectElo.Infra.Areas.Eventos.Repositories;
using ConectElo.Infra.Areas.Social.Repositories;
using ConectElo.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Text;

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
                    cfg.AddProfile<MensagemProfile>();
                },
                NullLoggerFactory.Instance
            );

            builder.Services.AddSingleton(mapperConfig.CreateMapper());

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, ct) =>
                {
                    document.Servers = [new OpenApiServer { Url = "http://localhost:5000" }];
                    document.Components ??= new();
                    document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                    {
                        ["Bearer"] = new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            Description = "Insira o token JWT obtido no endpoint de login."
                        }
                    };
                    document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }] = []
                    });
                    return Task.CompletedTask;
                });
            });


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ConectElo.Infra")).EnableThreadSafetyChecks(false));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/chat"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<Usuario>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();
            
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();
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
            builder.Services.AddScoped<IFeedService, FeedService>();
            builder.Services.AddScoped<IArquivoRepository, ArquivoRepository>();
            builder.Services.AddScoped<IArquivoService, ArquivoService>();
            builder.Services.AddScoped<IMensagemRepository, MensagemRepository>();
            builder.Services.AddScoped<IMensagemService, MensagemService>();

            builder.Services.AddSignalR();

            builder.Services.AddCors(options => {
                options.AddPolicy("Default", policy => {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.MapOpenApi();
                app.MapScalarApiReference();
            //}

            var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath);

            app.UseAuthentication();
            app.UseCors("Default");
            app.UseAuthorization();
            app.MapHub<ChatHub>("/hubs/chat");
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsPath),
                RequestPath = "/uploads"
            });

            app.Use(async (context, next) =>
            {
                var desativados = new[] { "/register", "/login" };
                if (desativados.Any(p => context.Request.Path.Equals(p, StringComparison.OrdinalIgnoreCase))
                    && context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = StatusCodes.Status410Gone;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        sucesso = false,
                        mensagem = "Este endpoint foi desativado. Use POST /api/Usuario/Salvar para registrar e POST /api/Autenticacao/Login para autenticar."
                    });
                    return;
                }
                await next(context);
            });


            app.MapControllers();

            app.MapIdentityApi<Usuario>();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
