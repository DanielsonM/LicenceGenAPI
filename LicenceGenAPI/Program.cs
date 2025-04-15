using System.Text;
using LicenceGenAPI.Configurarions;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Repository.Implementation;
using LicenceGenAPI.Repository.Service;
using LicenceGenAPI.Repository.User;
using LicenceGenAPI.Rules;
using LicenceGenAPI.Rules.Implementation;
using LicenceGenAPI.Rules.Services;
using LicenceGenAPI.Services;
using LicenceGenAPI.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var tokenConfiguration = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfiguration"))
    .Configure(tokenConfiguration);

builder.Services.AddSingleton(tokenConfiguration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfiguration.Issuer,
        ValidAudience = tokenConfiguration.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
    };
});

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddAuthorization(auth =>
auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build()));

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LicenceGenAPI", Version = "v1" });

    // Configuração de autenticação Bearer no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {token}"
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

// Configuração do banco de dados
builder.Services.AddDbContext<PostgreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionPostgre"), version =>
        version.SetPostgresVersion(17, 4)));

builder.Services.AddApiVersioning();

builder.Services.AddScoped<ILicenceRuleService, LicenceRule>();
builder.Services.AddScoped<ILicenceRepositoryService, LicenceRepository>();
builder.Services.AddTransient<IToken, TokenService>();
builder.Services.AddTransient<ILoginRule, LoginRule>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Aplica migrações automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PostgreDbContext>();
    context.Database.Migrate();

}

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LicenceGenAPI v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();