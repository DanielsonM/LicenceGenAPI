using Asp.Versioning;
using LicenceGenAPI.Configurations;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Repository.Implementation;
using LicenceGenAPI.Repository.Service;
using LicenceGenAPI.Rules.Implementation;
using LicenceGenAPI.Rules.Services;
using LicenceGenAPI.TokenService;
using LicenceGenAPI.TokenService.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestWithASPNETErudio.Services.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Adicionando URLs amigáveis
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configuração JWT (TokenConfiguration)
var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
    builder.Configuration.GetSection("JwtSettings")).Configure(tokenConfigurations);
builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration[tokenConfigurations.Issuer],
            ValidAudience = builder.Configuration[tokenConfigurations.Audience],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
        };
    });

// Configuração de autorização
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LicenceGenAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira 'Bearer' seguido pelo token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configuração de logging para depuração
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Registro do Entity Framework e conexão com PostgreSQL
builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionPostgre"), version =>
        version.SetPostgresVersion(17, 4)));

// Ativação de versionamento de API
builder.Services.AddApiVersioning();

// Registro dos serviços e repositórios
builder.Services.AddScoped<ILicenceRuleService, LicenceRule>();
builder.Services.AddScoped<ILicenceRepositoryService, LicenceRepository>();
builder.Services.AddScoped<ILoginService, LoginRuleImplementation>();
builder.Services.AddTransient<ITokenService, TokenImplementation>();
builder.Services.AddScoped<IUserRepositoryService, UsersRepository>();

// Adicionando suporte a controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configuração do pipeline de middleware
app.UseHttpsRedirection();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.MapControllers();
app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

// Aplicação automática de migrações (opcional)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PostgreDbContext>();
    // context.Database.Migrate();
}

// Mapeando controladores
app.MapControllers();

app.Run();